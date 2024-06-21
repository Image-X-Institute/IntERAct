# RealSense camera 
This README file is written to help users to set up the RealSense camera which could be useful to measure reproducibility, accuracy and behaviour of 1D and/or 6D motions. 
## Pre-requirements
  - RealSense LiDAR L515
  - USB port 3.0
  - Python 3.11.8 (new versions do not support pyrealsense2 library of RealSense package)
  - Visual Studio code version 1.90
  - USBC to USBA cable
  - Tutorial to run python codes on Visual Studio : https://code.visualstudio.com/docs/python/python-tutorial
  - Install opencv-python, numpy, datetime and time libraries
  - "1ROI_RealSense.py" used the header file "realsense_depth_1ROI.py"
  - "2ROI_RealSense.py"used the header file "realsense_depth_2ROI.py"
  - Tracking marker(s)
  - The camera must be positionned perpendicularly and atleast 50cm under the platform(s)

## Current RealSense camera parameters :
Depth Stream Configuration :
- Resolution : 1024x720 pixels.
- Format of the depth stream : 16-bit z-depth
- Frame rate per second : 30
Color Stream Configuration : 
- Resolution : 1280x720 pixels
- 8-bit BGR
- Frame rate per second : 30

ROI size : 30x30 pixels


## Pre-processing steps for measurement experiment :
- Plug the camera to the pc with the USBC to USBA cable
- If measurements need to be done on only one of the platforms, run the code "1ROI_RealSense.py" and it will open a display camera viewer.  
- Position the cursor where the region of interest should be (tracking marker) and click. 
- Once the ROI is selected, the camera starts recording the depth measurements.
- When the motion is over, press "esc" button on keyboard and the code will generate two files :
            - time_arrayYYYYMMDD_HHMMSS which contains timestamp data acquired by the camera
            - dist_arrayYYYYMMDD_HHMMSS which contains depth measurements        
- If measurements need to be done on both of the platforms, run the code "2ROI_RealSense.py" and a display camera viewer will appear.
-Position the cursor where the 1D region of interest should be (1D tracking marker) and click. Position the cursor where the 6D region of interest should be (6D tracking marker) and click.
- once the two ROIs are selected, the camera starts recording the depth measurements.
- When the motions are over, press "esc" button on keyboard and the code will genereates three files :
              - time_arrayYYYYMMDD_HHMMSS which contains timestamp data acquired
              - dist_array1DYYYYMMDD_HHMMSS which contains 1D depth measurements (1st ROI selected)
              - dist_array6DYYYYMMDD_HHMMSS which contains 6D depth measurements (2nd ROI selected)
  
