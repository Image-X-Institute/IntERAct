# 6 DoF Robotic Motion Phantom
## Introduction
Robotic Motion Phantom is an open source software for performing quality assurance tests of real-time image-guided radiotherapy systems. The Robotic Motion Phantom application provides methods to implement the appropriate sequence of transformations to accurately reproduce the full range and rate of patient-measured tumour motion using a robotically controlled phantom to provide precise geometric quality assurance for advanced radiotherapy approaches. The set-up and motion trace repeatability of the robotic motion system was evaluated against the Calypso system and was shown to have sub-mm and sub-degree accuracy and precision in translational and rotational degrees of freedom respectively.

## Requirements
  
  * Software 
     - C# and Visual Studio 
     - Winforms
    
  * Hardware
     - A six-axis robot
     - A phantom

## Third-party libraries

    - EasyModbusTCP v4.2
    - LiveCharts v0.9.7
    - LiveCharts.WinForms v0.9.7.1
    - LiveCharts.Wpf v0.9.7
    - Math.Net Numerics v4.12.0

## Building the Robotic Motion Phantom

    - Open PhantomControl.sln in Visual Studio and add all the five external libraries listed above using the References tab in the Solution Explorer. 
    - Build the solution in Debug mode (or in Release mode).
    - This will create the application PhantomControl\bin\Debug(or Release)\PhantomControl.exe which can then be operated using [this guideline](https://github.com  /ACRF-Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/main/Documentation/Software%20GUI%20Guide.pdf) when connected to a six-axis robotic system.

## Key Features

    - Accurate motion reproducibility for safer radiotherapy imaging systems.
    - A user-friendly GUI interface that is ease of use.
    - Design compactness.
    - Easy workflow.

## Documentation

[Documentation folder](https://github.com/ACRF-Image-X-Institute/6-DoF-Robotic-Motion-Phantom/tree/main/Documentation) provides relevant information related to the latest release of this software and the instructions to operate the robotic phantom.

## Publications

    - S. Alnaghy et al., Phys. Med. Biol. 64 105021 (2019).
    - Shi K. et al., Med Phys. 2020 Sep 30. doi: 10.1002/mp.14502. Epub ahead of print. PMID: 32997820.

## Licence

Content is released under [this licence](https://github.com/ACRF-Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/a5fb87378eb501c1a9539277ff3f0080b794489e/Copyright%20Notice%20and%20Licence.pdf) that includes attribution guidelines, contribution terms, and software and third-party licenses and permissions.

## Acknowledgements

The authors thank all the contributors, Dr. Andre Kyme, Dr. Vincent Caillet, Dr. Doan Trang Nguyen, Dr. Ricky O'Brien, Dr. Jeremy T. Booth and Prof. Paul J Keall for lending their valuable input and expertise leading up to the initial release, and, thanks to Dr. Melissa Mail and Dr. Sara Tomka for their help in producing the licence of this software.
