using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using EasyModbus;
using System.Reflection;
using static PhantomControl.MotionControl_tab;

namespace PhantomControl
{
    public partial class ApplicationGUI : Form
    {
        private string iconPath = "Resources\\Icons\\";

        URServer urServer = new URServer();
        
        Settings_tab settingsTab = new Settings_tab();

        public ApplicationGUI()
        {
            //Logger.initialiseLogger();
            InitializeComponent();


            motionControl_tab.Show();
            settings_tab.Hide();
            log_tab.Hide();

            if (!Settings_tab.bothSelected && !Settings_tab.oneSelected && !Settings_tab.sixSelected)
            {
                
                MessageBox.Show("Please Select Robot in the Settings Tab Before Continuing!");
            }
        }     

        private void flatButton_Close_Click(object sender, EventArgs e)
        {
            settingsTab.SaveSettings();
            Application.Exit();
        }

        private void flatButton_Minimise_Click(object sender, EventArgs e)
        {
            ApplicationGUI.ActiveForm.WindowState = FormWindowState.Minimized;
        }

        private void flatButton_motionControl_Click(object sender, EventArgs e)
        {
            label_heading.Text = "MOTION CONTROL";
            var ipath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources\\Icons\\gamepad_Black.png");
            //picContainer.Image = Image.FromFile(iconPath + "gamepad_Black.png");
            picContainer.Image = Image.FromFile(ipath);

            motionControl_tab.Show();
            settings_tab.Hide();
            log_tab.Hide();

            if (!Settings_tab.bothSelected && !Settings_tab.oneSelected && !Settings_tab.sixSelected)
            {
                MessageBox.Show("Please Select Robot in the Settings Tab Before Continuing!");
            }
        }

        private void flatButton_settings_Click(object sender, EventArgs e)
        {
            label_heading.Text = "SETTINGS";
            var ipath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources\\Icons\\settings_Black.png");
            //picContainer.Image = Image.FromFile(iconPath + "settings_Black.png");
            picContainer.Image = Image.FromFile(ipath);

            motionControl_tab.Hide();
            settings_tab.Show();
            log_tab.Hide();
  
        }

        private void flatButton_log_Click(object sender, EventArgs e)
        {
            label_heading.Text = "LOG";
            picContainer.Image = Image.FromFile(iconPath + "edit_Black.png");

            motionControl_tab.Hide();
            settings_tab.Hide();
            log_tab.Show();
        }

        private void flatButton_Resize_Click(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized
                ? FormWindowState.Normal
                : FormWindowState.Maximized;
        }

        private void picContainer_Click(object sender, EventArgs e)
        {

        }

        private void label_heading_Click(object sender, EventArgs e)
        {

        }

        private void log_tab_Load(object sender, EventArgs e)
        {

        }

        private void ApplicationGUI_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/main/README.md");
        }
    }

    public static class Logger
    {
        private static string logFilename = "/Log" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
        private static string logFilePath = "Output Files/Log/Log" + DateTime.Now.ToString("ddMMyy-hhmm");
        private static string logFullPath = string.Empty;

        private static string urScriptName = "/urScript" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
        private static string urScriptPath = "Output Files/GeneratedUrScripts";
        public static string urScriptFullPath = string.Empty;

        public static bool LogFileNameBOTHBool = false;

        private static string Timestamps1DFilename = "/timestamps1D" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
        private static string Timestamps1DPath = "Output Files/1DPlatform/Timestamps";
        public static string Timestamps1DFullPath = string.Empty;

        private static string ArduinoGetFilename = "/Get1D" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
        private static string ArduinoGetFilePath = "Output Files/1DPlatform/Arduino/Get";
        public static string ArduinoGetFullPath = string.Empty;

        private static string ArduinoSendFilename = "/Send1D" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
        private static string ArduinoSendFilePath = "Output Files/1DPlatform/Arduino/Send";
        public static string ArduinoSendFullPath = string.Empty;

        public static TextWriter wrireFile;
        // NEW LOG FILE
        // new log file is run everytime this class is called
        public static void initialiseLogger()
        {
            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
            }

            if (!Directory.Exists(urScriptPath))
            {
                Directory.CreateDirectory(urScriptPath);
            }

            if (!Directory.Exists(Timestamps1DPath))
            {
                Directory.CreateDirectory(Timestamps1DPath);
            }

            if (!Directory.Exists(ArduinoGetFilePath))
            {
                Directory.CreateDirectory(ArduinoGetFilePath);
            }
            

            if (!Directory.Exists(ArduinoSendFilePath))
            {
                Directory.CreateDirectory(ArduinoSendFilePath);
            }

            logFullPath = logFilePath + logFilename;
            urScriptFullPath = urScriptPath + urScriptName;
            Timestamps1DFullPath = Timestamps1DPath + Timestamps1DFilename;
            ArduinoGetFullPath = ArduinoGetFilePath + ArduinoGetFilename;
            ArduinoSendFullPath = ArduinoSendFilePath + ArduinoSendFilename;
        }

        //Create a new log file for 6DoF OR 1DoF platform
        private static void CreateNewLogFile()
        {
            logFilename = "/Log" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
            logFullPath = logFilePath + logFilename;
        }

        //Create a new log file for the two platforms simultaneously
        private static void CreateNewLogFile(string Robot, string filename)
        {
            logFilename = "/Log_" + Robot + "_" + filename + "_" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
            logFullPath = logFilePath + logFilename;
        }

        public static void RenameLogFileBOTH(string Robot, string filename)
        {

            try
            {

                if (File.Exists(logFullPath))
                {
                    CreateNewLogFile(Robot, filename);
                }
                
                LogFileNameBOTHBool = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not rename the log file: " + ex.Message);
            }
        }

        public static void RenameLogFile(string Robot, string filename)
        {
            try
            {
                if (File.Exists(logFullPath))
                {
                    CreateNewLogFile(Robot, filename);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not rename the log file: " + ex.Message);
            }
        }


        public static void initialiseDataFile(string filename)
        {
            string filename2 = filename.Substring(0, filename.Length - 4);
            wrireFile = new StreamWriter("Output Files/6DPlatform/_" + filename2 + "_" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt");
            TextWriter.Synchronized(wrireFile).WriteLine("Time(s)" + " " + "x(mm)" + " " + "y(mm)" + " " + "z(mm)" + " " + "rx(deg)" + " " + "ry(deg)" + " " + "rz(deg)");
        }

        // ADD SOMETHING TO LOG

    public static void addToLogFile(string text)
        {
            if (UrSettings.writeLogFile == true)
            {
                using (FileStream logFile = new FileStream(logFullPath, FileMode.Append, FileAccess.Write))

                using (StreamWriter _Log = new StreamWriter(logFile))
                {
                    _Log.WriteLine(DateTime.Now.ToString("[  dd/MM/yy hh:mm:ss  ]    ") + text);
                }

                //check log file size, if it is > 1 mb, start a new file

                FileInfo fi = new FileInfo(logFullPath);
                var filesize = fi.Length;

                if (filesize > 1 * 1024 * 1024)
                {
                    CreateNewLogFile();
                }
            }
        }

        public static void addToUrScriptFile(string text) 
        {
            if (UrSettings.writeUrScriptFile == true)
            {
                using (FileStream logFile = new FileStream(urScriptFullPath, FileMode.Append, FileAccess.Write))

                using (StreamWriter _Log = new StreamWriter(logFile))
                {
                    _Log.WriteLine(text);
                }

                //check log file size, if it is > 1 mb, start a new file

                FileInfo fi = new FileInfo(urScriptFullPath);
                var filesize = fi.Length;

                if (filesize > 1 * 1024 * 1024)
                {
                    urScriptName = "Log" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
                    urScriptFullPath = urScriptPath + urScriptName;
                }
            }

        }

        public static void addToTimestamps1DFile(string text)
        {
            if (UrSettings.writeTimestamps1DFile == true)
            {
                using (FileStream logFile = new FileStream(Timestamps1DFullPath, FileMode.Append, FileAccess.Write))

                using (StreamWriter _Log = new StreamWriter(logFile))
                {
                    _Log.WriteLine(text);
                }

                //check log file size, if it is > 1 mb, start a new file

                FileInfo fi = new FileInfo(Timestamps1DFullPath);
                var filesize = fi.Length;

                if (filesize > 1 * 1024 * 1024)
                {
                    Timestamps1DFilename = "timestamp1D" + "_" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
                    Timestamps1DFullPath = Timestamps1DPath + Timestamps1DFilename;
                }
            }

        }

        public static void addToArduinoGetFile(string text)
        {
            if (UrSettings.writeArduinoGetFile == true)
            {
                using (FileStream ArduinoGetFile = new FileStream(ArduinoGetFullPath, FileMode.Append, FileAccess.Write))

                using (StreamWriter _Log = new StreamWriter(ArduinoGetFile))
                {
                    _Log.WriteLine(text);
                }

                //check log file size, if it is > 1 mb, start a new file

                FileInfo fi = new FileInfo(ArduinoGetFullPath);
                var filesize = fi.Length;

                if (filesize > 1 * 1024 * 1024)
                {
                    ArduinoGetFilename = "Get" + "_" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
                    ArduinoGetFullPath = ArduinoGetFilePath + ArduinoGetFilename;
                }
            }

        }

        public static void addToArduinoSendFile(string text)
        {
            if (UrSettings.writeArduinoSendFile == true)
            {
                using (FileStream ArduinoSendFile = new FileStream(ArduinoSendFullPath, FileMode.Append, FileAccess.Write))

                using (StreamWriter _Log = new StreamWriter(ArduinoSendFile))
                {
                    _Log.WriteLine(text);
                }

                //check log file size, if it is > 1 mb, start a new file

                FileInfo fi = new FileInfo(ArduinoSendFullPath);
                var filesize = fi.Length;

                if (filesize > 1 * 1024 * 1024)
                {
                    ArduinoSendFilename = "Send" + "_" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
                    ArduinoSendFullPath = ArduinoSendFilePath + ArduinoSendFilename;
                }
            }

        }

        public static void saveDataToFile(double time, double x, double y, double z, double Rx, double Ry, double Rz)
        {
            TextWriter.Synchronized(wrireFile).WriteLine(time + " " + x + " " + y + " " + z + " " + Rx + " " + Ry + " " + Rz);
        }

        public static void closeSaveDataToFile()
        {
            wrireFile.Close();
        }

    }

}