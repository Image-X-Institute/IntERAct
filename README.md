# 6 DoF Robotic Motion Phantom
## Introduction
Robotic Motion Phantom is an open source software for quality assurance of real-time image-guided radiotherapy systems. The Robotic Motion Phantom application provides methods to implement the appropriate sequence of transformations to accurately reproduce the full range and rate of patient-measured tumour motion using a robotically controlled phantom to provide precise geometric quality assurance for advanced radiotherapy approaches. The set-up and motion trace repeatability of the robotic motion system was evaluated against the Calypso system and was shown to have sub-mm and sub-degree accuracy and precision in translational and rotational degrees of freedom respectively.

## Development Stack

    - C#
    - Winforms

## Third-party libraries

    - EasyModbusTCP v4.2
    - LiveCharts v0.9.7
    - LiveCharts.WinForms v0.9.7.1
    - LiveCharts.Wpf v0.9.7
    - Math.Net Numerics v4.12.0

## Building the Robotic Motion Phantom

Open PhantomControl.sln in Visual Studio and build the solution in Debug mode (or in Release mode). This will create the executable PhantomControl\bin\Debug(or Release)\PhantomControl.exe.

## Key Features

    - Accurate motion reproducibility for safer radiotherapy imaging systems.
    - A user-friendly GUI interface that is ease of use.
    - Design compactness.
    - Easy workflow.

## Documentation

See Documentation/README.md.

## Publications

    S. Alnaghy et al., Phys. Med. Biol. 64 105021 (2019).
    Shi K. et al., Med Phys. 2020 Sep 30. doi: 10.1002/mp.14502. Epub ahead of print. PMID: 32997820.

## Licenses

Content is released under XXXX. See the link [https://github.com/ACRF-Image-X-Institute/6-DoF-Robotic-Motion-Phantom/blob/a5fb87378eb501c1a9539277ff3f0080b794489e/Copyright%20Notice%20and%20Licence.pdf] for complete details, including attribution guidelines, contribution terms, and software and third-party licenses and permissions.
