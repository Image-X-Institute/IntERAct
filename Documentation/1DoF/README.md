## 1DoF platform
This folder contains the documentation on how to setup the 1DoF platform. The folder also contains a guide to run traces through the motion control software and an Arduino code, necessary to run the motion traces.


  -https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/main/Documentation/1DoF/Hardware%20Guide.pdf
  -https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/main/Documentation/1DoF/Arduino_V3.ino

The Voltage- motor speed relationship can be different depending on the payload, configuration, Firgelli model... In that case, [this Arduino code](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/main/Documentation/1DoF/voltage-motor%20speed%20relationship.ino) is used to run 1s the 1DoF platform with different voltage values. A motion acquisition system can be used [RealSense camera](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/tree/main/RealSense%20verification) to measure the displacement of the platform during one second. 

**1DoF platform wiring diagram:** please note - the circuit wiring diagram for the Arduino and 1DoF motion platform is: <Documentation/1DoF/1D motion platform circuit diagram.pdf>. The connections in this diagram have most recently been tested and ensure that the 1DoF works correctly (with the previous connections to the digital ~10 and ~11 ports as is seen in the Arduino-1D wiring diagram, the platform would extend to its maximum distance once plugged in (error)). The correct connections are to the ~10 and ~9 ports. Also note that this diagram includes an additional breadboard for ease of making the wiring connections and ease of use.
