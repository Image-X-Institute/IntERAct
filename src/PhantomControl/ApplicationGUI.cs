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

namespace PhantomControl
{
    public partial class ApplicationGUI : Form
    {
        private string iconPath = "Resources\\Icons\\";

        URServer urServer = new URServer();
        Settings_tab settingsTab = new Settings_tab();

        public ApplicationGUI()
        {
            InitializeComponent();
            Logger.initialiseLogger();

            motionControl_tab.Show();
            settings_tab.Hide();
            log_tab.Hide();
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
            label_heading.Text = "Motion Control";
            var ipath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources\\Icons\\gamepad_Black.png");
            //picContainer.Image = Image.FromFile(iconPath + "gamepad_Black.png");
            picContainer.Image = Image.FromFile(ipath);

            motionControl_tab.Show();
            settings_tab.Hide();
            log_tab.Hide();
         
        }

        private void flatButton_settings_Click(object sender, EventArgs e)
        {
            label_heading.Text = "Settings";
            var ipath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Resources\\Icons\\settings_Black.png");
            //picContainer.Image = Image.FromFile(iconPath + "settings_Black.png");
            picContainer.Image = Image.FromFile(ipath);

            motionControl_tab.Hide();
            settings_tab.Show();
            log_tab.Hide();
  
        }

        private void flatButton_log_Click(object sender, EventArgs e)
        {
            label_heading.Text = "Log";
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
    }

    public static class Logger
    {
        private static string logFilename = "/Log" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
        private static string logFilePath = "Output Files/Log";
        public static string logFullPath = string.Empty;

        private static string urScriptName = "/urScript" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
        private static string urScriptPath = "Output Files/GeneratedUrScripts";
        public static string urScriptFullPath = string.Empty;

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

            logFullPath = logFilePath + logFilename;
            urScriptFullPath = urScriptPath + urScriptName;
        }

        public static void initialiseDataFile()
        {
            wrireFile = new StreamWriter("Output Files/MotionData_" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt");
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
                    logFilename = "Log" + DateTime.Now.ToString("ddMMyy-hhmm") + ".txt";
                    logFullPath = logFilePath + logFilename;
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