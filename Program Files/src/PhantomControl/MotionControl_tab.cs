using EasyModbus;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.Design;
using System.Windows;
using System.Windows.Forms;


// This class is where most of the functions and other classes are called. The GUI MotionControl is run controlled through this class. 

namespace PhantomControl
{
    public partial class MotionControl_tab : UserControl
    {

        private bool _homebuttonclicked = false;
        private bool _playstopmotionclicked = false;
        public static double chartRange = 0;
        static SerialPort serialPort;
        double currentTime = 0.2;

        public int maxXValue;


        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        URServer urServer = new URServer();
        ModbusClient modbusClient = new ModbusClient();
        CoordinateTransformation coordTransform = new CoordinateTransformation();

        public ChartValues<DataStream> InputValues_X { get; set; }

        public ChartValues<DataStream> InputValues_Y { get; set; }

        public ChartValues<DataStream> InputValues_Z { get; set; }

        public ChartValues<DataStream> InputValues_Rx { get; set; }

        public ChartValues<DataStream> InputValues_Ry { get; set; }

        public ChartValues<DataStream> InputValues_Rz { get; set; }

        public ChartValues<DataStream> ChartValues_X { get; set; }

        public ChartValues<DataStream> ChartValues_Y { get; set; }

        public ChartValues<DataStream> ChartValues_Z { get; set; }

        public ChartValues<DataStream> ChartValues_Rx { get; set; }

        public ChartValues<DataStream> ChartValues_Ry { get; set; }

        public ChartValues<DataStream> ChartValues_Rz { get; set; }

        public ChartValues<DataStream> InputValues1D_Y { get; set; }
        public ChartValues<DataStream> ChartValues1D_Y { get; set; }

        //TestCS

        private string iconPath = "Resources\\Icons\\";

        bool _keepMonitoring = false;
        bool _keep1D = false;
        bool _firstRun = true;

        private object monitorDataLock = new object();

        int[] _holdingRegPoseLock;
        int[] _holdingRegTCPSpeedLock;
        int[] _holdingRegTime;

        double[] poseArray = new double[6];
        double[] poseSpeedArray = new double[6];


        public MotionControl_tab()
        {
            if (!this.DesignMode)
            {
                InitializeComponent();

                MotionTraces.setStartingPose();
                maxXValue = 1;
                initialseChart();




                //Add Led Bulbs 
                AddLedbulbs();

                // Check which robot was selected and disable he necessary buttons

                UpdateButtons();
                flatButton_Home.Enabled = false;
                flatButton_LoadTraces.Enabled = false;
                flatButton_PlayStopMotion.Enabled = false;
                flatButton_ResumeMotions.Enabled = false;

            }
        }

        private void UpdateButtons()
        {

            // Update the label to the current robot being used

            if (Settings_tab.bothSelected)
            {
                robotSelectLabel.Text = $"CURRENT ROBOT: BOTH";
                Logger.addToLogFile("CURRENT ROBOT SELECTED: BOTH");
                UpdateStatusBarMessage.ShowStatusMessage("CURRENT ROBOT SELECTED: BOTH");
                //flatButton_LoadTraces.Enabled = true;
                //flatButton_LoadTraces1D.Enabled = true;


            }
            if (Settings_tab.oneSelected)
            {
                robotSelectLabel.Text = $"CURRENT ROBOT: 1D";
                Logger.addToLogFile("CURRENT ROBOT SELECTED: 1D");
                UpdateStatusBarMessage.ShowStatusMessage("CURRENT ROBOT SELECTED: 1D");
                flatButton_LoadTraces.Enabled = false;
                //flatButton_LoadTraces1D.Enabled = true;
            }
            if (Settings_tab.sixSelected)
            {
                robotSelectLabel.Text = $"CURRENT ROBOT: 6DOF";
                Logger.addToLogFile("CURRENT ROBOT SELECTED: 6DOF");
                UpdateStatusBarMessage.ShowStatusMessage("CURRENT ROBOT SELECTED: 6DOF");
                flatButton_LoadTraces1D.Enabled = false;
                //flatButton_LoadTraces.Enabled = true;
            }
        }
        private void AddLedbulbs()
        {
            ledBulb_Connected = new Bulb.LedBulb();
            ledBulb_Connected.Size = new System.Drawing.Size(20, 20);
            ledBulb_Connected.Color = Color.Gray;
            ledBulb_Connected.On = false;
            this.panel9.Controls.Add(ledBulb_Connected);

            ledBulb_Ready = new Bulb.LedBulb();
            ledBulb_Ready.Size = new System.Drawing.Size(20, 20);
            ledBulb_Ready.Color = Color.Gray;
            ledBulb_Ready.On = false;
            this.panel8.Controls.Add(ledBulb_Ready);

        }

        // Function sets up plotting settings and objects

        private void initialseChart()
        {
            // this.cartesianChart_rotation.Size = new System.Drawing.Size(2020, 948);

            var mapper = Mappers.Xy<DataStream>()
                .X(model => model.X)                //use DateTime.Ticks as X
                .Y(model => model.Y);               //use the value property as Y

            //lets save the mapper globally.
            Charting.For<DataStream>(mapper);


            InputValues_X = new ChartValues<DataStream>();
            InputValues_Y = new ChartValues<DataStream>();
            InputValues_Z = new ChartValues<DataStream>();
            InputValues_Rx = new ChartValues<DataStream>();
            InputValues_Ry = new ChartValues<DataStream>();
            InputValues_Rz = new ChartValues<DataStream>();
            InputValues1D_Y = new ChartValues<DataStream>();

            ChartValues_X = new ChartValues<DataStream>();
            ChartValues_Y = new ChartValues<DataStream>();
            ChartValues_Z = new ChartValues<DataStream>();
            ChartValues_Rx = new ChartValues<DataStream>();
            ChartValues_Ry = new ChartValues<DataStream>();
            ChartValues_Rz = new ChartValues<DataStream>();
            ChartValues1D_Y = new ChartValues<DataStream>();

            //1D Chart


            //TestCS

            cartesianChart_translation.LegendLocation = LegendLocation.Right;
            cartesianChart_translation.DisableAnimations = true;
            cartesianChart_translation.DataTooltip = null;
            cartesianChart_translation.Hoverable = false;

            cartesianChart_rotation.LegendLocation = LegendLocation.Right;
            cartesianChart_rotation.DisableAnimations = true;
            cartesianChart_rotation.DataTooltip = null;
            cartesianChart_rotation.Hoverable = false;

            cartesianChart_translation.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointForeground = System.Windows.Media.Brushes.Blue,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection { 5 },
                    Stroke = System.Windows.Media.Brushes.Blue,
                    Values = InputValues_X,
                    PointGeometrySize = 0,
                    StrokeThickness = 0.7,
                    LineSmoothness = 0,
                    Title = "Input LR",


                },
                new LineSeries
                {
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointForeground = System.Windows.Media.Brushes.Green,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection { 5 },
                    Stroke = System.Windows.Media.Brushes.Green,
                    Values = InputValues_Y,
                    PointGeometrySize = 0,
                    StrokeThickness = 0.7,
                    LineSmoothness = 0,
                    Title = "Input SI"
                },
                new LineSeries
                {
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointForeground = System.Windows.Media.Brushes.Red,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection { 5 },
                    Stroke = System.Windows.Media.Brushes.Red,
                    Values = InputValues_Z,
                    PointGeometrySize = 0,
                    StrokeThickness = 0.7,
                    LineSmoothness = 0,
                    Title = "Input AP"
                },

                new LineSeries
                {
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointForeground = System.Windows.Media.Brushes.Blue,
                    Stroke = System.Windows.Media.Brushes.Blue,
                    Values = ChartValues_X,
                    PointGeometrySize = 0,
                    StrokeThickness = 1.3,
                    LineSmoothness = 0,
                    Title = "LR"
                },
                new LineSeries
                {
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointForeground = System.Windows.Media.Brushes.Green,
                    Stroke = System.Windows.Media.Brushes.Green,
                    Values = ChartValues_Y,
                    PointGeometrySize = 0,
                    StrokeThickness = 1.3,
                    LineSmoothness = 0,
                    Title = "SI"
                },
                new LineSeries
                {
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointForeground = System.Windows.Media.Brushes.Red,
                    Stroke = System.Windows.Media.Brushes.Red,
                    Values = ChartValues_Z,
                    PointGeometrySize = 0,
                    StrokeThickness = 1.3,
                    LineSmoothness = 0,
                    Title = "AP"
                },
                new LineSeries
                {
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointForeground = System.Windows.Media.Brushes.Gold,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection { 5 },
                    Stroke = System.Windows.Media.Brushes.Gold,
                    Values = InputValues1D_Y,
                    PointGeometrySize = 0,
                    StrokeThickness = 1.3,
                    LineSmoothness = 0,
                    Title = "Input AP 1D"
                },
                new LineSeries
                {
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointForeground = System.Windows.Media.Brushes.Gold,
                    Stroke = System.Windows.Media.Brushes.Gold,
                    Values = ChartValues1D_Y,
                    PointGeometrySize = 0,
                    StrokeThickness = 1.3,
                    LineSmoothness = 0,
                    Title = "1D AP"
                },
            };

            cartesianChart_rotation.Series = new SeriesCollection
            {

                new LineSeries
                {
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointForeground = System.Windows.Media.Brushes.Blue,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection { 5 },
                    Stroke = System.Windows.Media.Brushes.Blue,
                    Values = InputValues_Rx,
                    PointGeometrySize = 0,
                    StrokeThickness = 0.7,
                    LineSmoothness = 0,
                    Title = "Input rLR"
                },
                new LineSeries
                {
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointForeground = System.Windows.Media.Brushes.Green,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection { 5 },
                    Stroke = System.Windows.Media.Brushes.Green,
                    Values = InputValues_Ry,
                    PointGeometrySize = 0,
                    StrokeThickness = 0.7,
                    LineSmoothness = 0,
                    Title = "Input rSI"
                },
                new LineSeries
                {
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointForeground = System.Windows.Media.Brushes.Red,
                    StrokeDashArray = new System.Windows.Media.DoubleCollection { 5 },
                    Stroke = System.Windows.Media.Brushes.Red,
                    Values = InputValues_Rz,
                    PointGeometrySize = 0,
                    StrokeThickness = 0.7,
                    LineSmoothness = 0,
                    Title = "Input rAP"
                },

                new LineSeries
                {
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointForeground = System.Windows.Media.Brushes.Blue,
                    Stroke = System.Windows.Media.Brushes.Blue,
                    Values = ChartValues_Rx,
                    PointGeometrySize = 0,
                    StrokeThickness = 1.3,
                    LineSmoothness = 0,
                    Title = "rLR"
                },
                new LineSeries
                {
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointForeground = System.Windows.Media.Brushes.Green,
                    Stroke = System.Windows.Media.Brushes.Green,
                    Values = ChartValues_Ry,
                    PointGeometrySize = 0,
                    StrokeThickness = 1.3,
                    LineSmoothness = 0,
                    Title = "rSI"
                },
                new LineSeries
                {
                    Fill = System.Windows.Media.Brushes.Transparent,
                    PointForeground = System.Windows.Media.Brushes.Red,
                    Stroke = System.Windows.Media.Brushes.Red,
                    Values = ChartValues_Rz,
                    PointGeometrySize = 0,
                    StrokeThickness = 1.3,
                    LineSmoothness = 0,
                    Title = "rAP"
                }
            };

            System.Windows.Media.FontFamily font = new System.Windows.Media.FontFamily("Century Gothic");


            cartesianChart_translation.AxisX.Add(new Axis
            {
                Foreground = System.Windows.Media.Brushes.Black,
                Title = "Time (s)",
                FontFamily = font,
                FontSize = 15,
                MaxValue = chartRange,
                LabelFormatter = value => value.ToString("N1")

            });

            cartesianChart_translation.AxisY.Add(new Axis
            {
                Foreground = System.Windows.Media.Brushes.Black,
                Title = "Translation (mm)",
                FontFamily = font,
                FontSize = 15,
                LabelFormatter = value => value.ToString("N2")
            });

            cartesianChart_rotation.AxisX.Add(new Axis
            {
                Foreground = System.Windows.Media.Brushes.Black,
                Title = "Time (s)",
                FontFamily = font,
                FontSize = 15,
                MaxRange = 20,
                LabelFormatter = value => value.ToString("N1")
            });

            cartesianChart_rotation.AxisY.Add(new Axis
            {
                Foreground = System.Windows.Media.Brushes.Black,
                Title = "Rotation (deg)",
                FontFamily = font,
                FontSize = 15,
                LabelFormatter = value => value.ToString("N2")
            });

            ChartControls();
        }

        private void ToggleInputAP(object sender, System.EventArgs e)
        {

        }
        public void ChartControls()
        {
            cartesianChart_translation.Zoom = ZoomingOptions.Xy;
            cartesianChart_translation.Pan = PanningOptions.Xy;
            cartesianChart_rotation.Zoom = ZoomingOptions.Xy;
            cartesianChart_rotation.Pan = PanningOptions.Xy;
        }
        // Plots input motion data


        private void drawInputTrace()
        {


            for (int i = 0; i < MotionTraces.Size; i++)
            {
                double t = MotionTraces.t[i];
                double x = MotionTraces.X[i] * 1000;
                double y = MotionTraces.Y[i] * 1000;
                double z = MotionTraces.Z[i] * 1000;

                double Rx = MotionTraces.Rx[i] * (180 / Math.PI);
                double Ry = MotionTraces.Ry[i] * (180 / Math.PI);
                double Rz = MotionTraces.Rz[i] * (180 / Math.PI);

                /*                foreach (var XAxis in cartesianChart_translation.AxisX)
                                {
                                    XAxis.MinValue = 0;
                                    XAxis.MaxValue = MotionTraces.t[i];
                                    XAxis.SetRange(XAxis.MinValue, XAxis.MaxValue);
                                }*/
                rangeBar.Maximum = (int)MotionTraces.t.Max();
                plotInputData(t, x, y, z, Rx, Ry, Rz);
            }

        }
        private void drawInputTrace1D(List<double> displacementValues)
        {
            List<double> TimeList = new List<double>();
            double currentValue = 0.0;
            for (double i = 0; i < displacementValues.Count; i++)
            {
                TimeList.Add(currentValue);
                currentValue += 0.2;

            }

            for (int i = 0; i < displacementValues.Count; i++)
            {
                double t = TimeList[i];
                double y = displacementValues[i];

                foreach (var XAxis in cartesianChart_translation.AxisX)
                {
                    XAxis.MinValue = 0;
                    XAxis.MaxValue = TimeList[i];
                    XAxis.SetRange(XAxis.MinValue, XAxis.MaxValue);
                }

                rangeBar.Maximum = (int)TimeList.Max();
                plotInputData1D(t, y);

            }
        }



        private void plotInputData(double time, double x, double y, double z, double Rx, double Ry, double Rz)
        {
            InputValues_X.Add(new DataStream
            {
                X = time,
                Y = x
            });

            InputValues_Y.Add(new DataStream
            {
                X = time,
                Y = y
            });

            InputValues_Z.Add(new DataStream
            {
                X = time,
                Y = z
            });
            InputValues_Rx.Add(new DataStream
            {
                X = time,
                Y = Rx
            });

            InputValues_Ry.Add(new DataStream
            {
                X = time,
                Y = Ry
            });

            InputValues_Rz.Add(new DataStream
            {
                X = time,
                Y = Rz
            });

        }
        private void plotInputData1D(double time, double y)
        {

            InputValues1D_Y.Add(new DataStream
            {
                X = time,
                Y = y
            });
        }

        // Plots data streamed from MODBUS i.e. output data from robot

        private void plotData(double time, double x, double y, double z, double Rx, double Ry, double Rz)
        {

            ChartValues_X.Add(new DataStream
            {
                X = time,
                Y = x
            });

            ChartValues_Y.Add(new DataStream
            {
                X = time,
                Y = y
            });

            ChartValues_Z.Add(new DataStream
            {
                X = time,
                Y = z
            });
            ChartValues_Rx.Add(new DataStream
            {
                X = time,
                Y = Rx
            });

            ChartValues_Ry.Add(new DataStream
            {
                X = time,
                Y = Ry
            });

            ChartValues_Rz.Add(new DataStream
            {
                X = time,
                Y = Rz
            });

        }
        private void plotData1D(double time, double y)
        {


            ChartValues1D_Y.Add(new DataStream
            {
                X = time,
                Y = y
            });

        }

        // Clears plot in GUI, input string to clear plot "all", "output". or "input"

        private void clearPlot(string value)
        {
            if (value == "all")
            {
                foreach (var series in cartesianChart_translation.Series)
                {
                    series.Values.Clear();
                }
                foreach (var series in cartesianChart_rotation.Series)
                {
                    series.Values.Clear();
                }
            }
            if (value == "output")
            {
                cartesianChart_translation.Series.ElementAt(3).Values.Clear();
                cartesianChart_translation.Series.ElementAt(4).Values.Clear();
                cartesianChart_translation.Series.ElementAt(5).Values.Clear();

                cartesianChart_rotation.Series.ElementAt(3).Values.Clear();
                cartesianChart_rotation.Series.ElementAt(4).Values.Clear();
                cartesianChart_rotation.Series.ElementAt(5).Values.Clear();
            }
            if (value == "input")
            {
                cartesianChart_translation.Series.ElementAt(0).Values.Clear();
                cartesianChart_translation.Series.ElementAt(1).Values.Clear();
                cartesianChart_translation.Series.ElementAt(2).Values.Clear();

                cartesianChart_rotation.Series.ElementAt(0).Values.Clear();
                cartesianChart_rotation.Series.ElementAt(1).Values.Clear();
                cartesianChart_rotation.Series.ElementAt(2).Values.Clear();
            }
            else
            {
                return;
            }
        }

        // Sets axis limits on plot (Not used at the moment)

        private void setAxisLimits(double min, double max)
        {
            cartesianChart_translation.AxisX[0].MaxValue = max;
            cartesianChart_translation.AxisX[0].MinValue = min;
        }


        // Gets number of columns of input txt file

        private int getColNumber(OpenFileDialog ofd)
        {
            int ColumnsCount = 0;
            StreamReader stream = new StreamReader(ofd.FileName);

            try
            {
                ColumnsCount = Array.ConvertAll(stream.ReadLine().Split(' '), Double.Parse).Length;
            }

            catch (Exception excep)
            {
                UpdateStatusBarMessage.ShowStatusMessage("Error: Invalid file format");
                Logger.addToLogFile("Error: Invalid file format");
            }

            stream.Close();
            return ColumnsCount;
        }


        //Boolean which alert if input file lines are greater than maximum capacity
        private static bool boolExceedMemory = false;
        // Function loads motion data for robot to executed, data is stored in a global class names MotionTraces. Format of txt file is t x y z rx ry rz, that is for each value seprated by space followed by new line after rz

        private void flatButton_LoadTraces_Click(object sender, EventArgs e)
        {
            int MaximumInputLines = 1501;
            double timeKinematics_temp = UrSettings.TimeKinematics;
            if (_homebuttonclicked != true)
            {

                System.Windows.MessageBox.Show("Please click the home button first");
                return;
            }
            _homebuttonclicked = false;


            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true, Multiselect = false })
            {

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    flatButton_PlayStopMotion.Enabled = false;

                    MotionTraces.X.Clear();
                    MotionTraces.X.Clear();
                    MotionTraces.Y.Clear();
                    MotionTraces.Z.Clear();
                    MotionTraces.Rx.Clear();
                    MotionTraces.Ry.Clear();
                    MotionTraces.Rz.Clear();

                    if (Settings_tab.sixSelected)
                    {
                        textBox_filename.Text = System.IO.Path.GetFileName(ofd.FileName);
                        _filePath6D = textBox_filename.Text;
                        Logger.RenameLogFile("6D", textBox_filename.Text);
                        Logger.addToLogFile("The 6D input file " + textBox_filename.Text + " has been selected");

                        clearPlot("all");
                    }

                    if (Settings_tab.bothSelected)
                    {
                        textBox_filename.Text = System.IO.Path.GetFileName(ofd.FileName);
                        _filePath6D = textBox_filename.Text;


                        if (Logger.LogFileNameBOTHBool)
                        {
                            Logger.addToLogFile("The 6D input file " + textBox_filename.Text + " has been selected - BOTH PLATFORMS");
                        }
                        else
                        {
                            Logger.RenameLogFileBOTH("BOTH", textBox_filename.Text);
                            Logger.addToLogFile("The 6D input file " + textBox_filename.Text + " has been selected - BOTH PLATFORMS");

                        }
                    }


                    //char filename = OpenFileDialog.SafeFileName;
                    var FileName = System.IO.Path.GetFileName(ofd.FileName);

                    textBox_filename.Text = System.IO.Path.GetFileName(ofd.FileName);

                    _filePath6D = textBox_filename.Text;

                    int columnNum = getColNumber(ofd);

                    using (StreamReader sr = new StreamReader(ofd.FileName))
                    {
                        if (columnNum == 6)
                        {
                            string line;
                            int counter = 0;
                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] arr = line.Split(' ');

                                MotionTraces.X.Add(double.Parse(arr[0]) / 1000);
                                MotionTraces.Y.Add(double.Parse(arr[1]) / 1000);
                                MotionTraces.Z.Add(double.Parse(arr[2]) / 1000);

                                MotionTraces.Rx.Add(double.Parse(arr[3]) * (Math.PI / 180));
                                MotionTraces.Ry.Add(double.Parse(arr[4]) * (Math.PI / 180));
                                MotionTraces.Rz.Add(double.Parse(arr[5]) * (Math.PI / 180));

                                counter++;
                            }
                            MotionTraces.Size = counter;
                        }
                        if (columnNum == 7)
                        {
                            string line;
                            int counter = 0;
                            while ((line = sr.ReadLine()) != null)
                            {
                                string[] arr = line.Split(' ');

                                MotionTraces.t.Add(double.Parse(arr[0]));
                                MotionTraces.X.Add(double.Parse(arr[1]) / 1000);
                                MotionTraces.Y.Add(double.Parse(arr[2]) / 1000);
                                MotionTraces.Z.Add(double.Parse(arr[3]) / 1000);

                                MotionTraces.Rx.Add(double.Parse(arr[4]) * (Math.PI / 180));
                                MotionTraces.Ry.Add(double.Parse(arr[5]) * (Math.PI / 180));
                                MotionTraces.Rz.Add(double.Parse(arr[6]) * (Math.PI / 180));

                                counter++;
                            }

                            UrSettings.TimeKinematics = MotionTraces.t[1] - MotionTraces.t[0];           //Read the frequency from the input file 
                            Settings_tab.txtBox_time.Text = Convert.ToString(UrSettings.TimeKinematics);
                            Logger.addToLogFile("The sample rate from the input file is " + UrSettings.TimeKinematics + "s.");
                            MotionTraces.Size = counter;
                            if (MotionTraces.Size > MaximumInputLines)
                            {
                                boolExceedMemory = true;
                                UpdateStatusBarMessage.ShowStatusMessage("Warning: The number of lines from the input file exceeds the limit capacity. The 6DoF robot will stop and resume the motion after 5 minutes");
                                Logger.addToLogFile("Warning: The number of lines from the input file exceeds the limit capacity. The 6DoF robot will stop and resume the motion after 5 minutes");
                                System.Windows.MessageBox.Show("Warning: The number of lines from the input file exceeds the limit capacity. The 6DoF robot will stop and resume the motion after 5 minutes");
                                drawInputTrace();
                            }
                            else
                            {
                                Console.WriteLine("else");
                                drawInputTrace();
                            }
                        }

                        if (columnNum <= 5 || columnNum > 7)
                        {
                            UpdateStatusBarMessage.ShowStatusMessage("Error: Invalid format, format needed: [t  X  Y  Z  Rx  Ry  Rz] or [t  X  Y  Z]");
                            Logger.addToLogFile("Error: Invalid format, format needed: [t  X  Y  Z  Rx  Ry  Rz] or [t  X  Y  Z]");
                            System.Windows.MessageBox.Show("Error: Invalid input file format, format needed: [t  X  Y  Z  Rx  Ry  Rz] or [t  X  Y  Z]");
                        }

                        if (UrSettings.TimeKinematics < 0.2)
                        {
                            _playstopmotionclicked = false;
                            UpdateStatusBarMessage.ShowStatusMessage("Warning: Sampling rate in the input file is lower than 0.2s");
                            Logger.addToLogFile("Warning: Sampling rate in the input file is lower than 0.2s");
                            System.Windows.MessageBox.Show("Warning: The sample rate in the input file is lower than 0.2s");

                        }

                        if (UrSettings.TimeKinematics != timeKinematics_temp)
                        {
                            _playstopmotionclicked = false;
                            UpdateStatusBarMessage.ShowStatusMessage("Warning: Sampling rates in the input file and in the settings file are not the same");
                            Logger.addToLogFile("Warning: Sampling rates in the input file and in the settings file are not the same");
                            System.Windows.MessageBox.Show("Warning: The sample rates in the input file and in the settings are different");
                        }
                        if (UrSettings.TimeKinematics == Math.Abs(MotionTraces.t[1] - MotionTraces.t[2]))
                        {
                            _playstopmotionclicked = true;
                        }

                        //Finds the maximum value 
                        double maxx = 0;
                        double maxy = 0;
                        double maxz = 0;

                        for (int i = 1; i < MotionTraces.Size; i++)
                        {
                            if (MotionTraces.X[i] > maxx)
                            {
                                maxx = MotionTraces.X[i];
                            }

                            if (MotionTraces.Y[i] > maxy)
                            {
                                maxy = MotionTraces.Y[i];
                            }

                            if (MotionTraces.Z[i] > maxz)
                            {
                                maxz = MotionTraces.Z[i];
                            }
                        }


                    }
                }
                else
                {
                    return;
                }
            }

            flatButton_LoadTraces.Enabled = false;
            urServer.generateUrScript(UrSettings.TimeKinematics, UrSettings.TCP, UrSettings.PayLoad);

            if (UrSettings.movementToLarge == true)
            {
                ledBulb_Ready.Color = Color.LightPink;
                ledBulb_Ready.On = true;

                UpdateStatusBarMessage.ShowStatusMessage("Safety error: Large translation or rotation motion detected, may cause collision");
                Logger.addToLogFile("Safety error: Large translation or rotation motion detected, may cause collision");

                flatButton_LoadTraces.Enabled = true;
                flatButton_SetStartPos.Enabled = false;
                return;
            }

            if (UrSettings.singularity == true)
            {
                ledBulb_Ready.Color = Color.LightPink;
                flatButton_SetStartPos.Enabled = false;
                ledBulb_Ready.On = true;
                return;
            }
            else
            {
                ledBulb_Ready.Color = Color.LimeGreen;
                flatButton_SetStartPos.Enabled = true;
                ledBulb_Ready.On = true;
            }


            if (UrSettings.writeUrScriptFile == true)
            {
                Thread saveUrScriptFiles = new Thread(saveUrScripts);
                saveUrScriptFiles.Start();
            }
            else
            {
                flatButton_PlayStopMotion.Enabled = true;
                flatButton_LoadTraces.Enabled = true;
            }
        }

        // Saves URscript program based on lodaed motion traces as a txt file

        private void saveUrScripts()
        {
            foreach (string s in UrScriptProgram.urList)
            {
                Logger.addToUrScriptFile(s);
            }


            Invoke(new Action(() =>
            {
                flatButton_PlayStopMotion.Enabled = true;
            }));
        }

        //true -> 1D platform setup is vertical. 
        //false -> 1D platform setup is horizontal.
        private static bool bool1DConfiguration = true;

        // Buttons that will resume the 1D OR 6D motions
        private static bool motionPlay1D = false;
        private static bool isStopped = true;
        private static bool isResumed6D = false;
        private static int StopIndex;

        private void ResumeMotion6D_Process()
        {
            isResumed6D = true;
            Console.WriteLine("resume 6d");
            List<double> timeTemp = new List<double>();
            List<double> XTemp = new List<double>();
            List<double> YTemp = new List<double>();
            List<double> ZTemp = new List<double>();
            List<double> RxTemp = new List<double>();
            List<double> RyTemp = new List<double>();
            List<double> RzTemp = new List<double>();

            int indexResume6D = findNext6DPosition() + 1;

            timeTemp = MotionTraces.t.ToList();
            XTemp = MotionTraces.X.ToList();
            YTemp = MotionTraces.Y.ToList();
            ZTemp = MotionTraces.Z.ToList();
            RxTemp = MotionTraces.Rx.ToList();
            RyTemp = MotionTraces.Ry.ToList();
            RzTemp = MotionTraces.Rz.ToList();

            int MotionTracesSize = timeTemp.Count() - indexResume6D;

            MotionTraces.t.Clear();
            MotionTraces.X.Clear();
            MotionTraces.Y.Clear();
            MotionTraces.Z.Clear();
            MotionTraces.Rx.Clear();
            MotionTraces.Ry.Clear();
            MotionTraces.Rz.Clear();

            for (int i = 0; i < MotionTracesSize; i++)
            {

                MotionTraces.t.Add(i * UrSettings.TimeKinematics);
                MotionTraces.X.Add(XTemp[i + indexResume6D]);
                MotionTraces.Y.Add(YTemp[i + indexResume6D]);
                MotionTraces.Z.Add(ZTemp[i + indexResume6D]);
                MotionTraces.Rx.Add(RxTemp[i + indexResume6D]);
                MotionTraces.Ry.Add(RyTemp[i + indexResume6D]);
                MotionTraces.Rz.Add(RzTemp[i + indexResume6D]);
            }

            MotionTraces.Size = MotionTracesSize;
            clearPlot("all");
            drawInputTrace();
            lblProgressVal.Text = progressbar_Motion.Value.ToString();
            flatButton_PlayStopMotion.Text = " Stop Motion";
            flatButton_Home.Enabled = false;
            flatButton_SetStartPos.Enabled = false;

            Array.Clear(poseSpeedArray, 0, poseSpeedArray.Length);
            Array.Clear(poseArray, 0, poseArray.Length);

            if (MotionTraces.Size == 1)
            {
                progressbar_Motion.Maximum = 1;
            }
            else
            {
                progressbar_Motion.Maximum = 100;
            }
            urServer.generateUrScript(UrSettings.TimeKinematics, UrSettings.TCP, UrSettings.PayLoad);
            UrSettings.motionPlay = true;
            runMotion();
        }

        private void ResumeMotion1D_Process()
        {
            flatButton_PlayStopMotion.Text = " Stop Motion";
            flatButton_Home.Enabled = false;
            flatButton_SetStartPos.Enabled = false;
            Console.WriteLine("resume 1d");
            Resume1DRobot();
            motionPlay1D = true;
        }

        private void flatButton_1D_Configuration_Click(object sender, EventArgs e)
        {
            bool1DConfiguration = !bool1DConfiguration;
            if (bool1DConfiguration == true)
            {
                flatButton_1D_Configuration.Text = "Horizontal";
            }
            else
            {
                flatButton_1D_Configuration.Text = "Vertical";
            }
        }

        private void flatButton_ResumeMotions_Click(object sender, EventArgs e)
        {
            if (Settings_tab.sixSelected)
            {
                ResumeMotion6D_Process();
            }

            if (Settings_tab.oneSelected)
            {
                ResumeMotion1D_Process();

            }

            if (Settings_tab.bothSelected)
            {
                ResumeMotion6D_Process();
                ResumeMotion1D_Process();
            }
        }

        private async Task RestartingProcess6D()
        {
            Console.WriteLine("Exceed memory");
            int MaxLines = 1501;
            int CurrentLines = MotionTraces.Size;
            int NbRestart = (int)(CurrentLines / MaxLines);
            int remainder = CurrentLines - (NbRestart * MaxLines);
            List<double> timeTemp = new List<double>();
            List<double> XTemp = new List<double>();
            List<double> YTemp = new List<double>();
            List<double> ZTemp = new List<double>();
            List<double> RxTemp = new List<double>();
            List<double> RyTemp = new List<double>();
            List<double> RzTemp = new List<double>();

            timeTemp = MotionTraces.t.ToList();
            XTemp = MotionTraces.X.ToList();
            YTemp = MotionTraces.Y.ToList();
            ZTemp = MotionTraces.Z.ToList();
            RxTemp = MotionTraces.Rx.ToList();
            RyTemp = MotionTraces.Ry.ToList();
            RzTemp = MotionTraces.Rz.ToList();

            MotionTraces.t.Clear();
            MotionTraces.X.Clear();
            MotionTraces.Y.Clear();
            MotionTraces.Z.Clear();
            MotionTraces.Rx.Clear();
            MotionTraces.Ry.Clear();
            MotionTraces.Rz.Clear();

            Console.WriteLine(NbRestart);
            for (int i = 1; i <= NbRestart; i++)
            {
                Console.WriteLine("iteration number: " + i);
                int first_index = (i - 1) * MaxLines;
                int last_index = (i * MaxLines);
                int MotionTracesSlicedSize = last_index - first_index;
                MotionTraces.X = XTemp.GetRange(first_index, MotionTracesSlicedSize);
                for (int k = 0; k < MotionTraces.X.Count; k++)
                {
                    MotionTraces.t.Add(k * UrSettings.TimeKinematics);
                }
                MotionTraces.Y = YTemp.GetRange(first_index, MotionTracesSlicedSize);
                MotionTraces.Z = ZTemp.GetRange(first_index, MotionTracesSlicedSize);
                MotionTraces.Rx = RxTemp.GetRange(first_index, MotionTracesSlicedSize);
                MotionTraces.Ry = RyTemp.GetRange(first_index, MotionTracesSlicedSize);
                MotionTraces.Rz = RzTemp.GetRange(first_index, MotionTracesSlicedSize);
                MotionTraces.Size = MotionTracesSlicedSize;
                drawInputTrace();
                urServer.generateUrScript(UrSettings.TimeKinematics, UrSettings.TCP, UrSettings.PayLoad);
                await runMotionAsync();

                if (UrSettings.MotionPlay == false)
                {
                    return;
                }

                clearPlot("all");
                Console.WriteLine("next 10min");
            }
            MotionTraces.X = XTemp.GetRange(NbRestart * MaxLines, remainder);
            for (int k = 0; k < MotionTraces.X.Count; k++)
            {
                MotionTraces.t.Add(k * UrSettings.TimeKinematics);
            }
            MotionTraces.Y = YTemp.GetRange(NbRestart * MaxLines, remainder);
            MotionTraces.Z = ZTemp.GetRange(NbRestart * MaxLines, remainder);
            MotionTraces.Rx = RxTemp.GetRange(NbRestart * MaxLines, remainder);
            MotionTraces.Ry = RyTemp.GetRange(NbRestart * MaxLines, remainder);
            MotionTraces.Rz = RzTemp.GetRange(NbRestart * MaxLines, remainder);
            MotionTraces.Size = remainder;
            drawInputTrace();
            boolExceedMemory = false;
            urServer.generateUrScript(UrSettings.TimeKinematics, UrSettings.TCP, UrSettings.PayLoad);
            runMotion();

        }

        private void flatButton_PlayStopMotion_Click(object sender, EventArgs e)
        {

            bool selectedPlay = true;
            if (_playstopmotionclicked == true)
            {
                if (Settings_tab.sixSelected)
                {
                    // Code here for playing 6DOF Motion
                    if (UrSettings.motionPlay == false)
                    {
                        isResumed6D = false;
                        clearPlot("output");
                        progressbar_Motion.Value = 0;
                        lblProgressVal.Text = progressbar_Motion.Value.ToString();
                        // flatButton_PlayStopMotion.Iconimage = Image.FromFile(iconPath + "stop.png");
                        flatButton_PlayStopMotion.Text = " Stop Motion";
                        UrSettings.motionPlay = true;
                        flatButton_Home.Enabled = false;
                        flatButton_SetStartPos.Enabled = false;

                        Array.Clear(poseSpeedArray, 0, poseSpeedArray.Length);
                        Array.Clear(poseArray, 0, poseArray.Length);

                        if (MotionTraces.Size == 1)
                        {
                            //progressbar_Motion.MaxValue = 1;
                            progressbar_Motion.Maximum = 1;
                        }
                        else
                        {
                            // progressbar_Motion.MaxValue = (int)(MotionTraces.Size * UrSettings.timeKinematics);
                            progressbar_Motion.Maximum = 100;
                        }

                        if (boolExceedMemory)  // If the lines are greater than the capacity limit -> restart the motion every 5 min
                        {
                            clearPlot("all");
                            Console.WriteLine("memory");
                            RestartingProcess6D().ContinueWith(task =>
                            {
                                if (task.IsFaulted)
                                {

                                    Console.WriteLine("An error occurred: " + task.Exception?.Message);
                                }
                                else
                                {
                                    Console.WriteLine("All motions have been processed.");
                                }
                            });
                            return;
                        }
                        else if (!boolExceedMemory)
                        {
                            Console.WriteLine("not exceeding the memory");
                            urServer.generateUrScript(UrSettings.TimeKinematics, UrSettings.TCP, UrSettings.PayLoad);

                            runMotion();
                            return;
                        }


                    }

                    if (UrSettings.motionPlay == true)
                    {
                        resetRun();
                        flatButton_ResumeMotions.Enabled = true;


                        return;
                    }
                }

                else if (Settings_tab.oneSelected)
                {
                    if (motionPlay1D == false)
                    {

                        // Code here for playing 1DOF motion
                        flatButton_PlayStopMotion.Text = " Stop Motion";
                        motionPlay1D = true;
                        selectedPlay = false;
                        flatButton_Home.Enabled = false;
                        flatButton_SetStartPos.Enabled = false;
                        AllocConsole();
                        /* Thread run1D = new Thread(new ThreadStart(run1DRobot));*/
                        run1DRobot();
                        return;
                    }

                    if (motionPlay1D == true)
                    {
                        Stop1DPlatform();

                        flatButton_ResumeMotions.Enabled = true;
                        return;
                    }

                }

                else if (Settings_tab.bothSelected)
                {
                    if (UrSettings.motionPlay == false && motionPlay1D == false)
                    {
                        AllocConsole();
                        /*clearPlot("output");*/
                        progressbar_Motion.Value = 0;
                        lblProgressVal.Text = progressbar_Motion.Value.ToString();
                        // flatButton_PlayStopMotion.Iconimage = Image.FromFile(iconPath + "stop.png");
                        flatButton_PlayStopMotion.Text = " Stop Motion";
                        UrSettings.motionPlay = true;
                        motionPlay1D = true;
                        flatButton_Home.Enabled = false;
                        flatButton_SetStartPos.Enabled = false;
                        flatButton_ResumeMotions.Enabled = false;

                        Array.Clear(poseSpeedArray, 0, poseSpeedArray.Length);
                        Array.Clear(poseArray, 0, poseArray.Length);

                        if (MotionTraces.Size == 1)
                        {
                            //progressbar_Motion.MaxValue = 1;
                            progressbar_Motion.Maximum = 1;
                        }
                        else
                        {
                            // progressbar_Motion.MaxValue = (int)(MotionTraces.Size * UrSettings.timeKinematics);
                            progressbar_Motion.Maximum = 100;
                        }

                        urServer.generateUrScript(UrSettings.TimeKinematics, UrSettings.TCP, UrSettings.PayLoad);
                        runMotion();

                        //// Commented out this function in order to disable the Send Motion Function                 


                        run1DRobot();

                        return;
                    }

                    if (UrSettings.motionPlay == true && motionPlay1D == true)
                    {

                        flatButton_ResumeMotions.Enabled = true;
                        Stop1DPlatform();
                        resetRun();
                        return;
                    }
                }

            }
        }
        // Function that stopps the 1D platform
        public void Stop1DPlatform()
        {
            if (serialPort != null)
            {
                flatButton_PlayStopMotion.Text = " Play Motion";
                motionPlay1D = false;
                flatButton_SetStartPos.Enabled = false;
                flatButton_Home.Enabled = true;
                flatButton_LoadTraces.Enabled = false;
                serialPort.WriteLine("Stop");
                Console.WriteLine("Stop");
                Logger.addToLogFile("Stop 1D motion");
            }
            else if (serialPort == null)
            {
                System.Windows.MessageBox.Show($"Arduino was disconnected! Please reboot the program and establish serial port connection!");
                if (UrSettings.motionPlay == true)
                {
                    _keepMonitoring = false;
                    urServer.stopRobot();

                    UpdateStatusBarMessage.ShowStatusMessage(Environment.NewLine);
                    UrSettings.motionPlay = false;
                }
            }
        }

        // Function that begins thread to resume 1D robot
        private void Resume1DRobot()
        {


            if (_filePath1D != null)
            {
                Console.WriteLine("Resume trace");
                Logger.addToLogFile("Resume 1D motion line n° " + StopIndex + " (sliced input file)");
                Thread Resume1DMotion = new Thread(resumeMotion1D);

                Resume1DMotion.Start();

            }

            else if (_filePath1D == null)
            {
                System.Windows.MessageBox.Show("Please Load a Trace for the 1D Robot!");
                return;
            }
        }

        // Function that begins thread to run 1D robot
        private void run1DRobot()
        {
            flatButton_LoadTraces1D.Enabled = false;
            if (_filePath1D != null)
            {
                Console.WriteLine("Running 1D Trace...");
                Logger.addToLogFile("Start 1D motion");

                Thread Start1DMotion = new Thread(Start1D);
                Start1DMotion.Start();


                //flatButton_PlayStopMotion.Text = " Stop Motion";
                flatButton_LoadTraces1D.Enabled = true;
                flatButton_Home.Enabled = true;

            }
            else if (_filePath1D == null)
            {
                System.Windows.MessageBox.Show("Please Load a Trace for the 1D Robot!");
                return;
            }


        }

        // This function reads the file, shifts the displacement, calculates velocity and send calls the SendvoltageValues function
        public void Start1D()
        {
            List<double> displacementValues = ReadDisplacementValues(_filePath1D);
            List<double> newDisplacementValues = ShiftDisplacementToZero(displacementValues);

            List<double> velocityValues = CalculateVelocityfromDisplacement(newDisplacementValues);

            if (newDisplacementValues[0] > 0)
            {
                double startPos = newDisplacementValues[0];
                double speed2 = startPos / 200;
                double voltage2 = (speed2 + UrSettings.verticalMode[1]) / UrSettings.verticalMode[0];
                double voltage3 = (speed2 + UrSettings.horizontalMode[1]) / UrSettings.horizontalMode[0];
                //double timetoMove = ((startPos) / speed2) * 1000;
                //int delayAndTimetoMove = (int)(4600 - timetoMove - 50);
                //Thread.Sleep(delayAndTimetoMove);
                if (bool1DConfiguration)
                {
                    ShiftToStartPos(voltage2);
                }
                else
                {
                    ShiftToStartPos(voltage3);
                }

                //---------Couch tracking measurement
                //Thread.Sleep((int)timetoMove + 50);
                //AllocConsole();
                //if (serialPort.IsOpen == true)
                //{
                //    string voltageString2 = $"{0},{1},{200}";
                //    serialPort.WriteLine(voltageString2);
                //}
                //Thread.Sleep(30000);

                newDisplacementValues.RemoveAt(0);

                if (Settings_tab.bothSelected)
                {
                    //Thread.Sleep(4600);
                    eventSync1D6D.WaitOne(); //wait the first data received by the 6DoF controller to move the 1DoF platform
                }

                SendVoltageValues(velocityValues, newDisplacementValues);

                Console.WriteLine("dont start at 0");
            }
            else
            {
                if (Settings_tab.bothSelected)
                {
                    //Thread.Sleep(4600);
                    eventSync1D6D.WaitOne(); //wait the first data received by the 6DoF controller to move the 1DoF platform
                }
                SendVoltageValues(velocityValues, newDisplacementValues);
                Console.WriteLine("start at 0");
            }

        }

        // This function checks if the first position of the motion trace is non-zero, if it is, it will move the motor to that position before playing the motion
        public void ShiftToStartPos(double voltage2)
        {
            AllocConsole();
            if (serialPort.IsOpen == true)
            {
                string voltageString2 = $"{voltage2},{1},{200}";
                serialPort.WriteLine(voltageString2);
                serialPort.WriteLine($"{0},{1},{200}");
                return;
            }

        }
        // Function that re-calculates the input file; the first value of the list is the last position where the 1D stopped
        private void resumeMotion1D()
        {
            List<double> displacementValues = ReadDisplacementValues(_filePath1D);
            List<double> velocityValues = CalculateVelocityfromDisplacement(displacementValues);
            if (StopIndex < velocityValues.Count)
            {
                List<double> slicedVelocityValues = velocityValues.GetRange(StopIndex, velocityValues.Count - StopIndex);
                motionPlay1D = true;
                if (Settings_tab.bothSelected)
                {
                    Console.WriteLine("sync");
                    eventSync1D6D.WaitOne();
                }
                SendVoltageValues(slicedVelocityValues, displacementValues);
            }
        }
        // Function clears variables and resets values before another motion run performed
        private void resetRun()
        {
            // flatButton_PlayStopMotion.Iconimage = Image.FromFile(iconPath + "play.png");
            flatButton_PlayStopMotion.Text = " Play Motion";

            _keepMonitoring = false;
            urServer.stopRobot();
            Logger.addToLogFile("Stop 6D motion");

            UpdateStatusBarMessage.ShowStatusMessage(Environment.NewLine);
            UrSettings.motionPlay = false;
            flatButton_SetStartPos.Enabled = false;
            flatButton_Home.Enabled = true;
            flatButton_LoadTraces.Enabled = true;
        }

        /* This function runs the motion. MODBUS connection is setup and another thread is created to listen to the robot and stream data back into the software. TCP/IP connection is called from URServer class and 
         * motion traces are transformed form the CoordinateTransformtion class and send to the robot controller. */
        private void runMotion()
        {
            modbusClient = new ModbusClient(UrSettings.hostIPAddress, 502);

            try
            {
                modbusClient.Connect();
            }
            catch (Exception excep)
            {
                UpdateStatusBarMessage.ShowStatusMessage("MODBUS connection attempt via " + UrSettings.hostIPAddress + " failed.");
                Logger.addToLogFile("MODBUS connection attempt via " + UrSettings.hostIPAddress + " failed.");
            }

            if (modbusClient.Connected == true)
            {
                UpdateStatusBarMessage.ShowStatusMessage("MODBUS connection via " + UrSettings.hostIPAddress + " established.");
                Logger.addToLogFile("MODBUS connection via " + UrSettings.hostIPAddress + " established.");

                _keepMonitoring = true;

                Thread getDataFromControlerThread = new Thread(monitorData);
                getDataFromControlerThread.Start();

            }

            if (string.IsNullOrWhiteSpace(UrSettings.hostIPAddress))
            {
                UpdateStatusBarMessage.ShowStatusMessage("Error: No IP Address Selected");
                Logger.addToLogFile("Error: No IP Address Selected");
                return;
            }
            if (Index6DMonitoring == 0)
            {
                Logger.addToLogFile("Start 6D motion");
            }
            else
            {
                Logger.addToLogFile("Resume 6D motion time = " + Index6DMonitoring + "s (sliced input file)");
            }

            Thread tcpServerRunThread = new Thread(new ThreadStart(tcpServerRun));
            tcpServerRunThread.Start();

        }

        /* This function runs the motion. The definition of this function is based on the runMotion() function but enables the restarting motion after 10min for very long traces. It has to be an async task because we need to wait until the motion is over to run the next 10min motion without blocking the application */
        private async Task runMotionAsync()
        {
            modbusClient = new ModbusClient(UrSettings.hostIPAddress, 502);

            try
            {
                modbusClient.Connect();
                UpdateStatusBarMessage.ShowStatusMessage("MODBUS connection via " + UrSettings.hostIPAddress + " established.");
                Logger.addToLogFile("MODBUS connection via " + UrSettings.hostIPAddress + " established.");
            }
            catch (Exception excep)
            {
                UpdateStatusBarMessage.ShowStatusMessage("MODBUS connection attempt via " + UrSettings.hostIPAddress + " failed.");
                Logger.addToLogFile("MODBUS connection attempt via " + UrSettings.hostIPAddress + " failed.");
                return; // Quitter si la connexion échoue
            }

            if (string.IsNullOrWhiteSpace(UrSettings.hostIPAddress))
            {
                UpdateStatusBarMessage.ShowStatusMessage("Error: No IP Address Selected");
                Logger.addToLogFile("Error: No IP Address Selected");
                return;
            }

            _keepMonitoring = true;

            if (Index6DMonitoring == 0)
            {
                Logger.addToLogFile("Start 6D motion");
            }
            else
            {
                Logger.addToLogFile("Resume 6D motion time = " + Index6DMonitoring + "s (sliced input file)");
            }

            try
            {
                var getDataTask = Task.Run(() => monitorData());
                var tcpServerRunTask = Task.Run(() => tcpServerRun());

                // Await both tasks to complete
                Console.WriteLine("test");
                await Task.WhenAll(getDataTask, tcpServerRunTask);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                Logger.addToLogFile("An error occurred: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("test1"); // Cette ligne s'affiche lorsque toutes les tâches sont terminées
            }
        }




        //This event is initalize to sync the 1D and the 6D platforms. Once the code received the first data from the robot controller, the 1D is triggered to move. 
        static AutoResetEvent eventSync1D6D = new AutoResetEvent(false);


        //this function finds the new first position to reach when the user wants to start the 6D robot from where it stops (click on "Stop motion" during trace). 
        public static int findNext6DPosition()
        {
            int index = 0;
            Console.WriteLine(MotionTraces.t.Count());
            for (int i = 0; i < MotionTraces.Size; i++)
            {
                if ((Index6DMonitoring - MotionTraces.t[i]) < 0)
                {
                    Index6DTimeTravel = (int)(1000 * Math.Round(-(Index6DMonitoring - MotionTraces.t[i]), 2));
                    index = i;
                    break;
                }

                else
                {
                    index = -1;
                }
            }
            return index;
        }

        private void tcpServerRun()
        {
            urServer.sendUrScript(UrScriptProgram.urList);
            Console.WriteLine("TCP Over");

        }

        //Index6DMonitoring saves the last time value from the 6D robot feedback. 
        private static double Index6DMonitoring;
        private static int Index6DTimeTravel;
        private double Index6DMonitoringPlot;

        // This function is called in runMotion(), it is run on a separate thread and is responsible for connecting to MODBUS and streaming back data to the software to display and save in a txt file
        private void monitorData()
        {
            int falseCount = 0;
            int threshold = 800;
            int sampleRate = 10;// (int)(UrSettings.timeKinematics * 1000) - 10;
            double absoluteTime = 0;
            double currentTime = 0;
            double time = 0;
            double time_check_plot = 0;
            double time_check_display = 0;

            if (UrSettings.writeDataFile == true)
            {
                Logger.initialiseDataFile(_filePath6D);
            }

            while (_keepMonitoring)
            {
                ledBulb_Connected.Color = Color.LimeGreen;
                ledBulb_Connected.On = true;

                int[] readHoldingRegPose = { };
                int[] readHoldingRegTCPSpeed = { };
                int[] readHoldingRegTime = { };

                try
                {
                    modbusClient.WriteSingleRegister(2048, 0);
                    readHoldingRegPose = modbusClient.ReadHoldingRegisters(400, 6);
                    readHoldingRegTCPSpeed = modbusClient.ReadHoldingRegisters(410, 6);
                    readHoldingRegTime = modbusClient.ReadHoldingRegisters(2049, 5);
                }
                catch (IOException)
                {
                    UpdateStatusBarMessage.ShowStatusMessage("Error: Could not read or write data from UR controler");
                    Logger.addToLogFile("Error: Could not read or write data from UR controler");
                }

                lock (monitorDataLock)
                {
                    _holdingRegPoseLock = readHoldingRegPose;
                    _holdingRegTCPSpeedLock = readHoldingRegTCPSpeed;
                    _holdingRegTime = readHoldingRegTime;
                }

                if (isMoving())
                {
                    if (_firstRun == true)
                    {
                        eventSync1D6D.Set();
                        absoluteTime = getTime(_holdingRegTime);
                        _firstRun = false;

                    }
                    currentTime = getTime(_holdingRegTime);
                    time = currentTime - absoluteTime;
                    if (!boolExceedMemory)
                    {
                        Index6DMonitoring = time;
                    }
                }

                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        lock (monitorDataLock)
                        {
                            //UPDATE UI (PLOT DATA)                            
                            double poseValue = 0.0;
                            double poseSpeed = 0.0;

                            for (int i = 0; i < _holdingRegPoseLock.Length; i++)
                            {
                                if (i < 3)
                                {
                                    poseValue = convert16BitToDouble(_holdingRegPoseLock[i], true);
                                }
                                else
                                {
                                    poseValue = convert16BitToDouble(_holdingRegPoseLock[i], false);
                                }

                                poseSpeed = convert16BitToDouble(_holdingRegTCPSpeedLock[i], true);

                                poseArray[i] = poseValue;
                                poseSpeedArray[i] = poseSpeed;
                            }

                            if (isMoving())
                            {
                                if (boolExceedMemory)
                                {
                                    falseCount = 0;
                                }

                                if (isResumed6D)
                                {
                                    Thread.Sleep(Index6DTimeTravel);
                                }
                                isResumed6D = false;
                                //Matrix absPose = coordTransform.getAbsoultePose(poseArray, MotionTraces.startingPose);
                                Matrix<double> absPose = coordTransform.getAbsoultePose(poseArray, MotionTraces.startingPose);
                                double[] sixParamArray = coordTransform.getSixParameterPose(absPose);

                                if (UrSettings.writeDataFile == true)
                                {
                                    Logger.saveDataToFile(time, sixParamArray[0], sixParamArray[1], sixParamArray[2], sixParamArray[3], sixParamArray[4], sixParamArray[5]);
                                }

                                if (time == 0 || time > time_check_plot + 0.1)
                                {
                                    time_check_plot = time;
                                    plotData(time, sixParamArray[0], sixParamArray[1], sixParamArray[2], sixParamArray[3], sixParamArray[4], sixParamArray[5]);

                                }

                                //To make that the robot does not go above 5 mm limit from its maximum/minimum magnitude.
                                double maxx = 0;
                                double maxy = 0;
                                double maxz = 0;

                                for (int i = 1; i < MotionTraces.Size; i++)
                                {
                                    if (Math.Abs(MotionTraces.X[i]) > Math.Abs(maxx))
                                    {
                                        maxx = Math.Abs(MotionTraces.X[i]);
                                    }

                                    if (Math.Abs(MotionTraces.Y[i]) > Math.Abs(maxy))
                                    {
                                        maxy = Math.Abs(MotionTraces.Y[i]);
                                    }

                                    if (Math.Abs(MotionTraces.Z[i]) > Math.Abs(maxz))
                                    {
                                        maxz = Math.Abs(MotionTraces.Z[i]);
                                    }

                                }

                                if ((sixParamArray[0] > ((maxx * 10000) + 5)) || (sixParamArray[0] < -((maxx * 10000) + 5)) || (sixParamArray[1] > ((maxy * 1000) + 5)) || (sixParamArray[1] < -((maxy * 1000) + 5)) || (sixParamArray[2] > ((maxz * 1000) + 5)) || (sixParamArray[2] < -((maxz * 1000) + 5)))
                                {
                                    UpdateStatusBarMessage.ShowStatusMessage("Safety stop: The motion has exceeded 5 mm more than the maximum input motion");
                                    Logger.addToLogFile("Safety stop: The motion has exceeded 5 mm more than the maximum input motion");
                                    System.Windows.MessageBox.Show("Safety stop: The motion has exceeded 5 mm more than the maximum input motion");
                                    return;

                                }


                                if (time == 0 || time > time_check_display + 0.5)
                                {
                                    time_check_display = time;
                                    displayValues(sixParamArray);
                                }

                                int _t = (int)Math.Floor(time);

                                //if (_t <= progressbar_Motion.MaxValue)
                                //{
                                //    progressbar_Motion.Value = _t;
                                //}
                                int progressbar_max = (int)(MotionTraces.Size * UrSettings.timeKinematics);

                                double _test = ((time) / ((double)progressbar_max) * 100.0);
                                int _test1 = Convert.ToInt32(_test);
                                //if (_test1 <= progressbar_Motion.Maximum)

                                if (_test1 <= 100)
                                {
                                    updateRange6D(time);
                                    //changing _t to _test
                                    progressbar_Motion.Value = _test1;
                                    progresstext_Motion.Text = progressbar_Motion.Value.ToString() + "%";

                                }

                            }
                            else
                            {
                                if (boolExceedMemory)
                                {
                                    falseCount++;

                                }
                            }
                            if (falseCount >= threshold & UrSettings.motionPlay == true)
                            {
                                _keepMonitoring = false;
                                Console.WriteLine("monitor over");
                                Logger.addToLogFile("6D trace is complete");
                            }

                        }

                    });
                }
                else
                {

                    lock (monitorDataLock)
                    {

                    }
                }

                /*if (isMoving() == false && _firstRun == false) 
                {
                    if (time != 0)
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                 resetRun();
                                 return;
                            });
                        }
                    }                 
                }*/

                if (isMoving() == true && _firstRun == false)
                {
                    Thread.Sleep(sampleRate);
                }
                //if (progressbar_Motion.Value == 100)
                //{
                //    Logger.addToLogFile("6D trace is complete");
                //}

                //while (list_Progress.Count>list_Progress_Count)
                //{
                //    list_Progress_Count = list_Progress.Count;
                //}
                //Console.WriteLine("egale");
            }

            disconnectModbus();
            ledBulb_Connected.On = false;
        }

        private void updateRange6D(double time)
        {
            if (rangeBar.Value > time)
            {
                cartesianChart_translation.AxisX[0].MaxValue = time;
                cartesianChart_translation.AxisX[0].MinValue = 0;
                cartesianChart_rotation.AxisX[0].MaxValue = time;
                cartesianChart_rotation.AxisX[0].MinValue = 0;
            }
            else
            {
                cartesianChart_translation.AxisX[0].MaxValue = time;
                cartesianChart_translation.AxisX[0].MinValue = time - rangeBar.Value;
                cartesianChart_rotation.AxisX[0].MaxValue = time;
                cartesianChart_rotation.AxisX[0].MinValue = time - rangeBar.Value;
            }
        }

        // Function disconnects the MODBUS connection and resets values.
        private void disconnectModbus()
        {
            if (modbusClient != null)
            {
                modbusClient.Disconnect();

                if (modbusClient.Connected == false)
                {
                    _keepMonitoring = false;
                    UpdateStatusBarMessage.ShowStatusMessage("MODBUS disconected from controler");
                    Logger.addToLogFile("MODBUS disconected from controler");

                    if (UrSettings.writeDataFile == true)
                    {
                        Logger.closeSaveDataToFile();
                    }
                }

                modbusClient = null;
                _firstRun = true;
            }
        }

        // This function checks if the robot is moving and returns true or false

        private bool isMoving()
        {
            if (poseSpeedArray[0] != 0 || poseSpeedArray[1] != 0 || poseSpeedArray[2] != 0 || poseSpeedArray[3] != 0 || poseSpeedArray[4] != 0 || poseSpeedArray[5] != 0)
            {
                return true;
            }

            return false;
        }

        // Function takes an integer array from the holding registers from MODBUS about the time and converts it into a double value seconds

        private double getTime(int[] readHoldingRegTime)
        {
            double miliSec = (double)readHoldingRegTime[0] / 1000;
            int sec = readHoldingRegTime[1];
            int min = readHoldingRegTime[2] * 60;
            int hou = readHoldingRegTime[3] * 60 * 60;
            int day = readHoldingRegTime[4] * 24 * 60 * 60;

            double time = day + hou + min + sec + miliSec;

            return time;
        }

        // Function is used to comvert the MODBUS output in 16bit integer format to a double value

        private double convert16BitToDouble(int value, bool baseTenConvertion)
        {
            double _tempValue = 0.0;

            if (baseTenConvertion == true)
            {
                _tempValue = (double)value / 10;
            }
            if (baseTenConvertion == false)
            {
                _tempValue = (double)value / 1000;
            }

            return _tempValue;
        }

        private void displayValues(double[] positionValues)
        {

            string LR = Convert.ToString(Math.Round(positionValues[0], 2));
            string SI = Convert.ToString(Math.Round(positionValues[1], 2));
            string AP = Convert.ToString(Math.Round(positionValues[2], 2));

            string rLR = Convert.ToString(Math.Round(positionValues[3], 2));
            string rSI = Convert.ToString(Math.Round(positionValues[4], 2));
            string rAP = Convert.ToString(Math.Round(positionValues[5], 2));

            txtBox_LR.Text = LR;
            txtBox_SI.Text = SI;
            txtBox_AP.Text = AP;

            txtBox_rLR.Text = rLR;
            txtBox_rSI.Text = rSI;
            txtBox_rAP.Text = rAP;
        }

        // This function runs on a separate thread, it sets up a MODBUS connection to the robot controller to get starting pose of the robot. Before doing so it will set the tool center point (TCP) value (defult is [0 0 0 0 0 0] or based on the
        // values)
        private void threadSetStartPos()
        {
            if (UrScriptProgram.urList.Count != 0)
            {
                UrScriptProgram.urList.Clear();
            }

            UrScriptProgram.urList.Add("def tcpSet():");
            UrScriptProgram.urList.Add(urServer.setTPC(UrSettings.tcp));
            UrScriptProgram.urList.Add("end");
            urServer.sendUrScript(UrScriptProgram.urList);

            modbusClient = new ModbusClient(UrSettings.hostIPAddress, 502);

            try
            {
                modbusClient.Connect();
            }
            catch (Exception excep)
            {
                UpdateStatusBarMessage.ShowStatusMessage("MODBUS connection attempt via " + UrSettings.hostIPAddress + " failed.");
                Logger.addToLogFile("MODBUS connection attempt via " + UrSettings.hostIPAddress + " failed.");
            }

            if (modbusClient.Connected == true)
            {

                int[] readHoldingRegPose = { };
                double poseValue = 0.0;

                try
                {
                    readHoldingRegPose = modbusClient.ReadHoldingRegisters(400, 6);
                }
                catch (IOException)
                {
                    UpdateStatusBarMessage.ShowStatusMessage("Error: Could not read or write data from UR controler");
                    Logger.addToLogFile("Error: Could not read or write data from UR controler");
                }

                for (int i = 0; i < readHoldingRegPose.Length; i++)
                {
                    if (i < 3)
                    {
                        poseValue = convert16BitToDouble(readHoldingRegPose[i], true);
                        MotionTraces.startingPose[i] = poseValue;
                    }
                    else
                    {
                        poseValue = convert16BitToDouble(readHoldingRegPose[i], false);
                        MotionTraces.startingPose[i] = poseValue;
                    }
                }
                UpdateStatusBarMessage.ShowStatusMessage("Start Position Set To:  " + "x = " + MotionTraces.startingPose[0].ToString() + "     y = " + MotionTraces.startingPose[1].ToString() + "     z = " + MotionTraces.startingPose[2].ToString() + "     rx = " + MotionTraces.startingPose[3].ToString() + "     ry = " + MotionTraces.startingPose[4].ToString() + "     rz = " + MotionTraces.startingPose[5].ToString());
                Logger.addToLogFile("Start Position Set To:  " + MotionTraces.startingPose[0].ToString() + " " + MotionTraces.startingPose[1].ToString() + " " + MotionTraces.startingPose[2].ToString() + " " + MotionTraces.startingPose[3].ToString() + " " + MotionTraces.startingPose[4].ToString() + " " + MotionTraces.startingPose[5].ToString());

                Invoke(new Action(() =>
                {
                    flatButton_Home.Enabled = true;
                    flatButton_LoadTraces.Enabled = true;
                }));

            }
            modbusClient.Disconnect();
            UrScriptProgram.urList.Clear();
        }


        // Sets the start position --> runs threadSetStartPos
        private void flatButton_SetStartPos_Click(object sender, EventArgs e)
        {
            AllocConsole();

            // Update visibility of buttons according to selected robot
            UpdateButtons();

            // If 6DOF is selected, connect to 6DOF robot only
            if (Settings_tab.sixSelected)
            {
                Thread setStartPos = new Thread(threadSetStartPos);
                setStartPos.Start();
            }
            // If 1D is selected, connect to the 1D robot only
            if (Settings_tab.oneSelected)
            {
                threadConnectArduino();
            }
            // If Both is selected, connect to both robots
            if (Settings_tab.bothSelected)
            {
                Thread setStartPos = new Thread(threadSetStartPos);
                setStartPos.Start();

                threadConnectArduino();
            }
        }

        bool isConnectedAlready = false;
        private void threadConnectArduino()
        {
            // Connect to Arduino -- Establish Serial Connection
            foreach (var portName in SerialPort.GetPortNames())
            {
                string portInput = portName;
                serialPort = new SerialPort(portInput, 115200);
                if (serialPort.IsOpen == true && serialPort != null && isConnectedAlready == true)
                {
                    System.Windows.MessageBox.Show("Already Connected to Arduino!");
                    Logger.addToLogFile("1D Robot Couldnt Connect: Already Connected to Arduino");
                    UpdateStatusBarMessage.ShowStatusMessage("1D Robot Couldnt Connect: Already Connected to Arduino");
                    return;
                }
                else if (serialPort.IsOpen == false && serialPort != null)
                {
                    try
                    {
                        //Establish Serial Connection to Arduino
                        serialPort.Open();
                        arduinoConnect.Text = $"Arduino: Connected ({portInput}) ";
                        ledBulb1.On = true;
                        flatButton_SetStartPos.Enabled = false;
                        // Disable once connected
                        Console.WriteLine("Serial port {portInput} opened successfully.");
                        Logger.addToLogFile("1D Robot Connected: Serial port ({portInput}) opened successfully.");
                        UpdateStatusBarMessage.ShowStatusMessage("1D Robot Connected: Serial port ({portInput}) opened successfully");
                        flatButton_LoadTraces1D.Enabled = true;
                        flatButton_Home.Enabled = true;
                        isConnectedAlready = true;
                    }
                    catch
                    {
                        arduinoConnect.Text = $"Arduino: Failed to Connect! ";
                        Logger.addToLogFile("1D Robot Couldnt Connect: Arduino: Failed to Connect");
                        UpdateStatusBarMessage.ShowStatusMessage("1D Robot Couldnt Connect: Arduino: Failed to Connect");

                        return;
                    }
                }
                else if (serialPort == null)
                {
                    System.Windows.MessageBox.Show("Arduino not Connected!");
                    UpdateStatusBarMessage.ShowStatusMessage("Arduino not Connected!");
                    flatButton_Home.Enabled = false;
                    return;
                }

            }
        }

        // Moves the robot to the home position --> The home position is the start position set by the function "threadSetStartPos"
        private void flatButton_Home_Click(object sender, EventArgs e)
        {
            _homebuttonclicked = true;
            // Check if 1D is selected in settings or both
            if (Settings_tab.oneSelected)
            {
                // Home for 1D Platform
                Home1DPlatform();
            }

            // Check if 6DOF is selected in settings
            if (Settings_tab.sixSelected)
            {
                // Home 6DOF 
                if (UrScriptProgram.home.Count() != 0)
                {
                    UrScriptProgram.home.Clear();
                }

                UrScriptProgram.home.Add("def goHome():");
                UrScriptProgram.home.Add("movej(p[" + (MotionTraces.startingPose[0] / 1000).ToString() + ", " + (MotionTraces.startingPose[1] / 1000).ToString() + ", " + (MotionTraces.startingPose[2] / 1000).ToString() + ", " + MotionTraces.startingPose[3].ToString() + ", " + MotionTraces.startingPose[4].ToString() + ", " + MotionTraces.startingPose[5].ToString() + "]" + ", " + "a = " + "0.2" + ", " + "v = " + "0.2" + ")");
                UrScriptProgram.home.Add("end");

                urServer.sendUrScript(UrScriptProgram.home);
                Logger.addToLogFile("6D Robot to home");
            }
            //Check if Both is selected in settings
            if (Settings_tab.bothSelected)
            {
                // Home 1D Platform
                Home1DPlatform();

                // Home 6DOF 
                if (UrScriptProgram.home.Count() != 0)
                {
                    UrScriptProgram.home.Clear();
                }

                UrScriptProgram.home.Add("def goHome():");
                UrScriptProgram.home.Add("movej(p[" + (MotionTraces.startingPose[0] / 1000).ToString() + ", " + (MotionTraces.startingPose[1] / 1000).ToString() + ", " + (MotionTraces.startingPose[2] / 1000).ToString() + ", " + MotionTraces.startingPose[3].ToString() + ", " + MotionTraces.startingPose[4].ToString() + ", " + MotionTraces.startingPose[5].ToString() + "]" + ", " + "a = " + "0.2" + ", " + "v = " + "0.2" + ")");
                UrScriptProgram.home.Add("end");
                urServer.sendUrScript(UrScriptProgram.home);
                Logger.addToLogFile("6D Robot to home");
            }
        }

        private void txtBox_LR_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cartesianChart_rotation_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void MotionControl_tab_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        ////////////////////////////////////////////////////// 1D Robot Integration CODE HERE///////////////////////////////////////////////////////////////////////////////////////


        private void flatButton_LoadTraces1D_Click(object sender, EventArgs e)
        {

            // Initialise a new Open File Dialog, this will open a window for the user to select a .txt trace file
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Text Documents|*.txt", ValidateNames = true, Multiselect = false })
            {
                if (Settings_tab.oneSelected)
                {
                    // Clear the current plot in case a new file has been loaded

                    clearPlot("all");
                }



                if (ofd.ShowDialog() == DialogResult.OK)
                {

                    // Assign the file path name to a string variable _filePath

                    // Initiate a integer columnNum this will use the getColNumber function to check how many colum are in the trace file
                    int columnNum = getColNumber(ofd);

                    // For 1D motion traces there should only be two columns, Time and Displacement. If columnNum = 2 then continue to read the file
                    if (columnNum == 2)
                    {
                        _filePath1D = ofd.FileName;
                        textBox_filename.Text = System.IO.Path.GetFileName(ofd.FileName);
                        if (Settings_tab.oneSelected)
                        {
                            Logger.RenameLogFile("1D", textBox_filename.Text);
                            Logger.addToLogFile("The 1D input file " + textBox_filename.Text + " has been selected");


                        }

                        if (Settings_tab.bothSelected)
                        {
                            _filePath1D = ofd.FileName;
                            Console.WriteLine(_filePath1D);
                            textBox_filename.Text = System.IO.Path.GetFileName(ofd.FileName);
                            if (Logger.LogFileNameBOTHBool)
                            {
                                Logger.addToLogFile("The 1D input file " + textBox_filename.Text + " has been selected - BOTH PLATFORMS");
                            }
                            else
                            {
                                Logger.RenameLogFileBOTH("BOTH", textBox_filename.Text);
                                Logger.addToLogFile("The 1D input file " + textBox_filename.Text + " has been selected - BOTH PLATFORMS");

                            }

                        }


                        // Display the file path in the text box - visual indicator

                        // Read the displacement values
                        List<double> displacementValues = ReadDisplacementValues(_filePath1D);

                        // Shift the displacement values by the minimum value to ensure the new minimum is now 0
                        List<double> newdisplacementValues = ShiftDisplacementToZero(displacementValues);
                        // Plot the input trace
                        drawInputTrace1D(newdisplacementValues);


                        // Update button availability
                        if (_filePath1D != null)
                        {
                            flatButton_LoadTraces1D.Enabled = true;
                            flatButton_PlayStopMotion.Enabled = true;
                            _playstopmotionclicked = true;
                        }
                    }

                    // If the no. of columns are less or greater than 2, then the file is not compatible and the user should reselect.
                    if (columnNum > 2 || columnNum < 2)
                    {
                        UpdateStatusBarMessage.ShowStatusMessage("Error: Invalid format, format needed: [t  Y]");
                        Logger.addToLogFile("Error: Invalid format, format needed: [t  Y]");
                        System.Windows.MessageBox.Show("Error: Invalidd input file format, format needed: [t  Y]");
                    }
                }
            }
        }

        public string FilePath1D
        {
            get { return _filePath1D; }
        }

        public string FilePath6D
        {
            get { return _filePath6D; }
        }

        private string _filePath6D;
        private string _filePath1D;

        // This function will read the .txt file and parse all the displacment values
        static List<double> ReadDisplacementValues(string _filePath1D)
        {
            List<double> displacementValues = new List<double>();

            try
            {
                // Read all lines from the file
                string[] lines = File.ReadAllLines(_filePath1D);

                // Process each line
                foreach (string line in lines)
                {
                    // Split the line by comma
                    string[] values = line.Split(' ');

                    // Ensure there are two values in the line
                    if (values.Length == 2)
                    {
                        // Parse only the displacement value
                        if (double.TryParse(values[1], out double displacement))
                        {
                            // Add the displacement value to the list
                            displacementValues.Add(displacement);
                        }
                        else
                        {
                            Console.WriteLine("Error parsing displacement.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid data format in line: " + line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            // Return the list of displacement values
            return displacementValues;
        }

        // This function is responsible for sending the final voltage value, up bool and delay value to the arduino in order to move the motor. Takes velocity as an input
        public void SendVoltageValues(List<double> velocityValues, List<double> displacementValues)
        {

            AllocConsole();
            DateTime previous = DateTime.Now;
            DateTime previous2 = DateTime.Now;
            DateTime playmotionTime = DateTime.Now;

            Logger.Timestamps1DFullPath = "Output Files/1DPlatform/Timestamps/" + Path.GetFileName(_filePath1D) + "_" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
            Logger.addToTimestamps1DFile(Path.GetFileName(_filePath1D) + '\n' + "1D starting time : " + playmotionTime.ToString("HH:mm:ss:fff"));
            Logger.ArduinoSendFullPath = "Output Files/1DPlatform/Arduino/Send/" + Path.GetFileName(_filePath1D) + "_" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
            Logger.ArduinoGetFullPath = "Output Files/1DPlatform/Arduino/Get/" + Path.GetFileName(_filePath1D) + "_" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";

            Stopwatch stopwatch = Stopwatch.StartNew();
            string data;

            for (int i = 0; i < velocityValues.Count; i++)
            {
                if (motionPlay1D == false)
                {
                    StopIndex = i;
                    return;
                }
                var timeThreshold = i * 200;
                while (stopwatch.ElapsedMilliseconds < timeThreshold)
                {
                    string tempdata = "";
                    tempdata = serialPort.ReadExisting();

                    if (tempdata.Contains("ack"))
                    {
                        tempdata = stopwatch.ElapsedMilliseconds.ToString() + ',' + tempdata;
                        Logger.addToArduinoGetFile(tempdata);
                    }
                    else
                    {
                        if (tempdata.Length >= 1)
                        {
                            string CurrentPosition = tempdata;
                            tempdata = stopwatch.ElapsedMilliseconds.ToString() + ',' + tempdata;
                            Console.WriteLine(CurrentPosition);
                            if (CurrentPosition.Contains("Pos"))
                                Logger.addToArduinoGetFile(CurrentPosition);
                        }
                    }

                }
                if (stopwatch.ElapsedMilliseconds >= timeThreshold)
                {
                    if (serialPort.IsOpen == true)
                    {
                        DateTime now = DateTime.Now;
                        DateTime now2 = DateTime.Now;
                        var result = CalculateVoltagePair(velocityValues[i]);
                        var delayResult = CalculateDelayValue(result.voltageValue, velocityValues[i]);
                        double newVoltage = (result.voltageValue < 45 && result.voltageValue != 0) ? 60 : result.voltageValue;
                        string voltageSend = newVoltage.ToString("0.0");
                        string delaySend = delayResult.ToString("0.0");
                        string voltageString = $"{voltageSend},{result.isGoingUp},{delayResult}";
                        Logger.addToArduinoSendFile($"{result.voltageValue},{result.isGoingUp},{delayResult}");
                        var teststring = voltageString + "," + displacementValues[i + 1];
                        serialPort.WriteLine(teststring);
                        Console.WriteLine("total time: " + stopwatch.ElapsedMilliseconds + " " + teststring);
                        currentTime += (now - previous).Milliseconds * 0.001;


                        if (serialPort != null || serialPort.IsOpen == true)
                        {
                        }
                        else
                        {
                            System.Windows.MessageBox.Show($"Arduino was disconnected! Please reboot the program and establish serial port connection!");
                            return;
                        }
                    }
                    else if (serialPort.IsOpen == false)
                    {
                        System.Windows.MessageBox.Show($"Arduino was disconnected! Please reboot the program and establish serial port connection!");
                        if (UrSettings.motionPlay == true)
                        {
                            _keepMonitoring = false;
                            urServer.stopRobot();


                            UpdateStatusBarMessage.ShowStatusMessage(Environment.NewLine);
                            UrSettings.motionPlay = false;
                        }

                        // Home the 6DOF robot so it remembers previous home location
                        if (UrScriptProgram.home.Count() != 0)
                        {
                            UrScriptProgram.home.Clear();
                        }

                        UrScriptProgram.home.Add("def goHome():");
                        UrScriptProgram.home.Add("movej(p[" + (MotionTraces.startingPose[0] / 1000).ToString() + ", " + (MotionTraces.startingPose[1] / 1000).ToString() + ", " + (MotionTraces.startingPose[2] / 1000).ToString() + ", " + MotionTraces.startingPose[3].ToString() + ", " + MotionTraces.startingPose[4].ToString() + ", " + MotionTraces.startingPose[5].ToString() + "]" + ", " + "a = " + "0.2" + ", " + "v = " + "0.2" + ")");
                        UrScriptProgram.home.Add("end");

                        urServer.sendUrScript(UrScriptProgram.home);

                        return;
                    }
                }
            }


            Stopwatch stopwatchLast = Stopwatch.StartNew();
            while (stopwatchLast.ElapsedMilliseconds < 300)
            {

                try
                {
                    string tempdata = serialPort.ReadExisting();
                    if (tempdata.Contains("ack"))
                    {
                        tempdata = stopwatch.ElapsedMilliseconds.ToString() + ',' + tempdata;
                        Console.WriteLine(tempdata);
                        Logger.addToArduinoGetFile(tempdata);
                    }
                }
                catch
                {
                    break;
                }
            }

            stopwatchLast.Stop();
            currentTime = 0;
            string finalString = $"{0},{false},{2000}";
            serialPort.WriteLine(finalString);
            DateTime endTime = DateTime.Now;
            Logger.addToTimestamps1DFile("1D end time : " + endTime.ToString("HH:mm:ss:fff") + '\n');
            System.Windows.MessageBox.Show("Trace is Complete");
            //motionPlay1D = false;
            Logger.addToLogFile("1D trace is complete");
        }


        // This function will send a "Home" string to the Arduino in order to move the motor back to base position
        public void Home1DPlatform()
        {
            if (serialPort != null || serialPort.IsOpen == true)
            {
                serialPort.WriteLine("Home");
                Logger.addToLogFile("1D Robot to Home");
                UpdateStatusBarMessage.ShowStatusMessage("1D Robot to Home");
            }
            else if (serialPort == null || serialPort.IsOpen == false)
            {
                System.Windows.MessageBox.Show($"Arduino was disconnected! Please reboot the program and establish serial port connection!");
                if (UrSettings.motionPlay == true)
                {
                    _keepMonitoring = false;
                    urServer.stopRobot();

                    UpdateStatusBarMessage.ShowStatusMessage(Environment.NewLine);
                    UrSettings.motionPlay = false;
                }
            }
        }

        // This function will calculate the necessary time to move the motor in case the voltage is too low to make the motor move. e.g. if the voltage to move the motor is less that ~50mV, the motor wont move as expected. This function simply adjusts the voltage to an amount that will guarantee movement but adjust the time needed to move to achieve the same displacement
        static double CalculateDelayValue(double voltage2, double speed)
        {
            double delayValue = 200;
            double displacement = Math.Abs(speed) * 0.2;
            double Voltage = 60;
            if (voltage2 > 45)
            {
                delayValue = 200;

            }
            else if (voltage2 < 45)
            {
                if (speed > 0)
                {
                    //delayValue = displacement / (Voltage * 0.2265 - 2.5726) * 1000;
                    if (bool1DConfiguration)
                    {
                        Console.WriteLine("vertical");
                        delayValue = displacement / (Voltage * UrSettings.verticalMode[2] - UrSettings.verticalMode[3]) * 1000;
                    }
                    else
                    {
                        Console.WriteLine("horizontal");
                        //going_up = 0.2482×voltages−3.8393
                        delayValue = displacement / (Voltage * UrSettings.horizontalMode[2] - UrSettings.horizontalMode[3]) * 1000;
                    }
                }

                if (speed == 0)
                {
                    delayValue = 200;
                }
                if (speed < 0)
                {
                    //delayValue = displacement / (Voltage * 0.2229 - 3.9812) * 1000;
                    if (bool1DConfiguration)
                    {
                        delayValue = displacement / (Voltage * UrSettings.verticalMode[0] + UrSettings.verticalMode[1]) * 1000;
                    }
                    else
                    {
                        //going_down = 0.2778×voltages−7.7643
                        delayValue = displacement / (Voltage * UrSettings.horizontalMode[0] + UrSettings.horizontalMode[1]) * 1000;
                    }

                }
            }
            if (delayValue > 200)
            {
                Console.WriteLine("delay:" + delayValue + " displacement:" + displacement + " speed: " + speed);
            }
            return delayValue;
        }

        // This function takes velocity as an input and returns a voltage and int (up or down)
        private static (double voltageValue, int isGoingUp) CalculateVoltagePair(double v1)
        {
            double voltageValue = 0;
            double speed = v1;
            int isGoingUp = 1;

            if (speed > 0)
            {
                if (bool1DConfiguration)
                {
                    voltageValue = (speed - UrSettings.verticalMode[1]) / UrSettings.verticalMode[0];
                }
                else
                {
                    //voltageValue = (speed + 7.7643) / 0.2778; 
                    voltageValue = (speed - UrSettings.horizontalMode[1]) / UrSettings.horizontalMode[0];


                }
                isGoingUp = 1;
            }
            if (speed < 0)
            {
                //voltageValue = (Math.Abs(speed) + 2.5726) / 0.2265;

                if (bool1DConfiguration)
                {
                    voltageValue = (Math.Abs(speed) - UrSettings.verticalMode[3]) / UrSettings.verticalMode[2];
                }
                else
                {
                    voltageValue = (Math.Abs(speed) - UrSettings.horizontalMode[3]) / UrSettings.horizontalMode[2];
                }
                isGoingUp = 0;
            }
            if (speed == 0)
            {
                voltageValue = 0;
            }

            return (voltageValue, isGoingUp);
        }

        // This function takes the displacements read and shifts the minimum value to 0
        static List<double> ShiftDisplacementToZero(List<double> displacementValues)
        {
            // Find the minimum value in the array
            double minValue = displacementValues.Min();

            // Create a new array with values shifted by the absolute value of the minimum value
            List<double> shiftedValues = displacementValues.Select(value => value - minValue).ToList();
            return shiftedValues;
        }

        public static void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            AllocConsole();
            string data = serialPort.ReadLine();
            Console.WriteLine($"Received from Arduino: {data}");
        }

        // This function takes a list of displacement values and converts them to a list of velocity values. This will be eventually be inputted into a velocity to voltage conversion file
        static List<double> CalculateVelocityfromDisplacement(List<double> displacementValues)
        {
            List<double> velocityValues = new List<double>();
            List<double> processedPosition = new List<double>();
            int increment = 1;
            //WARNING
            for (int i = 0; i < displacementValues.Count - 1; i += increment)
            {
                processedPosition.Add((displacementValues[i]));
            }
            for (int i = 0; i < processedPosition.Count - 1; i++)
            {
                velocityValues.Add((processedPosition[i + 1] - processedPosition[i]) / (increment * 0.2));
            }
            //string path = "C:\\Users\\Robot\\Documents\\Processed Traces\\trace.txt";
            //File.WriteAllLines(path, processedPosition.Select(d => d.ToString()));
            return velocityValues;
        }

        // This function updates the UI depending on the selected robot
        private void robotChanged(object sender, EventArgs e)
        {
            updateLabel();
        }

        private void updateLabel()
        {
            if (Settings_tab.bothSelected)
            {
                robotSelectLabel.Text = "Current Robot: Both";
                flatButton_LoadTraces.Visible = true;
                flatButton_LoadTraces1D.Visible = true;
            }
            if (Settings_tab.sixSelected)
            {
                robotSelectLabel.Text = "Current Robot: 6DOF";
                flatButton_LoadTraces1D.Visible = false;
            }
            if (Settings_tab.oneSelected)
            {
                robotSelectLabel.Text = "Current Robot: 1D";
                flatButton_LoadTraces.Visible = false;
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////END OF 1D INTEGRATION CODE//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // This class holds the settings for the robot and software interface, the class is accessed by calling URSettings. The settings.txt file can be modified and will automaticly take values from the file into the varaibles
        public static class UrSettings
        {
            public static string hostIPAddress = string.Empty;
            public static string motionType = string.Empty;
            public static double payLoadMass = 0.0;
            public static double[] tcp = { 0, 0, 0, 0, 0, 0 };
            public static double[] alingedPos = { 0, 0, 0, 0, 0, 0 };
            public static double timeKinematics = 0;
            public static bool writeLogFile = true;
            public static bool writeUrScriptFile = true;
            public static bool writeTimestamps1DFile = true;
            public static bool writeArduinoGetFile = true;
            public static bool writeArduinoSendFile = true;
            public static bool writeDataFile = true;
            public static bool motionPlay = false;
            public static bool isConnected = false;
            public static bool singularity = false;
            public static bool movementToLarge = false;
            public static double[] verticalMode = { 0, 0, 0, 0 };
            public static double[] horizontalMode = { 0, 0, 0, 0 };

            public static bool IsConnected
            {
                get { return isConnected; }
                set { isConnected = value; }
            }

            public static bool Singularity
            {
                get { return singularity; }
                set { singularity = value; }
            }

            public static bool MovementToLarge
            {
                get { return movementToLarge; }
                set { movementToLarge = value; }
            }

            public static bool MotionPlay
            {
                get { return motionPlay; }
                set { motionPlay = value; }
            }

            public static string IP
            {
                get { return hostIPAddress; }
                set { hostIPAddress = value; }
            }

            public static string MotionType
            {
                get { return motionType; }
                set { motionType = value; }
            }

            public static double PayLoad
            {
                get { return payLoadMass; }
                set { payLoadMass = value; }
            }

            public static double[] TCP
            {
                get { return tcp; }
                set { tcp = value; }
            }

            public static double[] AlingedPos
            {
                get { return alingedPos; }
                set { alingedPos = value; }
            }

            public static double[] VerticalMode
            {
                get { return verticalMode; }
                set { verticalMode = value; }
            }

            public static double[] HorizontalMode
            {
                get { return horizontalMode; }
                set { horizontalMode = value; }
            }

            public static double TimeKinematics
            {
                get { return timeKinematics; }
                set { timeKinematics = value; }
            }

            public static bool WriteLogFile
            {
                get { return writeLogFile; }
                set { writeLogFile = value; }
            }

            public static bool WriteTimestamp1DFile
            {
                get { return WriteTimestamp1DFile; }
                set { WriteTimestamp1DFile = value; }
            }

            public static bool WriteArduinoGetFile
            {
                get { return WriteArduinoGetFile; }
                set { WriteArduinoGetFile = value; }
            }
            public static bool WriteArduinoSendFile
            {
                get { return WriteArduinoSendFile; }
                set { WriteArduinoSendFile = value; }
            }

            public static bool WriteUrScriptFile
            {
                get { return writeUrScriptFile; }
                set { writeUrScriptFile = value; }
            }

            public static bool WriteDataFile
            {
                get { return writeDataFile; }
                set { writeDataFile = value; }
            }
        }

        // MotionTraces Class, can be called in any class to set and accsess 6D motion traces

        public static class MotionTraces
        {
            public static double[] startingPose = new double[6];

            public static List<double> t = new List<double>();

            public static List<double> X = new List<double>();
            public static List<double> Y = new List<double>();
            public static List<double> Z = new List<double>();

            public static List<double> Rx = new List<double>();
            public static List<double> Ry = new List<double>();
            public static List<double> Rz = new List<double>();

            public static int sizeValue = 0;

            public static int Size
            {
                get { return sizeValue; }
                set { sizeValue = value; }
            }

            public static IEnumerable<double> _T
            {
                get { return _T.AsEnumerable(); }
            }

            public static IEnumerable<double> _X
            {
                get { return _X.AsEnumerable(); }
            }

            public static IEnumerable<double> _Y
            {
                get { return _Y.AsEnumerable(); }
            }

            public static IEnumerable<double> _Z
            {
                get { return _Z.AsEnumerable(); }
            }

            public static IEnumerable<double> _Rx
            {
                get { return _Rx.AsEnumerable(); }
            }

            public static IEnumerable<double> _Ry
            {
                get { return _Ry.AsEnumerable(); }
            }

            public static IEnumerable<double> _Rz
            {
                get { return _Rz.AsEnumerable(); }
            }

            public static void AddValue_T(double item)
            {
                t.Add(item);
            }

            public static void AddValue_X(double item)
            {
                X.Add(item);
            }

            public static void AddValue_Y(double item)
            {
                Y.Add(item);
            }

            public static void AddValue_Z(double item)
            {
                Z.Add(item);
            }

            public static void AddValue_Rx(double item)
            {
                Rx.Add(item);
            }

            public static void AddValue_Ry(double item)
            {
                Ry.Add(item);
            }

            public static void AddValue_Rz(double item)
            {
                Rz.Add(item);
            }

            public static void setStartingPose()
            {
                for (int i = 0; i < 6; i++)
                {
                    startingPose[i] = 0;
                }
            }
        }



        // Class used to get data from MODBUS and plot data

        public class DataStream
        {
            public double X { get; set; }
            public double Y { get; set; }

            public System.DateTime DateTime { get; set; }
            public double Value { get; set; }
        }

        private void arduinoConnect_Click_(object sender, EventArgs e)
        {

        }

        private void cartesianChart_translation_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void cartesianChart_1D_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void ledBulb1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel2_Click(object sender, EventArgs e)
        {

        }

        private void progressbar_Motion_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            /*            int Range = 20;

                        if (int.TryParse(textBox1.Text, out Range))
                        {
                            chartRange = Range;
                        }*/
        }

        private void label7_Click_2(object sender, EventArgs e)
        {

        }

        private void rangeBar_Scroll(object sender, EventArgs e)
        {
            chartRange = rangeBar.Value;
            cartesianChart_translation.AxisX[0].MinValue = 0;
            cartesianChart_rotation.AxisX[0].MinValue = 0;
            cartesianChart_translation.AxisX[0].MaxValue = chartRange;
            cartesianChart_rotation.AxisX[0].MaxValue = chartRange;
            rangeNo.Text = rangeBar.Value.ToString();

        }

        private void resetZoom_Click(object sender, EventArgs e)
        {
            cartesianChart_translation.AxisX[0].MinValue = double.NaN;
            cartesianChart_rotation.AxisX[0].MinValue = double.NaN;
            cartesianChart_translation.AxisX[0].MaxValue = double.NaN;
            cartesianChart_rotation.AxisX[0].MaxValue = double.NaN;

            cartesianChart_translation.AxisY[0].MinValue = double.NaN;
            cartesianChart_rotation.AxisY[0].MinValue = double.NaN;
            cartesianChart_translation.AxisY[0].MaxValue = double.NaN;
            cartesianChart_rotation.AxisY[0].MaxValue = double.NaN;
        }

        private void toggle1D_Click(object sender, EventArgs e)
        {
            LineSeries inputAP = (cartesianChart_translation.Series[6] as LineSeries);
            LineSeries outputAP = (cartesianChart_translation.Series[7] as LineSeries);

            inputAP.Visibility = inputAP.Visibility == Visibility.Visible
                ? Visibility.Hidden
                : Visibility.Visible;
            outputAP.Visibility = outputAP.Visibility == Visibility.Visible
                ? Visibility.Hidden
                : Visibility.Visible;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toggle6D_Click(object sender, EventArgs e)
        {
            LineSeries inputLR = (cartesianChart_translation.Series[0] as LineSeries);
            LineSeries outputLR = (cartesianChart_translation.Series[3] as LineSeries);

            LineSeries inputSI = (cartesianChart_translation.Series[1] as LineSeries);
            LineSeries outputSI = (cartesianChart_translation.Series[4] as LineSeries);

            LineSeries inputAP = (cartesianChart_translation.Series[2] as LineSeries);
            LineSeries outputAP = (cartesianChart_translation.Series[5] as LineSeries);

            inputLR.Visibility = inputLR.Visibility == Visibility.Visible
                ? Visibility.Hidden
                : Visibility.Visible;
            outputLR.Visibility = outputLR.Visibility == Visibility.Visible
                ? Visibility.Hidden
                : Visibility.Visible;

            inputSI.Visibility = inputSI.Visibility == Visibility.Visible
            ? Visibility.Hidden
            : Visibility.Visible;
            outputSI.Visibility = outputSI.Visibility == Visibility.Visible
                ? Visibility.Hidden
                : Visibility.Visible;

            inputAP.Visibility = inputAP.Visibility == Visibility.Visible
            ? Visibility.Hidden
            : Visibility.Visible;
            outputAP.Visibility = outputAP.Visibility == Visibility.Visible
                ? Visibility.Hidden
                : Visibility.Visible;
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ledBulb1_Click_1(object sender, EventArgs e)
        {

        }

        private void moveToStartPos_Click(object sender, EventArgs e)
        {
        }

        private void rangeNo_Click(object sender, EventArgs e)
        {

        }
    }
}
