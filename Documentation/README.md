## Documentation
This folder contains the documentation about the 1DoF platform (software and hardware setup and Arduino code to download), the 6DoF platform and then how to run the combined system through the motion software. The folder [Verification](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/tree/main/Documentation/Verification) shows the results of the combined system used in the IX laboratory but also in the clinic (Royal North Shore Hospital).

  - [1DoF platform documentation](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/tree/main/Documentation/1DoF)
  - [6DoF platform documentation](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/tree/main/Documentation/6DoF)
  - [Motion control software](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/main/Documentation/6DoF/Software%20GUI%20Guide.pdf)

An error message can occur before to run the software. This error can be ignored and the software can be ran properly. 

Error:		The command "copy /y C:\Users\Robot\source\repos\6-DoF-Robotic-Motion-Phantom_v2\Program Files\src\PhantomControl\settings.txt C:\Users\Robot\source\repos\6-DoF-Robotic-Motion-Phantom_v2\Program Files\src\PhantomControl\bin\Debug\

md C:\Users\Robot\source\repos\6-DoF-Robotic-Motion-Phantom_v2\Program Files\src\PhantomControl\Resources
\
xcopy /I /E C:\Users\Robot\source\repos\6-DoF-Robotic-Motion-Phantom_v2\Program Files\src\PhantomControl\Resources\ C:\Users\Robot\source\repos\6-DoF-Robotic-Motion-Phantom_v2\Program Files\src\PhantomControl\bin\Debug\Resources\" exited with code 4.	PhantomControl			
). 

**Please note before running the robot software:** Upon first installation of the robot software, not all folders will be generated for the output logs to go in. This could cause an error message. Before first operation, ensure the folders with exact folder names (in the appropriate structure) are present - see related Issues page: https://github.com/Image-X-Institute/IntERAct/issues/51.
