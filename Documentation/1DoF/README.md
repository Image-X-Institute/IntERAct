## 1DoF platform
This folder contains the documentation on how to setup the 1DoF platform. The folder also contains a guide to run traces through the motion control software and an Arduino code, necessary to run the motion traces.


  -https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/main/Documentation/1DoF/Hardware%20Guide.pdf
  -https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/main/Documentation/1DoF/Arduino_V3.ino

The Voltage- motor speed relationship can be different depending on the payload, configuration, Firgelli model... In that case, [this Arduino code](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/main/Documentation/1DoF/voltage-motor%20speed%20relationship.ino) is used to run 1s the 1DoF platform with different voltage values. A motion acquisition system can be used [RealSense camera](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/tree/main/RealSense%20verification) to measure the displacement of the platform during one second. 
