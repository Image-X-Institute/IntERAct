# Robot Analysis
## Trace and Angle analysis
To prove that the robotic arm can work properly (move according to the input trajectory), we conducted several experiments, inputting different traces and recording the output trace data. For some input trajectories may output only a fraction of the whole trajectory, use the 'ratio' variable in the code to control.
traceAnalysis.py is for analysing the data, plotted the input and output traces, and calculated the mean value, standard deviation and 1st and 99th percentile of the differences between the input and output for the x-axis (LR), y-axis (SI) and z-axis (AP), respectively. 

1. Copy the input traces file path to 'file_input' and the output traces path to 'file_output'
2. 'ratio' is the ratio of output trajectory to input trajectory as in the trace analysis may not run the whole trace.if you run the whole trace then it should be 1 as default.
3. 'offset' is to adjust time deviation of input and output traces.
4. After set thses variables you can run the code.
