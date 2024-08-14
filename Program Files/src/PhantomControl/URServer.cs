using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Media.Media3D;
using System.Windows.Forms;
using System.Threading;
using MathNet.Numerics.LinearAlgebra;
using static PhantomControl.MotionControl_tab;

/* This is the URServer class, the methods in this class control connection via TCP to the controller for the primary client and dashboard server. 
 * URScript functions are implemented in this class. When data is loaded into the software this class handles conversion to URScript language and sets
 * it in axis angle notation before being sent to the controller. In coordinate transformations methods from the CoordinateTransformation class are called 
 * in this class.
 */

namespace PhantomControl
{
    class URServer
    {
        private List<Point3D> axisAngleList = new List<Point3D>();                              
        private List<Point3D> translationList = new List<Point3D>();

        CoordinateTransformation coordTranform;

        //Socket db = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        private int PORT_30001 = 30001;
        private int PORT_29999 = 29999;
 
        public URServer()
        {
            coordTranform = new CoordinateTransformation();
        }

        // Converts payload value from double to string in URScript format
        private string setPayLoad(double mass)                                     
        {
            string payLoad = "set_payload_mass(" + mass.ToString() + ")";
            
            return payLoad;
        }

        // Converts TCP array values from double to string in URScript format, Note that TCPy and TCPz are swapped to match the IEC system
        public string setTPC(double[] posePosition)
        {   
            string pose = "p[" + posePosition[0].ToString() + "," + posePosition[2].ToString() + "," + posePosition[1].ToString() + "," + posePosition[3].ToString() + "," + posePosition[4].ToString() + "," + posePosition[5].ToString() + "]";
            string TCP = "set_tcp(" + pose + ")";

            return TCP;
        }

        /* The function below performs the transformation matrix from 6 parameter format (x, y, z, Rx, Ry, Rz) to axis angle values for the robot to run the motion.
         * The function runs the crossCalibration methoud. All the input data are converted into a 4 x 4 matrix.
         */
        private void crossCalibration()
        {
            // Clears vectors if they have stuff in them.
            if (axisAngleList.Count() != 0 && translationList.Count() != 0)
            {
                axisAngleList.Clear();
                translationList.Clear();
            }

            // Creates a temporally variable using Point3D to store translation values. Point3D --> temp_translation.x, temp_translation.y, temp_translation.z.
            // rotMatrix stores staring position in axis angle notation.
            Point3D temp_translation = new Point3D();
           
            Matrix<double> rotMatrix = coordTranform.rotMatrixFromAxisAngle(MotionTraces.startingPose[3], MotionTraces.startingPose[4], MotionTraces.startingPose[5]);

            // create new double array to store translation values from staring pose.
            double[] startingTranslation = new double[3];

            // Convert starting translation values into m from mm.
            for (int i = 0; i < 3; i++)
            {
                startingTranslation[i] = MotionTraces.startingPose[i] / 1000;
            }

            // Place starting pose in a 4 x 4 matrix format
            
            Matrix<double> To = coordTranform.getTransformationMatrix(rotMatrix, startingTranslation);

          
                // This for loop is the main part of this method. It performs the calibration and coordinate transformation into axis angle ready to be sent to the robot.
                for (int i = 0; i < MotionTraces.Size; i++)
            {
                    
                    // Converts input motion data into 4 x 4 matrix (Tk), this is expressed as Translation, Roll, Pitch, Yaw --> TRPY.
                    
                    Matrix<double> Tk = coordTranform.getTRPY(MotionTraces.X[i], MotionTraces.Y[i], MotionTraces.Z[i], MotionTraces.Rx[i], MotionTraces.Ry[i], MotionTraces.Rz[i]);

                    /* Here we left multiply the matrix To with Tk. We are also changing the coordinate system --> swap y axis with z axis. This is to change from the
                     * robot tool tip coordinate system to IEC 61217 coordinate system standard.
                     */
                    
                    Matrix<double> Tr = coordTranform.toolTipCoordToIEC(To, Tk);

                // Converts rotation part of matrix to axis angle --> stores it as a Point3D var.
                Point3D temp_axisAngle = coordTranform.axisAngleFromMatrix(Tr);

                // Stores translation part of matrix into Point3D var. 
                temp_translation.X = Tr[0, 3];
                temp_translation.Y = Tr[1, 3];
                temp_translation.Z = Tr[2, 3];

                // Adds translation and rotation values into vector.
                translationList.Add(temp_translation);
                axisAngleList.Add(temp_axisAngle);
            }

            // Safty check
            checkMotion();
        }

        // This method checks if converted values are within acceptable values i.e. It is a safety check to make sure the robot will not move a very large distance.
        private void checkMotion()
        {
            // deltaT is threshold tolerance for translation in mm, deltaR is the threshold tolerance for rotation in rad.
            double deltaT = 100;
            double deltaR = 1.05;

            for (int i = 0; i < translationList.Count() - 1; i++)
            {
                double diffX = (Math.Abs(translationList[i].X - translationList[i + 1].X)) * 1000;
                double diffY = (Math.Abs(translationList[i].Y - translationList[i + 1].Y)) * 1000;
                double diffZ = (Math.Abs(translationList[i].Z - translationList[i + 1].Z)) * 1000;

                double axisX = (Math.Abs(axisAngleList[i].X - axisAngleList[i + 1].X));
                double axisY = (Math.Abs(axisAngleList[i].Y - axisAngleList[i + 1].Y));
                double axisZ = (Math.Abs(axisAngleList[i].Z - axisAngleList[i + 1].Z));

                if (diffX >= deltaT || diffY >= deltaT || diffZ >= deltaT || axisX >= deltaR || axisY >= deltaR || axisZ >= deltaR)
                {
                    UrSettings.movementToLarge = true;
                    return;
                }
            }

            UrSettings.movementToLarge = false;
        }


        /* This methods takes the acceleration, velocity, time, tool centre point(TCP) and mass settings to write a UrScript function. It also gets the transformed motion 
         * data points and converts them into move poses for the robot to run (movej, movep, movel). Acceleration and velocity parameters must be given. However, if 
         * time is non-zero the acceleration and velocity values will be ignored and time will be used to calculate the motion between two points. This is the main method of this class
         * and calls other large methods within the fucntion such as crossCalibration().
         */
        public void generateUrScript(double time, double[] TCP, double mass)
        {
            // Clears UrScriptProgram.urList if full, this is so you dont get multiple UrScript programs sent to the robot.
            if (UrScriptProgram.urList.Count != 0)
            {
                UrScriptProgram.urList.Clear();
            }

            // Runs the crossCalibration method.
            crossCalibration();

            // UrScriptProgram object is called, commands in the URScript language are placed as a string in the .urList variable as List<strings> datatype. 
            UrScriptProgram.urList.Add("def moving():");

            UrScriptProgram.urList.Add(setPayLoad(mass));
            UrScriptProgram.urList.Add(setTPC(TCP));


            /* UrSettings class is called to generate the selected motion type; movep, movej, or movel. TranslationList, and axisAngleList vectors which store the transformed and calibrated motion poses
             * are converted into a string and converted into the UrScript move command format e.g. movej(x,y,z,Rx,Ry,Rz, velocity, accelration, time) --> Units (m,rad [Axis Angle]).
             */

            if (UrSettings.MotionType == "movej")
            {
                Console.WriteLine(MotionTraces.Size.ToString());
                for (int i = 0; i < MotionTraces.Size; i++)
                {
                    UrScriptProgram.urList.Add("movej(p[" + translationList[i].X.ToString() + ", " + translationList[i].Y.ToString() + ", " + translationList[i].Z.ToString() + ", " + axisAngleList[i].X.ToString() + ", " + axisAngleList[i].Y.ToString() + ", " + axisAngleList[i].Z.ToString() + "]" + ", " + "a = " + "0.2" + ", " + "v = " + "0.2" + ", " + "t = " + time.ToString() + ")");
                }
            }
            if (UrSettings.MotionType == "movel")
            {
                for (int i = 0; i < MotionTraces.Size; i++)
                {
                    UrScriptProgram.urList.Add("movel(p[" + translationList[i].X.ToString() + ", " + translationList[i].Y.ToString() + ", " + translationList[i].Z.ToString() + ", " + axisAngleList[i].X.ToString() + ", " + axisAngleList[i].Y.ToString() + ", " + axisAngleList[i].Z.ToString() + "]" + ", " + "a = " + "0.2" + ", " + "v = " + "0.2" + ", " + "t = " + time.ToString() + ")");
                }
            }

            UrScriptProgram.urList.Add("end");

            return;
        }

        /* This method uses port 2999 (Dashboard server) to check and see if there is a UrScript program that is running. It sends a sting message "running" to the server and waits to receive a response packet
         * that is a 68-byte array. The package size we are looking for is a 6-byte array at, starting at byte array 62. A true or false value is recived and converted into a sting from a byte array. This
         * method is not implemented into the code now, but should be used to check when the program is complted to automatacly close the MODBUS connection in the MotionControl class.
         */
        public bool isProgramRunning()
        {
            Socket db = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            if (db.Connected == false)
            {
                try
                {
                    IPAddress ipAddress = IPAddress.Parse(UrSettings.hostIPAddress);
                    db.Connect(ipAddress, PORT_29999);
                }
                catch (Exception excep)
                {
                    UpdateStatusBarMessage.ShowStatusMessage("Error faild to connect on port " + PORT_29999.ToString());
                    Logger.addToLogFile("Error faild to connect on port " + PORT_29999.ToString());
                    return false;
                }
            }

            string message = "running" + "\n";

            byte[] sendPackage = ASCIIEncoding.ASCII.GetBytes(message);
            byte[] RecvPackage = new byte[68];

            db.Send(sendPackage);

            System.Threading.Thread.Sleep(80);

            int packet = db.Receive(RecvPackage, RecvPackage.Length, 0);
            int packageSize = packet - 62;

            byte[] _temp = new byte[packageSize];

            for (int i = 0; i < _temp.Length; i++)
            {
                _temp[i] = RecvPackage[i + 62];
            }

            string programState = System.Text.Encoding.UTF8.GetString(_temp);
            programState = programState.Replace("\n", string.Empty);

            if (programState == "true")
            {
                db.Close();
                return true;
            }

            db.Close();
            return false;           
        }

        /* This method uses port 2999 (Dashboard server) to stop a URScript program mid-way though running. It only sends a packet byte array "stop" and doesn’t need to receive any conformation. This method is used in
         * the MotionControl class GUI buttion to stop the motion of the robot.
         */
        public void stopRobot()
        {
            Socket db = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            if (db.Connected == false)
            {
                try
                {
                    IPAddress ipAddress = IPAddress.Parse(UrSettings.hostIPAddress);
                    db.Connect(ipAddress, PORT_29999);
                }
                catch
                {
                    UpdateStatusBarMessage.ShowStatusMessage("Error faild to connect on port " + PORT_29999.ToString());
                    Logger.addToLogFile("Error faild to connect on port " + PORT_29999.ToString());
                }
            }

            string message = "stop" + "\n";

            byte[] sendPackage = ASCIIEncoding.ASCII.GetBytes(message);
            db.Send(sendPackage);

            db.Close();
            
        }

        /* This method is the last method called in the MotionControl class to run the motion on the robot. The fucntion takes one input variable List<string> that contains the URScript program. This method creates 
         * a TCP connection on port 30001 (secondary port) and sends the generated UrScript program that is performed in the generateUrScript method. 
         */
        public void sendUrScript(List<string> urList)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string urScriptStr = string.Empty;

            // The URScript program commands need to be on a new line for each command, the loop below converts the urList array of strings into a single string block with a new line for each command and is placed in urScriptStr variable.
            for (int i = 0; i < urList.Count; i++)
            {
                urScriptStr += urList[i] + Environment.NewLine;
            }

            UpdateStatusBarMessage.ShowStatusMessage("Opening IP address: " + UrSettings.hostIPAddress);
            Logger.addToLogFile("Opening IP address: " + UrSettings.hostIPAddress);

            if (s.Connected == false)
            {
                try
                {
                    IPAddress ipAddress = IPAddress.Parse(UrSettings.hostIPAddress);
                    s.Connect(ipAddress, PORT_30001);
                }
                catch (Exception excep)
                {
                    UpdateStatusBarMessage.ShowStatusMessage("Error faild to connect on port " + PORT_30001.ToString());
                    Logger.addToLogFile("Error faild to connect on port " + PORT_30001.ToString());
                    return;
                }
            }

            // If connect to port 30001 successfully, the urScriptStr string variable will be converted into a byte array and sent to the robot controller, the connection is then closed.
            byte[] array = ASCIIEncoding.ASCII.GetBytes(urScriptStr);
            s.Send(array);

            System.Threading.Thread.Sleep(1000);

            s.Close();
        }
     
    }

    // The UrScriptProgram is a static class that can be accessed in different classes. This objects contains methods to store a List<string> for the URScript program. Variabls in this class are a set and get format.
    public static class UrScriptProgram
    {
        public static List<string> urList = new List<string>();
        public static List<string> home = new List<string>();
        public static List<string> alinged = new List<string>();

        public static IEnumerable<string> _urList
        {
            get { return _urList.AsEnumerable(); }
        }

        public static IEnumerable<string> _home
        {
            get { return _home.AsEnumerable(); }
        }

        public static IEnumerable<string> _alinged
        {
            get { return _alinged.AsEnumerable(); }
        }

        public static void AddValue_List(string item)
        {
            urList.Add(item);
        }

        public static void AddValue_Home(string item)
        {
            home.Add(item);
        }

        public static void AddValue_Aligned(string item)
        {
            alinged.Add(item);
        }
    }

}
