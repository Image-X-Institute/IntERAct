 // * This is the CoordinateTransformation class. This class handles all coordinate transformations going from Euler to Axis Angle and visa versa. 
// * This class is called in both the URServer and MotionControl class to convert motion data from Euler to Axis Angle and Axis Angle to Euler respectively. 

using System;
using System.Windows.Media.Media3D;
using MathNet.Numerics.LinearAlgebra;
using static PhantomControl.MotionControl_tab;



namespace PhantomControl
{
    class CoordinateTransformation
    {

        Matrix<double> Rx = Matrix<double>.Build.Random(4, 4, 0);
        Matrix<double> Ry = Matrix<double>.Build.Random(4, 4, 0);
        Matrix<double> Rz = Matrix<double>.Build.Random(4, 4, 0);

        /*
         * Standard rotation and translation matrices for x, y and z and rx (psi), ry (theta), and rz (phi) are defined below.
         */

        private void rotationMatrix_X(double psi)
        {
            Rx[0, 0] = 1; Rx[0, 1] = 0; Rx[0, 2] = 0; Rx[0, 3] = 0;
            Rx[1, 0] = 0; Rx[1, 1] = Math.Cos(psi); Rx[1, 2] = -Math.Sin(psi); Rx[1, 3] = 0;
            Rx[2, 0] = 0; Rx[2, 1] = Math.Sin(psi); Rx[2, 2] = Math.Cos(psi); Rx[2, 3] = 0;
            Rx[3, 0] = 0; Rx[3, 1] = 0; Rx[3, 2] = 0; Rx[3, 3] = 1;
        }

        private void rotationMatrix_Y(double theta)
        {
            Ry[0, 0] = Math.Cos(theta); Ry[0, 1] = 0; Ry[0, 2] = Math.Sin(theta); Ry[0, 3] = 0;
            Ry[1, 0] = 0; Ry[1, 1] = 1; Ry[1, 2] = 0; Ry[1, 3] = 0;
            Ry[2, 0] = -Math.Sin(theta); Ry[2, 1] = 0; Ry[2, 2] = Math.Cos(theta); Ry[2, 3] = 0;
            Ry[3, 0] = 0; Ry[3, 1] = 0; Ry[3, 2] = 0; Ry[3, 3] = 1;
        }

        private void rotationMatrix_Z(double phi)
        {
            Rz[0, 0] = Math.Cos(phi); Rz[0, 1] = -Math.Sin(phi); Rz[0, 2] = 0; Rz[0, 3] = 0;
            Rz[1, 0] = Math.Sin(phi); Rz[1, 1] = Math.Cos(phi); Rz[1, 2] = 0; Rz[1, 3] = 0;
            Rz[2, 0] = 0; Rz[2, 1] = 0; Rz[2, 2] = 1; Rz[2, 3] = 0;
            Rz[3, 0] = 0; Rz[3, 1] = 0; Rz[3, 2] = 0; Rz[3, 3] = 1;
        }

        private Matrix<double> translationMaxtrix(double X, double Y, double Z)
        {
            
            Matrix<double> Txyz = Matrix<double>.Build.Random(4, 4, 0);

            Txyz[0, 0] = 1; Txyz[0, 1] = 0; Txyz[0, 2] = 0; Txyz[0, 3] = X;
            Txyz[1, 0] = 0; Txyz[1, 1] = 1; Txyz[1, 2] = 0; Txyz[1, 3] = Y;
            Txyz[2, 0] = 0; Txyz[2, 1] = 0; Txyz[2, 2] = 1; Txyz[2, 3] = Z;
            Txyz[3, 0] = 0; Txyz[3, 1] = 0; Txyz[3, 2] = 0; Txyz[3, 3] = 1;

            return Txyz;
        }

        // Gets Tflip matrix to convert coordinate systems to IEC standard

        private Matrix<double> getMatrixTFlip()
        {
       
            Matrix<double> Tflip = Matrix<double>.Build.Random(4, 4, 0);

            Tflip[0, 0] = 1; Tflip[0, 1] = 0; Tflip[0, 2] = 0; Tflip[0, 3] = 0;
            Tflip[1, 0] = 0; Tflip[1, 1] = 0; Tflip[1, 2] = -1; Tflip[1, 3] = 0;
            Tflip[2, 0] = 0; Tflip[2, 1] = 1; Tflip[2, 2] = 0; Tflip[2, 3] = 0;
            Tflip[3, 0] = 0; Tflip[3, 1] = 0; Tflip[3, 2] = 0; Tflip[3, 3] = 1;

            return Tflip;
        }

        // Function takes six parameter format (translation and rotation) and outputs a 4x4 matrix left multiplied by translation, roll, pitch, and yaw (TRPY). Order of rotation around x, then y, then z.

        public Matrix<double> getTRPY(double X, double Y, double Z, double psi, double theta, double phi)
        {
           
            Matrix<double> TRPY = Matrix<double>.Build.Random(4, 4, 0);

            
            Matrix<double> Txyz = translationMaxtrix(X, Y, Z);

            rotationMatrix_X(psi);
            rotationMatrix_Y(theta);
            rotationMatrix_Z(phi);

            
            Matrix<double> tempMatrix_1 = Matrix<double>.Build.Random(4, 4, 0);
            Matrix<double> tempMatrix_2 = Matrix<double>.Build.Random(4, 4, 0);

            tempMatrix_1 = Txyz.Multiply(Rz);
            tempMatrix_2 = tempMatrix_1.Multiply(Ry);
            TRPY = tempMatrix_2.Multiply(Rx);

            return TRPY;
        }

        /*
         * AxisAngleFromMatrix function takes a 4x4 matrix in TRPY format and converts the rotations into axis angle notation. 
         * Function outputs a Point3D object which can acsses the x, y and z elements.
         * E.g. Point3D foo; => foo.X = valueX, foo.Y = valueY, foo.Z = valueZ
         */

       
        public Point3D axisAngleFromMatrix(Matrix<double> TRPY)
        {
            Point3D axisAngle = new Point3D();

            //Matrix RPY = Matrix.Create(3, 3, 0);
            Matrix<double> RPY = Matrix<double>.Build.Random(3, 3, 0);

            double epsilon_1 = 0.01;
            double epsilon_2 = 0.1;
            double angle = 0.0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    RPY[i, j] = TRPY[i, j];
                }
            }

            // Test for singularities angle = 0 deg & angle = 180 deg
            if ((Math.Abs(RPY[0, 1] - RPY[1, 0])) < epsilon_1 && (Math.Abs(RPY[0, 2] - RPY[2, 0])) < epsilon_1 && (Math.Abs(RPY[1, 2] - RPY[2, 1])) < epsilon_1)
            {
                UpdateStatusBarMessage.ShowStatusMessage("Singularity found");
                // Singularity found
                if ((Math.Abs(RPY[0, 1] + RPY[1, 0])) < epsilon_2 && (Math.Abs(RPY[0, 2] + RPY[2, 0])) < epsilon_2 && (Math.Abs(RPY[1, 2] + RPY[2, 1])) < epsilon_2 && (Math.Abs(RPY[0, 0] + RPY[1, 1] + RPY[2, 2] - 3)) < epsilon_2)
                {
                    // Singularity is angle = 0 deg
                    angle = 0;
                    axisAngle.X = 1;
                    axisAngle.Y = 0;
                    axisAngle.Z = 0;

                    UrSettings.singularity = true;
                    return axisAngle;
                }
                else
                {
                    // Singularity is angle = 180 deg
                    angle = Math.PI;
                    double xx = (RPY[0, 0] + 1) / 2;
                    double yy = (RPY[1, 1] + 1) / 2;
                    double zz = (RPY[2, 2] + 1) / 2;
                    double xy = (RPY[0, 1] + RPY[1, 0]) / 4;
                    double xz = (RPY[0, 2] + RPY[2, 0]) / 4;
                    double yz = (RPY[1, 2] + RPY[2, 1]) / 4;

                    double temp_x = 0.0;
                    double temp_y = 0.0;
                    double temp_z = 0.0;

                    if ((xx > yy) && (xx > zz))
                    {
                        if (xx < epsilon_1)
                        {
                            temp_x = 0;
                            temp_y = 0.7071;
                            temp_z = 0.7071;
                        }
                        else
                        {
                            temp_x = Math.Sqrt(xx);
                            temp_y = xy / temp_x;
                            temp_z = xz / temp_x;
                        }
                    }
                    else if (yy > zz)
                    {
                        if (yy < epsilon_1)
                        {
                            temp_x = 0.7071;
                            temp_y = 0;
                            temp_z = 0.7071;
                        }
                        else
                        {
                            temp_y = Math.Sqrt(yy);
                            temp_x = xy / temp_y;
                            temp_z = yz / temp_y;
                        }
                    }
                    else if (zz < epsilon_1)
                    {
                        temp_x = 0.7071;
                        temp_y = 0.7071;
                        temp_z = 0;
                    }
                    else
                    {
                        temp_z = Math.Sqrt(zz);
                        temp_x = xz / temp_z;
                        temp_y = yz / temp_z;
                    }

                    axisAngle.X = temp_x;
                    axisAngle.Y = temp_y;
                    axisAngle.Z = temp_z;

                    UrSettings.singularity = true;
                    return axisAngle;
                }
            }

            angle = Math.Acos((RPY[0, 0] + RPY[1, 1] + RPY[2, 2] - 1) / 2);
            double N = Math.Sqrt(Math.Pow(RPY[2, 1] - RPY[1, 2], 2) + Math.Pow(RPY[0, 2] - RPY[2, 0], 2) + Math.Pow(RPY[1, 0] - RPY[0, 1], 2));

            if (Math.Abs(N) < 0.001)
            {
                N = 1;
            }

            double x = (RPY[2, 1] - RPY[1, 2]) / N;
            double y = (RPY[0, 2] - RPY[2, 0]) / N;
            double z = (RPY[1, 0] - RPY[0, 1]) / N;


            axisAngle.X = x * angle;
            axisAngle.Y = y * angle;
            axisAngle.Z = z * angle;

            UrSettings.singularity = false;
            return axisAngle;
        }

        // This functions takes an axis angle in the format (Rx, Ry, Rz) that is outputted from the Robot via MODBUS and converts it into a 3x3 rotation matrix.

        public Matrix<double> rotMatrixFromAxisAngle(double Rx, double Ry, double Rz)
        {

            Matrix<double> rotationMatrix = Matrix<double>.Build.Random(3, 3, 0);

            double angle = Math.Sqrt(Math.Pow(Rx, 2) + Math.Pow(Ry, 2) + Math.Pow(Rz, 2));

            double c = Math.Cos(angle);
            double s = Math.Sin(angle);
            double t = 1.0 - c;

            //  if axis is not already normalised then uncomment this
            double magnitude = Math.Sqrt(Rx * Rx + Ry * Ry + Rz * Rz);
            if (magnitude == 0)
            {
                return null;
            }
            Rx /= magnitude;
            Ry /= magnitude;
            Rz /= magnitude;

            rotationMatrix[0, 0] = c + Rx * Rx * t;
            rotationMatrix[1, 1] = c + Ry * Ry * t;
            rotationMatrix[2, 2] = c + Rz * Rz * t;

            double tmp1 = Rx * Ry * t;
            double tmp2 = Rz * s;
            rotationMatrix[1, 0] = tmp1 + tmp2;
            rotationMatrix[0, 1] = tmp1 - tmp2;
            tmp1 = Rx * Rz * t;
            tmp2 = Ry * s;
            rotationMatrix[2, 0] = tmp1 - tmp2;
            rotationMatrix[0, 2] = tmp1 + tmp2; tmp1 = Ry * Rz * t;
            tmp2 = Rx * s;
            rotationMatrix[2, 1] = tmp1 + tmp2;
            rotationMatrix[1, 2] = tmp1 - tmp2;

            return rotationMatrix;
        }

        // This function takes a 4x4 matrix and converts it into six parameter formats in form (x, y, z, rx, ry, rz), here translation is in mm and rotation are in degrees. The retuned datatype is an array of doubles

        public double[] getSixParameterPose(Matrix<double> m)
        {
            double[] sixParameterPose = new double[6];

            double sy = -m[2, 0];
            double cy = 1 - (sy * sy);
            double cx = 0;
            double sx = 0;
            double cz = 0;
            double sz = 0;

            if (cy > 0.00001)
            {
                cy = Math.Sqrt(cy);
                cx = m[2, 2] / cy;
                sx = m[2, 1] / cy;
                cz = m[0, 0] / cy;
                sz = m[1, 0] / cy;
            }
            else
            {
                cy = 0;
                cx = m[1, 1];
                sx = -m[1, 2];
                cz = 1;
                sz = 0;
            }

            double radsToDeg = 180 / Math.PI;

            sixParameterPose[0] = m[0, 3];
            sixParameterPose[1] = m[1, 3];
            sixParameterPose[2] = m[2, 3];
            sixParameterPose[3] = Math.Atan2(sx, cx) * radsToDeg;
            sixParameterPose[4] = Math.Atan2(sy, cy) * radsToDeg;
            sixParameterPose[5] = Math.Atan2(sz, cz) * radsToDeg;

            return sixParameterPose;
        }

        // This function getTransformationMatrix Assembles a 4x4 Matrix from a 3x3 rotation matrix and a translation vector, returns 4x4 Matrix
        
        public Matrix<double> getTransformationMatrix(Matrix<double> rotationMatrix, double[] translation)
        {
            
            Matrix<double> T = Matrix<double>.Build.Random(4, 4, 0);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    T[i, j] = rotationMatrix[i, j];
                }
            }

            T[0, 3] = translation[0];
            T[1, 3] = translation[1];
            T[2, 3] = translation[2];
            T[3, 0] = 0;
            T[3, 1] = 0;
            T[3, 2] = 0;
            T[3, 3] = 1;

            return T;
        }

        /* This function applies the coordinate transformation to get the relative position of the TCP. The function takes two double arrays "poseArray", which is the current value of the 
         * robot position, and "startingPose" which is the starting position of the robot. The retuned double array "Tk_abs" is the relative posistion based on the starting pose in IEC coordinate system  
         */

        public Matrix<double> getAbsoultePose(double[] poseArray, double[] startingPose)
        {
            double[] absPose = new double[6];
            
            Matrix<double> rotMatrixStart = rotMatrixFromAxisAngle(startingPose[3], startingPose[4], startingPose[5]);
            Matrix<double> rotMatrixInterrogated = rotMatrixFromAxisAngle(poseArray[3], poseArray[4], poseArray[5]);

            double[] translationStart = new double[3];
            double[] translationInterrogated = new double[3];

            for (int i = 0; i < 3; i++)
            {
                translationStart[i] = startingPose[i];
                translationInterrogated[i] = poseArray[i];
            }

           
            Matrix<double> To = getTransformationMatrix(rotMatrixStart, translationStart);
            Matrix<double> Ti = getTransformationMatrix(rotMatrixInterrogated, translationInterrogated);

            Matrix<double> Tflip = getMatrixTFlip();

            Matrix<double> _t1 = (Tflip.Inverse()).Multiply(To.Inverse());
            Matrix<double> _t2 = _t1.Multiply(Ti);
            Matrix<double> Tk_abs = _t2.Multiply(Tflip);

            return Tk_abs;
        }

        // This function converts the coordinate system of the robot TCP to the IEC61217 coordinate system. To is a 4x4 starting pose matrix, and Tk is thr 4x4 input motion matrix

        
        public Matrix<double> toolTipCoordToIEC(Matrix<double> To, Matrix<double> Tk)
        {
            

            Matrix<double> Tflip = getMatrixTFlip();

            Matrix<double> _t1 = To.Multiply(Tflip);
            Matrix<double> _t2 = _t1.Multiply(Tk);

            Matrix<double> Tr = _t2.Multiply(Tflip.Inverse());

            return Tr;
        }

    }
}