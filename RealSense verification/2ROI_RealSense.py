import cv2
import numpy as np
import socket
import struct
import time
from realsense_depth import DepthCamera
import matplotlib.pyplot as plt
from datetime import datetime
import os
 
def substract_secondes(hour1, hour2):
    h1, minutes1, secondes1, millisecondes1 = map(int, hour1.split(':'))
    h2, minutes2, secondes2, millisecondes2 = map(int, hour2.split(':'))
    total_secondes1 = h1 * 3600 + minutes1 * 60 + secondes1 + millisecondes1 / 1000
    total_secondes2 = h2 * 3600 + minutes2 * 60 + secondes2 + millisecondes2 / 1000
    difference_secondes = total_secondes1 - total_secondes2
    return difference_secondes

import cv2
import numpy as np
import socket
import struct
import time
from realsense_depth import DepthCamera
 
# Function to get average distance from a specified ROI in the depth frame
def get_average_distance(roi, depth_frame):
    x, y, w, h = roi
    roi_depth = depth_frame[y:y+h, x:x+w]
    if np.isnan(roi_depth).any():
        return 0  # Return a default value when encountering NaN
    else:
        return np.mean(roi_depth)
 
# Mouse event to specify the points for two ROIs
def show_distance(event, x, y, args, params):
    global points, roi_selected
    if event == cv2.EVENT_LBUTTONDOWN:
        if len(points) < 2:
            points.append((x, y))
            if len(points) == 2:
                roi_selected = True
 
points = []  # List to store selected points for two ROIs
roi_selected = False  # Flag to indicate if two ROIs are selected
 
# Initialize depth camera
dc = DepthCamera()
 
# Set up mouse callback for selecting ROI
cv2.namedWindow("Color frame")
cv2.setMouseCallback("Color frame", show_distance)
 
# Initialize variables
roi = ()  # ROI coordinates
roi_selected = False  # Flag to indicate if ROI is selected
start_time = time.time()
initial_time = start_time
#time_delay = 0.1  # 10Hz frame rate
dist_array_1 = [] # Store depth measurements from ROI 1
dist_array_2 = [] # Store depth measurements from ROI 2
time_array = []
time_array_sensor = []
frame_array = []
frame_number = 0
count = 0

dist_1_multi = []
dist_2_multi = []

while True:
    # Get depth frame from the depth camera
    ret, depth_frame, color_frame = dc.get_frame()
    # ret, depth_frame, color_frame = dc.get_frame()
 
    # Draw rectangles for selected ROIs if two ROIs are selected
    img = color_frame.copy()
    if roi_selected:
        for i, point in enumerate(points):
            cv2.rectangle(img, point, (point[0] + 30, point[1] + 30), (255, 0, 0), 2)
            cv2.putText(img, f"ROI {i+1}", (point[0], point[1] - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 0, 0), 2)
 
        # Calculate depth measurements for each ROI
        distance1 = get_average_distance((points[0][0], points[0][1], 30, 30), depth_frame)
        distance2 = get_average_distance((points[1][0], points[1][1], 30, 30), depth_frame)
        
        elapsed_time = time.time() - start_time

        time_array.append(elapsed_time)
        # For Lidar 515 camera
        distance1 = distance1 /4 
        distance2 = distance2 /4

        dist_array_1.append(distance1)
        dist_array_2.append(distance2)
        # Print distance readings on the screen
        cv2.putText(img, f"ROI 1 Distance: {distance1:.2f} mm", (10, 20), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 255, 255), 2)
        cv2.putText(img, f"ROI 2 Distance: {distance2:.2f} mm", (10, 40), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 255, 255), 2)

        count += 1

    # Display the depth frame
    cv2.imshow("Color frame", img)
 
    key = cv2.waitKey(1)
 
    # Break the loop if 'esc' key is pressed
    if key == 27:
        break

# Release resources
cv2.destroyAllWindows()


time_reference = time_array[0]

#Shift the distance values to 0 as a minimum
min_dist1 = np.min(dist_array_1)
min_dist2 = np.min(dist_array_2)

for k in range(len(dist_array_1)):
    dist_array_1[k] = dist_array_1[k] - min_dist1

for k in range(len(dist_array_2)):
    dist_array_2[k] = dist_array_2[k] - min_dist2

for k in range(len(time_array)):
    time_array[k] = time_array[k] - time_reference


#Read the file that aknowledge the starting of the 1D motion
with open('starting_timestamp1D.txt', 'r') as file1D:
    time_motion1D = file1D.readline().strip()


#Read the file that aknowledge the starting of the 6D motion
directory_path = r"C:\Users\Robot\source\repos\Image-X-Institute\Multi-tracking"
filename = max([f for f in os.listdir(directory_path) if f.startswith("Log_")], key=lambda x: os.path.getmtime(os.path.join(directory_path, x)))
file_path = os.path.join(directory_path, filename)
with open(file_path, 'r') as file:
    time_motion6D = file.read()

delta_time = substract_secondes(time_motion6D, time_motion1D)
print(delta_time)


timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")

filename_time_array1 = f"time_array{timestamp}.txt"
filename_dist_array1D = f"dist_array1D{timestamp}.txt"
filename_dist_array6D = f"dist_array6D{timestamp}.txt"


 

with open(filename_time_array1, 'w') as file:
    for k in range(len(time_array)):
        file.write(str(time_array[k]) + "\n")  
  

with open(filename_dist_array1D, 'w') as file:
    for k in range(len(time_array)):
        file.write(str(dist_array_1[k]) + "\n")  

with open(filename_dist_array6D, 'w') as file:
    for k in range(len(time_array)):
        file.write(str(dist_array_2[k]) + "\n")  


        