# Combined system : 6 DoF Robotic Motion Phantom and 1DoF Motion Platform

The association of 6DoF and 1DoF application has been developed to perform quality assurance tests for real-time image-guided radiotherapy systems. The 6 DoF system consists of a six-axis robotic arm (Universal Robot, UR3), an acrylic phantom, a custom-base plate to mount the robotic phantom onto the treatment couch and a software application which controls the robotic phantom to reproduce the patient-measured tumor motion. The [software](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/tree/main/Program%20Files) provides methods to implement the appropriate sequence of transformations to accurately reproduce the full range (6 DoF translational and rotational motion) and rate of patient-measured tumour motion using the robotically controlled phantom. The set-up and motion trace repeatability of the robotic system was evaluated against the Calypso system and was shown to provide sub-mm and sub-degree accuracy and precision in translational and rotational degrees of freedom respectively. The basis of moving the 1D motion platform is the linear actuator motor, that is driven by a microcontroller. The aim of this project is to integrate a 1DoF vertical motion platform with 6DoF Robot Phantom to establish control of both robots under one software. The purpose of this goal is emulate a patient's behavior that is present during treatment such as breathing, which can make it much harder to track and target the tumor. The 6DoF robot is used to mimic the internal motion of the tumor while the 1DoF platform is intended to use the data collected from surface/external tracers on the patient to model and replicate the external motion of the tumor. For more details, please see [this publication](https://doi.org/10.1088/1361-6560/ab1935).


## Key Features

- Accurate motion reproducibility for 6DoF tumour motion and 1DoF external respiratory motion.
- A user-friendly GUI interface.
- Design compactness.
- Easy workflow.


## Getting Started
### Requirements
    
  * Hardware
     - A six-axis robot (Tested on a commercially available [UR3 Universal Robot](https://www.universal-robots.com/products/ur3-robot/)) and its associated hardware
     - A phantom
     - A one dimensionnal linear actuator (Tested on a commercially available https://www.firgelliauto.com.au/products/os-series) and its associated hardware (Arduino board, DC Motor Drive)
     - Acrylic pane
  
  * Software 
     - Microsoft Visual Studio 2022 with .NET framework 4.5 (tested). May work on other versions too.  
     - Winforms
  
  * Third-party libraries (located in `External Libraries/`)
     - EasyModbusTCP v4.2
     - LiveCharts v0.9.7
     - LiveCharts.WinForms v0.9.7.1
     - LiveCharts.Wpf v0.9.7
     - Math.Net Numerics v4.12.0
     
## Documentation

The [1DoF Documentation folder](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/tree/main/Documentation/1DoF) contains required information to assemble a similar 1DoF system. The [6DoF Documentation folder](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/tree/main/Documentation/6DoF) contains required information to assemble a similar 6-DoF system: Bill of materials, CAD files for the phantom and hardware tools, robot specifications, documentation, a user-guide to operate the system, and, step-by-step QA procedure for a real-time image-guided radiotherapy technology, Kilovoltage Intrafraction Monitoring (KIM) using the robotic phantom. The [Combined system documentation folder](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/tree/main/Documentation/Combined%20system) contains required information to run the combined system. 

### Setting up the system

See the [Combined system documentation](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/tree/main/Documentation/Combined%20system).

### Building the Robotic Motion Phantom software

The software was written and tested in C# using Visual Studio 2019 with .NET Framework 4.5 using Windows 10 64-bit, but may work with other versions of Visual Studio.



1. Open `PhantomControl.sln` in Visual Studio (https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/tree/main/Program%20Files).
2. Build the solution in Release mode.
3. This will create the application `PhantomControl\bin\Release\PhantomControl.exe` which can then be operated using [this guide](https://github.com/Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/main/Documentation/Combined%20system/Combined%20system%20Documentation.pdf) when connected to a six-axis robotic system or/and one dimensionnal platform.

## Publications

- S. Alnaghy et al., Phys. Med. Biol. 64 105021 (2019), [doi:10.1088/1361-6560/ab1935](https://doi.org/10.1088/1361-6560/ab1935)
- Shi K. et al., Med Phys. 47(12):6068-6076 (2020), [doi: 10.1002/mp.14502](https://aapm.onlinelibrary.wiley.com/doi/full/10.1002/mp.14502)

## Clinical Trials

- Liver Ablative Radiotherapy utilizing KIM, [TROG 17.03 LARK trial](https://www.trog.com.au/1703-LARK) 
- Novel Integration of New prostate radiation therapy schedules with adJuvant Androgen deprivation, [TROG 18.01 NINJA trial](https://www.trog.com.au/1801-NINJA)
- Surface Monitoring technology to Remove The mask, [SMART trial](https://image-x.sydney.edu.au/home/clinical-trials/).

## Clinical use
- Adapted for routine quality assurance tests at Princess Alexandra Hospital.

## Licence

Content is released under [this licence](https://github.com/ACRF-Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/a5fb87378eb501c1a9539277ff3f0080b794489e/Copyright%20Notice%20and%20Licence.pdf) which includes attribution guidelines, contribution terms, and software and third-party licences and permissions.

## Limitations and safety 

The UR3 robot has automated tasks up to 3 kg (6.6 lbs). It has reach radius of up to 500 mm (19.7 in) with 0.1 mm and 0.2 deg repeatability. For all other safety related issues, please visit the [Universal Robot website](https://www.universal-robots.com/articles/).

## Future work

Future works planned to be done with this project:

- Phantoms with lung insert for quality assurance of the markerless tracking systems.
- Concurrent geometric and dosimetric quality assurance with a radiation detector or films. 
- QA other real-time tracking systems such as depth sensors as used in the [Remove the Mask Project](https://image-x.sydney.edu.au/home/remove-the-mask/).

## Authors

Dr. Saree Alnaghy, Dr. Chandrima Sengupta, Kuldeep Makhija.

## Contact
Chandrima.Sengupta@sydney.edu.au

## Acknowledgements

The authors thank all the contributors, Dr. Andre Kyme, Dr. Vincent Caillet, Dr. Doan Trang Nguyen, Dr. Ricky O'Brien, Dr. Jeremy T. Booth and Prof. Paul J Keall for lending their valuable input and expertise leading up to the initial release. Thanks to Dr. Melissa Mail and Dr. Sara Tomka for their help in producing the licence of this software and to the Image-X team for providing valuable input in developing this repository.
