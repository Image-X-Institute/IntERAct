import seaborn as sns
import matplotlib.pyplot as plt
import numpy as np
from scipy import stats
from math import sqrt
from tabulate import tabulate
from sklearn import preprocessing

file_input = r''
file_output = r''

# Ratio of output trajectory to input trajectory as in the trace analysis may not run the whole trace
ratio = 1
# Time deviation adjustment of input and output trajectories
offset = 0.13

# name the image with input file name
filename = file_input.split('\\')[-1].split('.')[0]

name = filename

# name = "LungTrace_MeanComplexity"


with open(file_input) as f:
    groups_in = f.read().rstrip().split("\n")
    data_in = [list(map(float, (group.split(' ')))) for group in groups_in]


with open(file_output) as f:
    groups_out = f.read().rstrip().split("\n")
    # print(groups_out[:3])
    groups_out.pop(0)
    # print(groups_out[:3])
    data_out = [list(map(float, (group.split(' ')))) for group in groups_out]

# data_out = data_out[16:]
time_in = [data[0] for data in data_in]
time_out = [data[0] for data in data_out]

analysis_result = []

end = int(len(time_in) * ratio)

if ratio == 1:
    time_in_real = time_in
else:
    time_in_real = time_in[:end]
# time_out = preprocessing.minmax_scale(time_out, (time_in_real[0],time_in_real[-1]))

# print(time_in_real[:20])
# print(len(time_in_real))
# print(time_out[:20])
# print(len(time_out))

t_in=0
t_out = 0
index = []

# while t_out < len(time_out) and t_in < len(time_in_real):
# # while t_out < 20 and t_in < 20:
#     # print(t_in,t_out)
#     diff = time_out[t_out] - time_in_real[t_in]
#     if abs(diff) < 0.08:
#         # print("t_out", t_out)
#         # print("t_in",t_in)
#         index.append(t_out)
#         t_in += 1
#         t_out += 1
#     elif diff > 0:
#         t_in += 1
#     elif diff < 0:
#         t_out += 1
#     # print(t_out)
# print("length of output time:", len(time_out))
# print("length of input time:", len(time_in_real))
# print("length of found match:", len(index))

# align the time before calculating the difference
time_distance = np.zeros((len(time_in_real),len(time_out)))

time_out_aligned = [time-offset for time in time_out]
for i in range(len(time_in_real)) :
    for j in range(len(time_out)):
        time_distance[i,j] = abs(time_in_real[i] - time_out_aligned[j])

print(len(time_distance[10]))
print(len(time_distance))
found = [[],[]]
# for i in range(len(time_in_real)):
#     print(np.where(distance == np.min(distance,axis=0)))
#     # found.append([i,])distance[i]

# found = np.where(time_distance == np.min(time_distance,axis=1))
for i in range(len(time_distance)):
    found[0].append(i)
    found[1].append(np.argmin(time_distance[i]))
    
# print("match condition")
# print(len(found))
# print(found)
# print(found[0][100:200])
# print(len(found[0]))
# print(len(found[1]))

# print(found[0])
# print(found[1])

# find the match point in input trace and out put trace by looking for the closet pair
found_match = list(zip(found[0],found[1]))

found_set = [[],[]]
for i in range(len(found_match)):
    if (not found_match[i][0] in found_set[0]) and not (found_match[i][1] in found_set[1]):
        found_set[0].append(found_match[i][0])
        found_set[1].append(found_match[i][1])
    else:
        if found_match[i][0] in found_set[0]:
            index = found_set[0].index(found_match[i][0])
        elif found_match[i][1] in found_set[1]:
            index = found_set[1].index(found_match[i][1])
        in1,out1 = found_set[0][index], found_set[1][index]
        in2,out2 = found_match[i][0], found_match[i][1]
        diff1 = abs(time_in_real[in1] - time_out_aligned[out1])
        diff2 = abs(time_in_real[in2] - time_out_aligned[out2])

        if diff1 < diff2:
            pass
        else:
            found_set[0][index] = in2
            found_set[1][index] = out2

# print(found_set)
# print(len(found_set[1]))
# print(len(found_set[0]))

def standard_diviation(data):
    mean = np.mean(data)
    # print(mean)
    div = [(d - mean)**2 for d in data]
    # print(div)
    std_div = sqrt(sum(div)/len(div))
    return std_div

def confidence_interval(data):
    data_mean = np.mean(data)
    data_std = np.std(data, ddof=1)  # use ddof=1 for sample standard deviation

    # calculate the critical value for a 95% confidence interval
    critical_value = np.percentile(data, [1, 99])

    # calculate the margin of error
    margin_of_error = (critical_value[1] - critical_value[0]) / 2

    # calculate the confidence interval
    conf_int = (data_mean - margin_of_error, data_mean + margin_of_error)
    return conf_int


x_in = [data[1] for data in data_in]
x_out = [data[1] for data in data_out]
x_in_real = x_in[:end]
# print("1",len(x_out))
# print("2",len(x_in_real))
diff_x = []
for i in range(len(found_set[0])):
    diff_x.append(x_out[found_set[1][i]] - x_in_real[found_set[0][i]])


y_in = [data[2] for data in data_in]
y_out = [data[2] for data in data_out]
y_in_real = y_in[:end]
diff_y = []
for i in range(len(found_set[0])):
    diff_y.append(y_out[found_set[1][i]] - y_in_real[found_set[0][i]])


z_in = [data[3] for data in data_in]
z_out = [data[3] for data in data_out]
z_in_real = z_in[:end]
diff_z = []
for i in range(len(found_set[0])):
    diff_z.append(z_out[found_set[1][i]] - z_in_real[found_set[0][i]])


print("Mean value of differences between input and output:")
print("X: ", np.mean(diff_x))
print("Y: ", np.mean(diff_y))
print("Z: ", np.mean(diff_z))

print("Standard deviation of differences between input and output: ")
print("X: ", np.std(diff_x))
print("Y: ", np.std(diff_y))
print("Z: ", np.std(diff_z))

print("1st and 99th Percentile: ")
print("X:", confidence_interval(diff_x))
print("Y:", confidence_interval(diff_y))
print("Z:", confidence_interval(diff_z))
# data = [['Standard Deviation',np.std(xi),np.std(xo),np.std(yi),np.std(yo),np.std(zi),np.std(zo)],
#         ['Mean',np.mean(xi),np.mean(xo),np.mean(yi),np.mean(yo),np.mean(zi),np.mean(zo)]]

# col_names = ['Statistical values (Liver Baseline)','Input X', 'Output X', 'Input Y', 'Output Y', 'Input Z', 'Output Z']
# print(tabulate(data, headers=col_names, tablefmt="fancy_grid"))

# with open ('analysis.txt','a') as f:
#     f.write(name + '\n')
#     for row in analysis_result:
#         f.write(row+'\n')
#     f.write('\n')

# If the input traces have angle, also analyze the angles.

rx_in = [data[4] for data in data_in]
rx_out = [data[4] for data in data_out]
rx_in_real = rx_in[:end]
print("1",len(rx_out))
print("2",len(rx_in_real))
diff_rx = []
for i in range(len(found_set[0])):
    diff_rx.append(rx_out[found_set[1][i]] - rx_in_real[found_set[0][i]])
print("mean different in rotation degree x:",np.mean(diff_rx))
print("Standard deviation of input and output rotation differences(x): ", np.std(diff_rx))
print("95% confidence interval rx: ", confidence_interval(diff_rx))


ry_in = [data[5] for data in data_in]
ry_out = [data[5] for data in data_out]
ry_in_real = ry_in[:end]
print("1",len(ry_out))
print("2",len(ry_in_real))
diff_ry = []
for i in range(len(found_set[0])):
    diff_ry.append(ry_out[found_set[1][i]] - ry_in_real[found_set[0][i]])
print("mean different in rotation degree y:",np.mean(diff_ry))
print("Standard deviation of input and output ratation differences(y): ", np.std(diff_ry))
print("95% confidence interval rotation ry: ", confidence_interval(diff_ry))


rz_in = [data[6] for data in data_in]
rz_out = [data[6] for data in data_out]
rz_in_real = rz_in[:end]
print("1",len(rz_out))
print("2",len(rz_in_real))
diff_rz = []
for i in range(len(found_set[0])):
    diff_rz.append(rz_out[found_set[1][i]] - rz_in_real[found_set[0][i]])
print("mean different in x:",np.mean(diff_rz))
print("Standard deviation of input and output differences(x): ", np.std(diff_rz))
print("95% confidence interval x: ", confidence_interval(diff_rz))

time_in_real = time_in[:end]
x_in_real = x_in[:end]
y_in_real = y_in[:end]
z_in_real = z_in[:end]

time_out_aligned = [time-offset for time in time_out]

# Plot traces
plt.xlabel('Time in sec')
plt.ylabel('Translation in mm')
plt.plot(time_in_real,x_in_real, 'b',label='Input traces x' )
plt.plot(time_out_aligned,x_out, 'b--', label = 'Output traces x')
plt.plot(time_in_real,y_in_real, 'r', label='Input traces y' )
plt.plot(time_out_aligned,y_out, 'r--', label = 'Output traces y')
plt.plot(time_in_real,z_in_real, 'g', label='Input traces z' )
plt.plot(time_out_aligned,z_out, 'g--', label = 'Output traces z')
plt.legend(loc = 'best')
plt.title(name)
plt.savefig(name)
plt.show()


