# 6 DoF Robotic Motion Phantom

The 6 DoF Robotic Motion Phantom application has been developed to perform quality assurance tests for real-time image-guided radiotherapy systems. The 6 DoF system consists of a six-axis robotic arm (Universal Robot, UR3), an acrylic phantom, a custom-base plate to mount the robotic phantom onto the treatment couch and a software application which controls the robotic phantom to reproduce the patient-measured tumor motion. The software provides methods to implement the appropriate sequence of transformations to accurately reproduce the full range (6 DoF translational and rotational motion) and rate of patient-measured tumour motion using the robotically controlled phantom. The set-up and motion trace repeatability of the robotic system was evaluated against the Calypso system and was shown to provide sub-mm and sub-degree accuracy and precision in translational and rotational degrees of freedom respectively. For more details, please see [this publication](https://doi.org/10.1088/1361-6560/ab1935).

## Key Features

- Accurate motion reproducibility for 6DoF tumour motion.
- A user-friendly GUI interface.
- Design compactness.
- Easy workflow.


## Getting Started
### Requirements
    
  * Hardware
     - A six-axis robot (Tested on a commercially available [UR3 Universal Robot](https://www.universal-robots.com/products/ur3-robot/)) and its associated hardware
     - A phantom
  
  * Software 
     - Microsoft Visual Studio 2019 with .NET desktop development workload
     - Winforms
  
  * Third-party libraries (located in `External Libraries/`)
     - EasyModbusTCP v4.2
     - LiveCharts v0.9.7
     - LiveCharts.WinForms v0.9.7.1
     - LiveCharts.Wpf v0.9.7
     - Math.Net Numerics v4.12.0
     

### Setting up the Robotic Motion Phantom

See the [Getting Started Guide](https://github.com/ACRF-Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/main/Documentation/Getting%20Started.docx).

### Building the Robotic Motion Phantom software

The software was written and tested in C# using Visual Studio 2019 with .NET Framework 4.5 using Windows 10 64-bit, but may work with other versions of Visual Studio.

1. Open `PhantomControl.sln` in Visual Studio.
2. Build the solution in Debug mode.
3. This will create the application `PhantomControl\bin\Debug\PhantomControl.exe` which can then be operated using [this guide](https://github.com/ACRF-Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/main/Documentation/Software%20GUI%20Guide.pdf) when connected to a six-axis robotic system.

## Documentation

Please see the [documentation](https://github.com/ACRF-Image-X-Institute/6-DoF-Robotic-Motion-Phantom/tree/main/Documentation) for instructions on operating the robotic phantom and relevant information related to the latest release of this software. 

## Publications

- S. Alnaghy et al., Phys. Med. Biol. 64 105021 (2019), [doi:10.1088/1361-6560/ab1935](https://doi.org/10.1088/1361-6560/ab1935)
- Shi K. et al., Med Phys. 47(12):6068-6076 (2020), [doi: 10.1002/mp.14502](https://aapm.onlinelibrary.wiley.com/doi/full/10.1002/mp.14502)

## Clinical Trials

- Liver Ablative Radiotherapy utilizing KIM, [TROG 17.03 LARK trial](https://www.trog.com.au/1703-LARK) 
- Novel Integration of New prostate radiation therapy schedules with adJuvant Androgen deprivation, [TROG 18.01 NINJA trial](https://www.trog.com.au/1801-NINJA)

## Licence

Content is released under [this licence](https://github.com/ACRF-Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/a5fb87378eb501c1a9539277ff3f0080b794489e/Copyright%20Notice%20and%20Licence.pdf) which includes attribution guidelines, contribution terms, and software and third-party licences and permissions.

## Limitations and safety 

The UR3 robot has automated tasks up to 3 kg (6.6 lbs). It has reach radius of up to 500 mm (19.7 in) with 0.1 mm and 0.2 deg repeatability. For all other safety related issues, please visit the [Universal Robot website](https://www.universal-robots.com/articles/).

## Future work

Future works planned to be done with this project:

- Phantoms with lung insert for quality assurance of the markerless tracking systems.
- Concurrent geometric and dosimetric quality assurance with a radiation detector or films. 
- QA other real-time tracking systems such as depth sensors as used in the [Remove Mask Project](https://image-x.sydney.edu.au/home/remove-the-mask/).

## Authors

Dr. Saree Alnaghy, Dr. Chandrima Sengupta, Kuldeep Makhija.

## Acknowledgements

The authors thank all the contributors, Dr. Andre Kyme, Dr. Vincent Caillet, Dr. Doan Trang Nguyen, Dr. Ricky O'Brien, Dr. Jeremy T. Booth and Prof. Paul J Keall for lending their valuable input and expertise leading up to the initial release. Thanks to Dr. Melissa Mail and Dr. Sara Tomka for their help in producing the licence of this software and to the Image-X team for providing valuable input in developing this repository.
