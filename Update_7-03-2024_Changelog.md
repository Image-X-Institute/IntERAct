# 6 DoF Robotic Motion Phantom- 1D Integration Update Changelog

This update brings the recent integration of a 1 Degree, Vertical Motion Platform robot that can be controlled by this program. With this integration, the user is able to upload and run motion traces on both the 6DOF and 1D robot simultaneously; this update also allows standalone control of either the 6DOF or 1D robot.
## Key Features and additions

- Integration of 1-Dimensional Vertical Motion Platform
- Additions to chart control and enhanced user interface and user experience
- Update to program design
- Improved readability

- Implemented range adjustment, zooming and panning

Before:
![image](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/assets/152374331/6fb378e9-6d13-4714-b382-79e74a13ba00)

After:
![image](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/assets/152374331/9902e453-fcb0-4e7d-9f27-b6479b43400e)

- Added range slider which will adjust how far from the start the user wants to view the trace. This is most useful when playing the motion trace and viewing the real time output plots

- Added Reset Zoom button. Since zooming and panning is also implemented (with click and drag, scroll wheel etc), the user may want to reset the plot zoom

- Implemented 1D robot input plots and real-time feedback/output plots on the same translation chart

- Allow to toggle between the 6DOF trace, 1D trace or both traces

Both:
![image](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/assets/152374331/32db6489-ab57-4597-a690-264ed46658c1)

Toggle 6DOF:
![image](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/assets/152374331/ae45e7f3-08cf-4ef5-8b30-f2e19b667d5c)

Toggle 1D:
![image](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/assets/152374331/3e62af17-292d-40a5-b1a0-b827fe8a844c)

#### Additional Notes:
1. Improved and cleaned up UI. Buttons colours and positioning.
2. Allow user to chose between 6DOF, 1D or simulataneous control.
3. Improved how the UI buttons, controls and plots scale according to screen size. This works fine with any resolution however if the user's display scaling is not set at 100%, there could be scaling issues. To check, go into Display Settings on your device (currently implemented for Motion Control tab only, not the Log or Settings tab).
4. Live translational plotting of 1D motion feedback.
5. Added Help/Documentation hyperlink to redirect users to correct documentation
6. Added 1D connection status below current 6DOF connection status
7. User inputs such as robot selection, connecting and homing of 1D platform are now entered to log automatically.

## Getting Started
### Updated Requirements
    
  * Hardware 6D
     - A six-axis robot (Tested on a commercially available [UR3 Universal Robot](https://www.universal-robots.com/products/ur3-robot/)) and its associated hardware
     - A phantom
  * Hardware 1D
    - Optical Linear Actuator - 35lb, 4 inch stroke length (commercially available [https://www.firgelliauto.com.au/products/os-series])
    - Arduino Uno - (commercially available [https://store.arduino.cc/products/arduino-uno-rev3]
    - Optical Base Mounting Bracket for vertical applications (commercially available [https://www.firgelliauto.com.au/products/mb-pb-premium-base-mounting-bracket])
    - High Current DC Motor Drive (commercially available [https://www.firgelliauto.com/products/high-current-dc-motor-drice-43a])
    - 12V DC power supply
    - Wiring equipment (breadboard, wires etc).
  
  * Software 
     - Microsoft Visual Studio 2019 with .NET framework 4.5 (tested). May work on other versions too.  
     - Winforms
     - Arduino IDE
  
  * Third-party libraries (located in `External Libraries/`)
     - EasyModbusTCP v4.2
     - LiveCharts v0.9.7
     - LiveCharts.WinForms v0.9.7.1
     - LiveCharts.Wpf v0.9.7
     - Math.Net Numerics v4.12.0
     
