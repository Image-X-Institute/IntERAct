import cv2
import pyrealsense2
import csv
import numpy as np
from realsense_depth import *
import matplotlib.pyplot as plt
import time



from matplotlib.animation import FuncAnimation
import collections

point = (400,300) # Giving coordination of this point

# Change the ROI area
# Return an average distance from all selected pixels 
def youssef(point, depth_frame):
    dist = 0
    n=0
    # Change the ROI x and y lengths from here
    x = 30
    y = 30
    for i in range(x):
        for j in range(y):
            try:
                dist += depth_frame[point[1]+i,point[0]+j]
            except IndexError:
                return 0  
            n+=1 # Pixel number 
    
    dist = dist/n # Get average distance from all pixels 
    return dist#, (point[1] + x-1), (point[0] + y-1)


# Define the point specified by the cursor 
# Save the coordination information too?
def show_distance(event, x, y, args, params):
    global point
    point = (x,y)
    



dc = DepthCamera()

# Measure the measure of an object defined by the cursor
# Create mouse event
cv2.namedWindow("Colour frame")
cv2.setMouseCallback("Colour frame", show_distance)
dist_array = [] # Store the output distance 
time_array = []
dist_plot = []
count = 0

start_time = time.time()

# For real-time monitoring graph plotting
'''
fieldnames = ["time_x","distance"]
with open('L515_Distance_30_sine_plot.csv','w') as csv_file:
    csv_writer = csv.DictWriter(csv_file,fieldnames=fieldnames)
    csv_writer.writeheader()
'''

while True:


    ret, depth_frame, colour_frame = dc.get_frame()
    #distance_youssef, x, y = youssef(point,depth_frame)
    distance_youssef = youssef(point,depth_frame)

    elapsed_time = time.time() - start_time
    time_array.append(elapsed_time)

    # Print the cursor marker
    cv2.rectangle(colour_frame, point, (point[0]+29,point[1]+29), (0,0,255))

    #distance_youssef, x, y = youssef(point,depth_frame)
    #print(distance_youssef/4)
    # Do count =+1 as the time stamp?
    count += 1
    #timeStamp.append(count)
    dist_array.append(distance_youssef/4) # This variable should be used to give real time monitoring plot
    dist_plot = distance_youssef/4
    
    # For real-time monitoring graph plotting
    '''
    with open('L515_Distance_30_sine_plot.csv','a') as csv_file:
        csv_writer = csv.DictWriter(csv_file,fieldnames=fieldnames)
        info = {
            "time_x":count,
            "distance":dist_plot
            }
        csv_writer.writerow(info)
        print(count,dist_plot) # AS TEST
    '''
    # Print distance on the screen 
    cv2.putText(colour_frame,"{}mm".format(distance_youssef/4),(point[0],point[1]),cv2.FONT_HERSHEY_COMPLEX,1,(0,0,0),2)
    
    
    #cv2.imshow("Depth frame", depth_frame) # Showing the depth frame
    cv2.imshow("Colour frame", colour_frame) # Showing the colour frame
    
    key = cv2.waitKey(1)



    # Press esc to stop the frame
    if key == 27:
        break 
def reject_outliers(data, m=2):
    return data[abs(data - np.mean(data)) < m * np.std(data)]

data_to_save = np.column_stack((time_array, dist_array))
# Saving the measurement to csv file
np.savetxt('L515_Distance_30_sine_13.csv', data_to_save, delimiter=',', header='Time,Distance', comments='')

