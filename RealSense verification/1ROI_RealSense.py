import cv2
import numpy as np
import time
from datetime import datetime
# import pandas as pd
from realsense_depth_1ROI import DepthCamera
import matplotlib.pyplot as plt
from scipy.interpolate import interp1d
import pyrealsense2 as rs
 
def get_time():
    Now = datetime.now()
    hour = str(Now.hour)
    minutes = str(Now.minute)
    secondes = str(Now.second)
    millisecondes = str(Now.microsecond // 1000)
    format_time = hour + ":" + minutes + ":" + secondes + ":" + millisecondes
    return format_time
 
def substract_secondes(hour1, hour2):
    h1, minutes1, secondes1, millisecondes1 = map(int, hour1.split(':'))
    h2, minutes2, secondes2, millisecondes2 = map(int, hour2.split(':'))
    total_secondes1 = h1 * 3600 + minutes1 * 60 + secondes1 + millisecondes1 / 1000
    total_secondes2 = h2 * 3600 + minutes2 * 60 + secondes2 + millisecondes2 / 1000
    difference_secondes = total_secondes1 - total_secondes2
    return difference_secondes
 
# Function to get average distance from a specified ROI in the depth frame
def get_average_distance(roi, depth_frame):
    x, y, w, h = roi
    roi_depth = depth_frame[y:y+h, x:x+w]
    if np.isnan(roi_depth).any():
        return 0  # Return a default value when encountering NaN
    else:
        return np.mean(roi_depth)
 
# Mouse event to specify the ROI
# Change the ROI area here
def show_distance(event, x, y, args, params):
    global roi, roi_selected
    if event == cv2.EVENT_LBUTTONDOWN:
        roi = (x, y, 32, 32)
        roi_selected = True
 
roi = ()  # ROI coordinates
roi_selected = False  # Flag to indicate if ROI is selected
 
# Initialize depth camera
dc = DepthCamera()
 
# Set up mouse callback for selecting ROI
cv2.namedWindow("Color frame")
cv2.setMouseCallback("Color frame", show_distance)
 
dist_array = []
time_array = []
count = 0
 
start_time = time.time()
 
while True:
    # Get depth and color frames from the depth camera
    
    ret, depth_frame, color_frame = dc.get_frame()
    
    # Draw ROI on color frame if selected
    if roi_selected:
       
        if count == 0:
            time_clickOnCursor = get_time()
       
        x, y, w, h = roi
        cv2.rectangle(color_frame, (x, y), (x + w, y + h), (0, 255, 0), 2)
 
        # Calculate depth measurement for ROI
        distance = get_average_distance(roi, depth_frame)
        elapsed_time = time.time() - start_time
        time_array.append(elapsed_time)
        count += 1
        #  =================== For Realsense L515 model ======================
        distance = distance/4
        # ==================== For D415 model, please silent the above code =======================
        # For Realsense D415 model
        dist_array.append(distance)
 
        # Print depth measurement on color frame
        cv2.putText(color_frame, f"Distance: {distance:.2f} mm", (x, y - 10), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (0, 255, 0), 2)
 
    # Display the color frame
    cv2.imshow("Color frame", color_frame)
 
    key = cv2.waitKey(1)
 
    # Break the loop if 'esc' key is pressed
    if key == 27:
        break
 
 
# Save the measurements to a CSV file along with timestamps
data_to_save = np.column_stack((time_array, dist_array))
np.savetxt('point_measurement.csv', data_to_save, delimiter=',', header='Time,Distance', comments='')
# Release resources
cv2.destroyAllWindows()

ref_time = time_array[0]
ref_dist = dist_array[0]

for k in range(len(dist_array)):
    dist_array[k] = dist_array[k] - ref_dist
    time_array[k] = time_array[k] - ref_time

timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")

filename_time_array = f"time_array{timestamp}.txt"
filename_dist_array = f"dist_array{timestamp}.txt"

with open(filename_time_array, 'w') as file:
    for k in range(len(time_array)):
        file.write(str(time_array[k]) + "\n")  

with open(filename_dist_array, 'w') as file:
    for k in range(len(time_array)):
        file.write(str(dist_array[k]) + "\n")  

