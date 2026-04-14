# import cv2
# import numpy as np
# import time
# from datetime import datetime
# from realsense_depth_1ROI import DepthCamera  # Updated class above


# import numpy as np

# # def get_average_distance_mm(roi, depth_z16, depth_scale):
# #     x, y, w, h = roi
# #     # Clamp ROI to frame bounds
# #     H, W = depth_z16.shape
# #     x = max(0, min(x, W - 1))
# #     y = max(0, min(y, H - 1))
# #     w = max(1, min(w, W - x))
# #     h = max(1, min(h, H - y))

# #     roi_depth = depth_z16[y:y+h, x:x+w]

# #     # Valid L515 pixels are > 0 (zeros mean invalid/missing)
# #     valid = roi_depth[roi_depth > 0]
# #     if valid.size == 0:
# #         return None, 0.0  # no valid pixels

# #     distance_m = float(valid.mean()) / 4 #* depth_scale
# #     return distance_m / 1000, valid.size / float(w*h)  # (mm, valid_ratio)

# # Function to get average distance from a specified ROI in the depth frame
# def get_average_distance_mm(roi, depth_frame):
#     x, y, w, h = roi
#     roi_depth = depth_frame[y:y+h, x:x+w]
#     if np.isnan(roi_depth).any():
#         return 0  # Return a default value when encountering NaN
#     else:
#         return np.mean(roi_depth) / 1000 # /4 for L515 apparently


# def get_time():
#     Now = datetime.now()
#     return f"{Now.hour}:{Now.minute}:{Now.second}:{Now.microsecond // 1000}"

# def show_distance(event, x, y, flags, params):
#     global roi, roi_selected
#     if event == cv2.EVENT_LBUTTONDOWN:
#         roi = (x, y, 64, 64)
#         roi_selected = True

# roi = ()
# roi_selected = False

# dc = DepthCamera(l515_preset='max_range', laser_power=100.0, confidence=1.0)

# cv2.namedWindow("Color frame")
# cv2.setMouseCallback("Color frame", show_distance)

# dist_array = []
# time_array = []
# count = 0
# start_time = time.time()

# while True:
#     ret, depth_frame, color_frame = dc.get_frame()
#     if not ret:
#         continue

#     # Optional: visualize depth as a quick check (scaled for display)
#     depth_vis = cv2.applyColorMap(
#         cv2.convertScaleAbs(depth_frame, alpha=0.03), cv2.COLORMAP_JET
#     )
#     cv2.imshow("Depth (aligned to color)", depth_vis)

#     if roi_selected:
#         if count == 0:
#             time_clickOnCursor = get_time()

#         x, y, w, h = roi
#         H, W = depth_frame.shape
#         x, y, w, h = max(0, min(x, W-1)), max(0, min(y, H-1)), w, h
#         w = min(w, W - x)
#         h = min(h, H - y)
#         roi = (x, y, w, h)

#         cv2.rectangle(color_frame, (x, y), (x + w, y + h), (0, 255, 0), 2)

#         distance_mm = get_average_distance_mm(roi, depth_frame)
#         #distance_mm, valid_ratio = get_average_distance_mm(roi, depth_frame) #get_average_distance_mm(roi, depth_frame, dc.depth_scale)
#         elapsed_time = time.time() - start_time

#         # Only record if we have valid data
#         if distance_mm is not None:
#             time_array.append(elapsed_time)
#             dist_array.append(distance_mm)
#             count += 1
#             cv2.putText(color_frame,
#                         f"Distance: {distance_mm:.1f} mm", #, (valid: {valid_ratio*100:.0f}%)",
#                         (x, max(20, y - 10)),
#                         cv2.FONT_HERSHEY_SIMPLEX, 0.6, (0, 255, 0), 2)
#         else:
#             cv2.putText(color_frame,
#                         "Distance: --- (no valid depth)",
#                         (x, max(20, y - 10)),
#                         cv2.FONT_HERSHEY_SIMPLEX, 0.6, (0, 0, 255), 2)

#     cv2.imshow("Color frame", color_frame)
#     key = cv2.waitKey(1)
#     if key == 27:
#         break

# # Save if we collected samples
# if len(time_array) > 0 and len(dist_array) > 0:
#     # Normalize to the first measurement
#     ref_time = time_array[0]
#     ref_dist = dist_array[0]
#     time_array = [t - ref_time for t in time_array]
#     dist_array = [d - ref_dist for d in dist_array]

#     timestamp = datetime.now().strftime("%Y%m%d_%H%M%S")
#     np.savetxt('point_measurement.csv',
#                np.column_stack((time_array, dist_array)),
#                delimiter=',', header='Time,Distance(mm relative)', comments='')

#     with open(f"time_array{timestamp}.txt", 'w') as f:
#         for t in time_array:
#             f.write(str(t) + "\n")
#     with open(f"dist_array{timestamp}.txt", 'w') as f:
#         for d in dist_array:
#             f.write(str(d) + "\n")

# cv2.destroyAllWindows()
# dc.release()

import cv2
import numpy as np
import time
from datetime import datetime
from realsense_depth_1ROI import DepthCamera
 
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
        roi = (x, y, 64, 64)
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

        ###
        # Added to test L515
        #distance_z16 = get_average_distance(roi, depth_frame)
        #distance = distance_z16 * dc.depth_scale * 1000  # convert to mm
        ###
        elapsed_time = time.time() - start_time
        time_array.append(elapsed_time)
        count += 1
        #  =================== For Realsense L515 model ======================
        #distance = distance/4
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

