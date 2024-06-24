# 6 DoF Robotic Motion Phantom- 1D Integration Update Changelog

This update continues the work carried out in the update-07-03-2024 branch and includes codes for testing the accuracy, reproducibility, and behaviour of the combined system (6DoF Robotic Motion Phantom - 1DoF Vertical 
Motion Phantom). In this version, more information is recorded in the log files during use and the user will be able to pause the 1D platform via the software during the trace. 


## Materials used for the tests and updated requirements
    
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
     
