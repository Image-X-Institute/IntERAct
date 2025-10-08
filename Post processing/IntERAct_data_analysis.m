%% IntERAct_data_analysis.m
% Author: Alicja Kaczynska
% Purpose: Code to analyse results from laboratory and clinic experiments 
% testing the performance of the combined 6DoF and 1DoF IntERAct motion 
% platform. Applies RMSE method to align the input motions and output 
% traces and calculates statistical quantifiers (accuracy metrics).

clear; close all;

%% Please replace with the location of this folder on user device
addpath("C:\Users\akac0297\source\repos\IntERAct\Post processing\")

%% Select the trace to analyse
test_locations = {'lab','clinic'};
test_location = test_locations{1}; % <----  User selection
traces = {'LIGHT SABR Regular motion 1 - laboratory', 'LIGHT SABR Regular motion 2 - laboratory', 'LIGHT SABR High motion - laboratory', ...
    'LIGHT SABR Regular complexity - laboratory', 'LIGHT SABR High complexity - laboratory', 'LARK Regular motion 1 - laboratory', ...
    'LARK Regular motion 2 - laboratory', 'LIGHT SABR Regular motion 1 - clinic', 'LIGHT SABR Regular motion 2 - clinic', ...
    'LARK Regular motion 1 - clinic', 'LARK Regular motion 2 - clinic'};
trace = traces{1}; % <-------------------  User selection
disp(strcat(['Selected motion trace: ', trace]));

%% LABORATORY TESTING ANALYSIS
if strcmp(trace, 'LIGHT SABR Regular motion 1 - laboratory')
    oneD_resp_file = 'Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 1\1.Mean_Motion_resp_shifted_rescaled_gradual_start.txt';
    sixD_input_file = "Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 1\1.Mean_Motion_gradual_start.txt";
    % test 1
    folder = 'Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 1\Test 1\';
    time_file1 = strcat([folder, 'time_array20240722_140741.txt']);
    oneD_dist_file1 = strcat([folder, 'dist_array1D20240722_140741.txt']);
    sixD_dist_file1 = strcat([folder, 'dist_array6D20240722_140741.txt']);
    % test 2
    folder = 'Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 1\Test 2\';
    time_file2 = strcat([folder, 'time_array20240722_141350.txt']);
    oneD_dist_file2 = strcat([folder, 'dist_array1D20240722_141350.txt']);
    sixD_dist_file2 = strcat([folder, 'dist_array6D20240722_141350.txt']);
    % test 3
    folder = 'Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 1\Test 3\';
    time_file3 = strcat([folder, 'time_array20240722_142021.txt']);
    oneD_dist_file3 = strcat([folder, 'dist_array1D20240722_142021.txt']);
    sixD_dist_file3 = strcat([folder, 'dist_array6D20240722_142021.txt']);
    SI_index = 4;
elseif strcmp(trace, 'LIGHT SABR Regular motion 2 - laboratory')
    oneD_resp_file = 'Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 2\2.Mean_Motion2_resp_shifted_rescaled_gradual_start.txt';
    sixD_input_file = "Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 2\2.Mean_Motion2_gradual_start.txt";
    % test 1
    folder = 'Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 2\Test 1\';
    time_file1 = strcat([folder, 'time_array20240722_142612.txt']);
    oneD_dist_file1 = strcat([folder, 'dist_array1D20240722_142612.txt']);
    sixD_dist_file1 = strcat([folder, 'dist_array6D20240722_142612.txt']);
    % test 2
    folder = 'Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 2\Test 2\';
    time_file2 = strcat([folder, 'time_array20240722_143239.txt']);
    oneD_dist_file2 = strcat([folder, 'dist_array1D20240722_143239.txt']);
    sixD_dist_file2 = strcat([folder, 'dist_array6D20240722_143239.txt']);
    % test 3
    folder = 'Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 2\Test 3\';
    time_file3 = strcat([folder, 'time_array20240722_143859.txt']);
    oneD_dist_file3 = strcat([folder, 'dist_array1D20240722_143859.txt']);
    sixD_dist_file3 = strcat([folder, 'dist_array6D20240722_143859.txt']);
    SI_index = 4;
elseif strcmp(trace, 'LIGHT SABR High motion - laboratory')
    oneD_resp_file = 'Figure 5 - Table 2\LIGHT SABR Lung High Motion\2.High_Motion_resp.txt';
    sixD_input_file = "Figure 5 - Table 2\LIGHT SABR Lung High Motion\2.High_Motion SI.txt";
    % test 1
    folder = 'Figure 5 - Table 2\LIGHT SABR Lung High Motion\Test 1\';
    time_file1 = strcat([folder, 'time_array20240530_113551.txt']);
    oneD_dist_file1 = strcat([folder, 'dist_array1D20240530_113551.txt']);
    sixD_dist_file1 = strcat([folder, 'dist_array6D20240530_113551.txt']);
    % test 2
    folder = 'Figure 5 - Table 2\LIGHT SABR Lung High Motion\Test 2\';
    time_file2 = strcat([folder, 'time_array20240530_114251.txt']);
    oneD_dist_file2 = strcat([folder, 'dist_array1D20240530_114251.txt']);
    sixD_dist_file2 = strcat([folder, 'dist_array6D20240530_114251.txt']);
    % test 3
    folder = 'Figure 5 - Table 2\LIGHT SABR Lung High Motion\Test 3\';
    time_file3 = strcat([folder, 'time_array20240530_115555.txt']);
    oneD_dist_file3 = strcat([folder, 'dist_array1D20240530_115555.txt']);
    sixD_dist_file3 = strcat([folder, 'dist_array6D20240530_115555.txt']);
    SI_index = 4;
elseif strcmp(trace, 'LIGHT SABR Regular complexity - laboratory')
    oneD_resp_file = 'Figure 5 - Table 2\LIGHT SABR Lung Mean Complexity\3.Mean_Complexity_resp.txt';
    sixD_input_file = "Figure 5 - Table 2\LIGHT SABR Lung Mean Complexity\3.Mean_Complexity SI.txt";
    % test 1
    folder = 'Figure 5 - Table 2\LIGHT SABR Lung Mean Complexity\Test 1\';
    time_file1 = strcat([folder, 'time_array20240530_120522.txt']);
    oneD_dist_file1 = strcat([folder, 'dist_array1D20240530_120522.txt']);
    sixD_dist_file1 = strcat([folder, 'dist_array6D20240530_120522.txt']);
    % test 2
    folder = 'Figure 5 - Table 2\LIGHT SABR Lung Mean Complexity\Test 2\';
    time_file2 = strcat([folder, 'time_array20240530_121249.txt']);
    oneD_dist_file2 = strcat([folder, 'dist_array1D20240530_121249.txt']);
    sixD_dist_file2 = strcat([folder, 'dist_array6D20240530_121249.txt']);
    % test 3
    folder = 'Figure 5 - Table 2\LIGHT SABR Lung Mean Complexity\Test 3\';
    time_file3 = strcat([folder, 'time_array20240530_121952.txt']);
    oneD_dist_file3 = strcat([folder, 'dist_array1D20240530_121952.txt']);
    sixD_dist_file3 = strcat([folder, 'dist_array6D20240530_121952.txt']);
    SI_index = 4;
elseif strcmp(trace, 'LIGHT SABR High complexity - laboratory')
    oneD_resp_file = 'Figure 5 - Table 2\LIGHT SABR Lung High Complexity\4.High_Complexity_resp.txt';
    sixD_input_file = "Figure 5 - Table 2\LIGHT SABR Lung High Complexity\4.High_Complexity SI.txt";
    % test 1
    folder = 'Figure 5 - Table 2\LIGHT SABR Lung High Complexity\Test 1\';
    time_file1 = strcat([folder, 'time_array20240530_134542.txt']);
    oneD_dist_file1 = strcat([folder, 'dist_array1D20240530_134542.txt']);
    sixD_dist_file1 = strcat([folder, 'dist_array6D20240530_134542.txt']);
    % test 2
    folder = 'Figure 5 - Table 2\LIGHT SABR Lung High Complexity\Test 2\';
    time_file2 = strcat([folder, 'time_array20240530_135234.txt']);
    oneD_dist_file2 = strcat([folder, 'dist_array1D20240530_135234.txt']);
    sixD_dist_file2 = strcat([folder, 'dist_array6D20240530_135234.txt']);
    % test 3
    folder = 'Figure 5 - Table 2\LIGHT SABR Lung High Complexity\Test 3\';
    time_file3 = strcat([folder, 'time_array20240530_135903.txt']);
    oneD_dist_file3 = strcat([folder, 'dist_array1D20240530_135903.txt']);
    sixD_dist_file3 = strcat([folder, 'dist_array6D20240530_135903.txt']);
    SI_index = 4;
elseif strcmp(trace, 'LARK Regular motion 1 - laboratory')
    oneD_resp_file = 'Figure 5 - Table 2\LARK Liver Mean Motion 1\3.Mean_motion_1_092_resp_shifted_rescaled_gradual_start.txt';
    sixD_input_file = "Figure 5 - Table 2\LARK Liver Mean Motion 1\3.Mean_motion_1_092_gradual_start.txt";
    % test 1
    folder = 'Figure 5 - Table 2\LARK Liver Mean Motion 1\Test 1\';
    time_file1 = strcat([folder, 'time_array20240722_144516.txt']);
    oneD_dist_file1 = strcat([folder, 'dist_array1D20240722_144516.txt']);
    sixD_dist_file1 = strcat([folder, 'dist_array6D20240722_144516.txt']);
    % test 2
    folder = 'Figure 5 - Table 2\LARK Liver Mean Motion 1\Test 2\';
    time_file2 = strcat([folder, 'time_array20240722_145136.txt']);
    oneD_dist_file2 = strcat([folder, 'dist_array1D20240722_145136.txt']);
    sixD_dist_file2 = strcat([folder, 'dist_array6D20240722_145136.txt']);
    % test 3
    folder = 'Figure 5 - Table 2\LARK Liver Mean Motion 1\Test 3\';
    time_file3 = strcat([folder, 'time_array20240722_145820.txt']);
    oneD_dist_file3 = strcat([folder, 'dist_array1D20240722_145820.txt']);
    sixD_dist_file3 = strcat([folder, 'dist_array6D20240722_145820.txt']);
    SI_index = 4;
elseif strcmp(trace, 'LARK Regular motion 2 - laboratory')
    oneD_resp_file = 'Figure 5 - Table 2\LARK Liver Mean Motion 2\4.Mean_motion_2_111_resp_shifted_rescaled_gradual_start.txt';
    sixD_input_file = "Figure 5 - Table 2\LARK Liver Mean Motion 2\4.Mean_motion_2_111_gradual_start.txt";
    % test 1
    folder = 'Figure 5 - Table 2\LARK Liver Mean Motion 2\Test 1\';
    time_file1 = strcat([folder, 'time_array20240722_150400.txt']);
    oneD_dist_file1 = strcat([folder, 'dist_array1D20240722_150400.txt']);
    sixD_dist_file1 = strcat([folder, 'dist_array6D20240722_150400.txt']);
    % test 2
    folder = 'Figure 5 - Table 2\LARK Liver Mean Motion 2\Test 2\';
    time_file2 = strcat([folder, 'time_array20240722_183209.txt']);
    oneD_dist_file2 = strcat([folder, 'dist_array1D20240722_183209.txt']);
    sixD_dist_file2 = strcat([folder, 'dist_array6D20240722_183209.txt']);
    % test 3
    folder = 'Figure 5 - Table 2\LARK Liver Mean Motion 2\Test 3\';
    time_file3 = strcat([folder, 'time_array20240722_183739.txt']);
    oneD_dist_file3 = strcat([folder, 'dist_array1D20240722_183739.txt']);
    sixD_dist_file3 = strcat([folder, 'dist_array6D20240722_183739.txt']);
    SI_index = 4;
end

if strcmp(test_location, 'lab')
    inputFiles = struct( ...
    'oneD_resp', oneD_resp_file, ...
    'sixD_input', sixD_input_file);

    outputRuns{1}.oneD_dist = oneD_dist_file1;
    outputRuns{1}.sixD_dist = sixD_dist_file1;
    outputRuns{1}.time = time_file1;
    
    outputRuns{2}.oneD_dist = oneD_dist_file2;
    outputRuns{2}.sixD_dist = sixD_dist_file2;
    outputRuns{2}.time = time_file2;
    
    outputRuns{3}.oneD_dist = oneD_dist_file3;
    outputRuns{3}.sixD_dist = sixD_dist_file3;
    outputRuns{3}.time = time_file3;
    psdRunIndex = 2; % Which run to use for PSD comparison
    
    results = runMultiRunMotionAnalysis(inputFiles, outputRuns, psdRunIndex, SI_index, trace);
    disp(results)
end

%% CLINIC TESTING ANALYSIS
if strcmp(trace, 'LIGHT SABR Regular motion 1 - clinic')
    oneD_resp_file = "Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 1\1.Mean_Motion_resp_shifted_rescaled_gradual_start.txt";
    time_file = "Figure 6 - Table 3\LIGHT SABR Lung Mean Motion 1\time_array1.Mean_Motion.txt";
    oneD_dist_file = "Figure 6 - Table 3\LIGHT SABR Lung Mean Motion 1\dist_array1.Mean_Motion.txt";
    sixD_dist_file = "Figure 6 - Table 3\LIGHT SABR Lung Mean Motion 1\MotionData_1.Mean_Motion_gradual_start_030724-0553.txt";
    sixD_input_file = "Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 1\1.Mean_Motion_gradual_start.txt";
    cropIndices = [3020, 5210];
elseif strcmp(trace, 'LIGHT SABR Regular motion 2 - clinic')
    oneD_dist_file = "2.Mean_Motion2\dist_array2.Mean_Motion.txt";
    time_file = "2.Mean_Motion2\time_array2.Mean_Motion.txt";
    sixD_dist_file = "2.Mean_Motion2\MotionData_2.Mean_Motion2_gradual_start_030724-0557.txt";
    oneD_resp_file = "2.Mean_Motion2\2.Mean_Motion2_resp_shifted_rescaled_gradual_start.txt";
    sixD_input_file = "2.Mean_Motion2\2.Mean_Motion2_gradual_start.txt";
    cropIndices = [2400, 3750];
elseif strcmp(trace, 'LARK Regular motion 1 - clinic')
    oneD_resp_file ="Figure 5 - Table 2\LARK Liver Mean Motion 1\3.Mean_motion_1_092_resp_shifted_rescaled_gradual_start.txt";
    time_file = "Figure 6 - Table 3\LARK Liver Mean Motion 1\time_array3.Mean_Motion.txt";
    oneD_dist_file = "Figure 6 - Table 3\LARK Liver Mean Motion 1\dist_array3.Mean_Motion.txt";
    sixD_dist_file = "Figure 6 - Table 3\LARK Liver Mean Motion 1\MotionData_3.Mean_motion_1_092_gradual_start_030724-0603.txt";
    sixD_input_file = "Figure 5 - Table 2\LARK Liver Mean Motion 1\3.Mean_motion_1_092_gradual_start.txt";
    cropIndices = [5750,7050];
elseif strcmp(trace, 'LARK Regular motion 2 - clinic')
    oneD_dist_file = "Figure 6 - Table 3\LARK Liver Mean Motion 2\dist_array4.Mean_Motion.txt";
    time_file = "Figure 6 - Table 3\LARK Liver Mean Motion 2\time_array4.Mean_Motion.txt";
    sixD_dist_file = "Figure 6 - Table 3\LARK Liver Mean Motion 2\MotionData_4.Mean_motion_2_111_gradual_start_030724-0606.txt";
    oneD_resp_file = "Figure 5 - Table 2\LARK Liver Mean Motion 2\4.Mean_motion_2_111_resp_shifted_rescaled_gradual_start.txt";
    sixD_input_file = "Figure 5 - Table 2\LARK Liver Mean Motion 2\4.Mean_motion_2_111_gradual_start.txt";
    cropIndices = [1600, 3000];
end

if strcmp(test_location, 'clinic')
    inputFiles = struct( ...
        'oneD_resp', oneD_resp_file, ...
        'sixD_input', sixD_input_file ...
    );
    outputFiles = struct( ...
        'time', time_file, ...
        'oneD_dist', oneD_dist_file, ...
        'sixD_dist', sixD_dist_file ...
    );
    timeWindow = [0, 2500];
    results = runSingleMotionAnalysis(inputFiles, outputFiles, cropIndices, trace);
    SI_index = 3;
end

%% Diaphragmatic drift analysis
[time1D, disp1D] = read1Dfile(inputFiles.oneD_resp);
[time6D, disp6D] = read6Dfile(inputFiles.sixD_input, SI_index);

threshold_mm = 10;
window_sec = 1;

% Detect rapid shifts
[events1D, numEvents1D] = detectRapidDiaphragmShifts(time1D, disp1D, threshold_mm, window_sec);
[events6D, numEvents6D] = detectRapidDiaphragmShifts(time6D, disp6D, threshold_mm, window_sec);
fprintf('Number of >=10 mm shifts in <=1 s in 1DoF trace: %d\n', numEvents1D);
fprintf('Number of >=10 mm shifts in <=1 s in 6DoF trace: %d\n', numEvents6D);
if ~isempty(events1D)
    totalDuration1D = sum(events1D(:,4));  % column 4 is duration
    fprintf('Total duration of rapid shifts in 1DoF trace: %.1f seconds\n', totalDuration1D);
else
    fprintf('No rapid shifts detected in 1DoF trace.\n');
end

if ~isempty(events6D)
    totalDuration6D = sum(events6D(:,4));  % column 4 is duration
    fprintf('Total duration of rapid shifts in 6DoF trace: %.1f seconds\n', totalDuration6D);
else
    fprintf('No rapid shifts detected in 6DoF trace.\n');
end

%% Functions
function [timeShift, amplitudeShift, stats, difference, adjustedKIMYCent, interpHexaY, shiftedKIMTimestamps, interpHexaTimestamps] = findOptimalShift(dataHexaTimestamps, dataHexaY, dataKIMTimestamps, dataKIMYCent, x, y)
    [uniqueHexaTimestamps, uniqueIndices] = unique(dataHexaTimestamps);
    uniqueHexaY = dataHexaY(uniqueIndices);

    % Define the range of time shifts to test
    timeShiftValues = x:0.05:y;
    numTimeShifts = length(timeShiftValues);

    % Initialize RMSE values for time and amplitude shifts
    rmseValues = inf(numTimeShifts, 1);
    optimalAmplitudeShifts = zeros(numTimeShifts, 1);

    % Initialize statistics
    meanDiff = zeros(numTimeShifts, 1);
    stdDiff = zeros(numTimeShifts, 1);
    perc1Diff = zeros(numTimeShifts, 1);
    perc99Diff = zeros(numTimeShifts, 1);
    minDiff = zeros(numTimeShifts,1);
    maxDiff = zeros(numTimeShifts,1);

    for i = 1:numTimeShifts
        % Apply time shift
        shiftedKIMTimestamps = dataKIMTimestamps - timeShiftValues(i);
        validIndices = find(shiftedKIMTimestamps >= min(dataHexaTimestamps) & shiftedKIMTimestamps <= max(dataHexaTimestamps));

        if isempty(validIndices)
            continue;
        end

        shiftedKIMTimestamps = shiftedKIMTimestamps(validIndices);
        shiftedKIMYCent = dataKIMYCent(validIndices);

        % Interpolate dataHexaY at shiftedKIMTimestamps
        interpHexaY = interp1(uniqueHexaTimestamps, uniqueHexaY, shiftedKIMTimestamps, 'linear', 'extrap');

        % Find the optimal amplitude shift for this time shift
        minAmplitudeShift = min(shiftedKIMYCent - interpHexaY);
        maxAmplitudeShift = max(shiftedKIMYCent - interpHexaY);
        amplitudeShiftRange = minAmplitudeShift:0.01:maxAmplitudeShift;

        rmseAmplitude = inf;
        for amplitudeShift = amplitudeShiftRange
            adjustedKIMYCent = shiftedKIMYCent - amplitudeShift;
            tempRmse = sqrt(mean((interpHexaY - adjustedKIMYCent).^2));

            if tempRmse < rmseAmplitude
                rmseAmplitude = tempRmse;
                optimalAmplitudeShifts(i) = amplitudeShift;
            end
        end

        % Store the minimum RMSE for this time shift
        rmseValues(i) = rmseAmplitude;

        % Calculate statistics for the best amplitude shift at this time shift
        bestAmplitudeShift = optimalAmplitudeShifts(i);
        adjustedKIMYCent = shiftedKIMYCent - bestAmplitudeShift;
        difference = adjustedKIMYCent - interpHexaY;

        meanDiff(i) = mean(difference);
        stdDiff(i) = std(difference);
        perc1Diff(i) = prctile(difference, 1);
        perc99Diff(i) = prctile(difference, 99);
        minDiff(i) = min(difference);
        maxDiff(i) = max(difference);
    end

    % Find the optimal time shift
    [~, optimalIndex] = min(rmseValues);
    timeShift = timeShiftValues(optimalIndex);
    amplitudeShift = optimalAmplitudeShifts(optimalIndex);

    % Output statistics for the optimal shift
    stats.meanDiff = meanDiff(optimalIndex);
    stats.stdDiff = stdDiff(optimalIndex);
    stats.perc1Diff = perc1Diff(optimalIndex);
    stats.perc99Diff = perc99Diff(optimalIndex);
    stats.minDiff = minDiff(optimalIndex);
    stats.maxDiff = maxDiff(optimalIndex);

    % Recompute adjusted KIM and difference vector for optimal shift
    shiftedKIMTimestamps = dataKIMTimestamps - timeShift;
    validIndices = find(shiftedKIMTimestamps >= min(dataHexaTimestamps) & shiftedKIMTimestamps <= max(dataHexaTimestamps));
    shiftedKIMTimestamps = shiftedKIMTimestamps(validIndices);
    shiftedKIMYCent = dataKIMYCent(validIndices);

    interpHexaY = interp1(uniqueHexaTimestamps, uniqueHexaY, shiftedKIMTimestamps, 'linear', 'extrap');
    adjustedKIMYCent = shiftedKIMYCent - amplitudeShift;
    difference = adjustedKIMYCent - interpHexaY;

    % Also return the interpolated Hexa timestamps for completeness
    interpHexaTimestamps = shiftedKIMTimestamps;
end

function resultsStruct = runMultiRunMotionAnalysis(inputFiles, outputRuns, psdRunIndex, SI_index, trace)
    % Load input signals
    [tracesx, tracesy] = read1Dfile(inputFiles.oneD_resp);
    [time6D_in, data6D_in] = read6Dfile(inputFiles.sixD_input, SI_index);  % SI component only

    numRuns = length(outputRuns);
    fs = 10;  % Sampling frequency (Hz)

    % Preallocate
    rmse1D = zeros(1, numRuns);
    rmse6D = zeros(1, numRuns);
    std1D = zeros(1, numRuns);
    std6D = zeros(1, numRuns);
    lag1D = zeros(1, numRuns);
    lag6D = zeros(1, numRuns);
    stats1D = cell(1, numRuns);
    stats6D = cell(1, numRuns);
    diff1D_all = [];
    diff6D_all = [];

    input1D_resampled = cell(1, numRuns);
    output1D_resampled = cell(1, numRuns);
    input6D_resampled = cell(1, numRuns);
    output6D_resampled = cell(1, numRuns);
    time1D_resampled = cell(1, numRuns);
    time6D_resampled = cell(1, numRuns);

    percent1D_under1mm_all = zeros(1, numRuns);
    percent2D_under1mm_all = zeros(1, numRuns);

    for i = 1:numRuns
        % Load output signals
        traces1DTime = readTimeFile(outputRuns{i}.time, 0);
        traces1DDist_output = readDistFile(outputRuns{i}.oneD_dist, 0);
        time6D_out = traces1DTime;
        data6D_out = readmatrix(outputRuns{i}.sixD_dist);

        % Filter input signals (no filtering currently applied)
        ind_in = 1:length(time6D_in);
        time6D_in_filtered = time6D_in(ind_in);
        data6D_in_filtered = data6D_in(ind_in);

        ind_out = 1:length(time6D_out);
        time6D_out_filtered = time6D_out(ind_out);
        data6D_out_filtered = data6D_out(ind_out);

        % Interpolate and align signals
        [~, ~, results1D_temp, diff1D, trace1Doutput, trace1Dinput, time1Dout, time1Din] = ...
            findOptimalShift(tracesx, tracesy, traces1DTime, traces1DDist_output, 0, 40);

        [~, ~, results6D_temp, diff6D, trace6Doutput, trace6Dinput, time6Dout, time6Din] = ...
            findOptimalShift(time6D_in_filtered, data6D_in_filtered, time6D_out_filtered, data6D_out_filtered, 0, 40);

        % Remove duplicate time points
        [time1Din_unique, idx_in1D] = unique(time1Din);
        trace1Dinput_unique = trace1Dinput(idx_in1D);
        [time1Dout_unique, idx_out1D] = unique(time1Dout);
        trace1Doutput_unique = trace1Doutput(idx_out1D);

        [time6Din_unique, idx_in6D] = unique(time6Din);
        trace6Dinput_unique = trace6Dinput(idx_in6D);
        [time6Dout_unique, idx_out6D] = unique(time6Dout);
        trace6Doutput_unique = trace6Doutput(idx_out6D);

        % Resample to uniform grid
        t1D_uniform = max(time1Dout_unique(1), time1Din_unique(1)):1/fs:min(time1Dout_unique(end), time1Din_unique(end));
        input1D_resampled{i} = interp1(time1Din_unique, trace1Dinput_unique, t1D_uniform, 'linear');
        output1D_resampled{i} = interp1(time1Dout_unique, trace1Doutput_unique, t1D_uniform, 'linear');
        time1D_resampled{i} = t1D_uniform;

        t6D_uniform = max(time6Dout_unique(1), time6Din_unique(1)):1/fs:min(time6Dout_unique(end), time6Din_unique(end));
        input6D_resampled{i} = interp1(time6Din_unique, trace6Dinput_unique, t6D_uniform, 'linear');
        output6D_resampled{i} = interp1(time6Dout_unique, trace6Doutput_unique, t6D_uniform, 'linear');
        time6D_resampled{i} = t6D_uniform;

        % Cross-correlation lag
        [cc1D, lags1D] = xcorr(output1D_resampled{i} - mean(output1D_resampled{i}), input1D_resampled{i} - mean(input1D_resampled{i}), 'coeff');
        [~, idxMax1D] = max(cc1D);
        lag1D(i) = lags1D(idxMax1D) / fs;

        [cc6D, lags6D] = xcorr(output6D_resampled{i} - mean(output6D_resampled{i}), input6D_resampled{i} - mean(input6D_resampled{i}), 'coeff');
        [~, idxMax6D] = max(cc6D);
        lag6D(i) = lags6D(idxMax6D) / fs;

        % RMSE and stats
        rmse1D(i) = sqrt(mean(diff1D.^2));
        rmse6D(i) = sqrt(mean(diff6D.^2));
        std1D(i) = std(diff1D);
        std6D(i) = std(diff6D);
        stats1D{i} = results1D_temp;
        stats6D{i} = results6D_temp;

        % Collect for pooled LoA
        diff1D_all = [diff1D_all; trace1Doutput(:) - trace1Dinput(:)];
        diff6D_all = [diff6D_all; trace6Doutput(:) - trace6Dinput(:)];

        % Bland-Altman
        blandAltmanAnalysis(trace1Dinput, trace1Doutput, ['1DoF Run ', num2str(i)]);
        blandAltmanAnalysis(trace6Dinput, trace6Doutput, ['6DoF Run ', num2str(i)]);
       
        % Percentage of time with error <= 1 mm
        percent1D_under1mm = sum(abs(trace1Dinput - trace1Doutput) <= 1) / length(trace1Dinput) * 100;
        percent6D_under1mm = sum(abs(trace6Dinput - trace6Doutput) <= 1) / length(trace6Dinput) * 100;
        
        percent1D_under1mm_all(i) = percent1D_under1mm;
        percent6D_under1mm_all(i) = percent6D_under1mm;
        
        fprintf('\nRun %d: Percentage of time with error <= 1 mm\n', i);
        fprintf('  1DoF platform: %.2f%%\n', percent1D_under1mm);
        fprintf('  6DoF platform: %.2f%%\n', percent6D_under1mm);

        % Percentage of time with error <= 1 mm
        percent1D_under2mm = sum(abs(trace1Dinput - trace1Doutput) <= 2) / length(trace1Dinput) * 100;
        percent6D_under2mm = sum(abs(trace6Dinput - trace6Doutput) <= 2) / length(trace6Dinput) * 100;
        
        percent1D_under2mm_all(i) = percent1D_under2mm;
        percent6D_under2mm_all(i) = percent6D_under2mm;
        
        fprintf('\nRun %d: Percentage of time with error <= 2 mm\n', i);
        fprintf('  1DoF platform: %.2f%%\n', percent1D_under2mm);
        fprintf('  6DoF platform: %.2f%%\n', percent6D_under2mm);
    end

    % Repeatability
    rc1D = 1.96 * sqrt(2) * mean(std1D);
    rc6D = 1.96 * sqrt(2) * mean(std6D);

    % Bland-Altman LoA across all runs
    meanDiff1D = mean(diff1D_all);
    stdDiff1D = std(diff1D_all);
    perc1_1D = prctile(diff1D_all, 1);
    perc99_1D = prctile(diff1D_all, 99);
    LoA1D = [meanDiff1D - 1.96 * stdDiff1D, meanDiff1D + 1.96 * stdDiff1D];

    meanDiff6D = mean(diff6D_all);
    stdDiff6D = std(diff6D_all);
    perc1_6D = prctile(diff6D_all, 1);
    perc99_6D = prctile(diff6D_all, 99);
    LoA6D = [meanDiff6D - 1.96 * stdDiff6D, meanDiff6D + 1.96 * stdDiff6D];
    
    fprintf('\n--- Bland-Altman Summary ---\n');
    fprintf('1DoF Mean Difference: %.3f | LoA: [%.3f, %.3f]\n', meanDiff1D, LoA1D(1), LoA1D(2));
    fprintf('6DoF Mean Difference: %.3f | LoA: [%.3f, %.3f]\n', meanDiff6D, LoA6D(1), LoA6D(2));

    % PSD comparison for selected run
    run = outputRuns{psdRunIndex};
    traces1DTime = readTimeFile(run.time, 0);
    traces1DDist_output = readDistFile(run.oneD_dist, 0);
    [~, ~, ~, ~, trace1Doutput, trace1Dinput, time1Dout, time1Din] = ...
        findOptimalShift(tracesx, tracesy, traces1DTime, traces1DDist_output, -40, 40);

    t_uniform = time1Dout(1):1/fs:time1Dout(end);
    input_resampled1D = interp1(time1Din, trace1Dinput, t_uniform, 'linear');
    output_resampled1D = interp1(time1Dout, trace1Doutput, t_uniform, 'linear');

    run = outputRuns{psdRunIndex};
    traces6DTime = readTimeFile(run.time, 0);
    traces6DDist_output = readDistFile(run.sixD_dist, 0);
    [~, ~, ~, ~, trace6Doutput, trace6Dinput, time6Dout, time6Din] = ...
        findOptimalShift(time6D_in_filtered, data6D_in_filtered, traces6DTime, traces6DDist_output, -40, 40);

    t_uniform = time6Dout(1):1/fs:time6Dout(end);
    input_resampled6D = interp1(time6Din, trace6Dinput, t_uniform, 'linear');
    output_resampled6D = interp1(time6Dout, trace6Doutput, t_uniform, 'linear');

    comparePSD(input_resampled1D, output_resampled1D, input_resampled6D, output_resampled6D, fs, trace)

    % Windowed RMSE
    t1D_common = max(cellfun(@(t) t(1), time1D_resampled)):1/fs:min(cellfun(@(t) t(end), time1D_resampled));
    t6D_common = max(cellfun(@(t) t(1), time6D_resampled)):1/fs:min(cellfun(@(t) t(end), time6D_resampled));

    for i = 1:numRuns
        input1D_common{i} = interp1(time1D_resampled{i}, input1D_resampled{i}, t1D_common, 'linear');
        output1D_common{i} = interp1(time1D_resampled{i}, output1D_resampled{i}, t1D_common, 'linear');
        input6D_common{i} = interp1(time6D_resampled{i}, input6D_resampled{i}, t6D_common, 'linear');
        output6D_common{i} = interp1(time6D_resampled{i}, output6D_resampled{i}, t6D_common, 'linear');
    end

    windowSize = 10 * fs;
    numWindows1D = floor(length(t1D_common) / windowSize);
    numWindows6D = floor(length(t6D_common) / windowSize);
    rmseWindowedMean1D = zeros(numWindows1D, 1);
    rmseWindowedMean6D = zeros(numWindows6D, 1);

    for w = 1:numWindows1D
        idxStart = (w-1)*windowSize + 1;
        idxEnd = w*windowSize;
        rmse_runs = zeros(numRuns, 1);
        for i = 1:numRuns
            rmse_runs(i) = sqrt(mean((input1D_common{i}(idxStart:idxEnd) - output1D_common{i}(idxStart:idxEnd)).^2));
        end
        rmseWindowedMean1D(w) = mean(rmse_runs);
    end

    for w = 1:numWindows6D
        idxStart = (w-1)*windowSize + 1;
        idxEnd = w*windowSize;
        rmse_runs = zeros(numRuns, 1);
        for i = 1:numRuns
            rmse_runs(i) = sqrt(mean((input6D_common{i}(idxStart:idxEnd) - output6D_common{i}(idxStart:idxEnd)).^2));
        end
        rmseWindowedMean6D(w) = mean(rmse_runs);
    end

    % Plot Windowed RMSE
    figure;
    subplot(2,1,1);
    plot(1:numWindows6D, rmseWindowedMean6D, '-o');
    title('Windowed RMSE - 6DoF Platform');
    xlabel('Window Index (10s)');
    ylabel('RMSE (mm)');
    grid on;

    subplot(2,1,2);
    plot(1:numWindows1D, rmseWindowedMean1D, '-o');
    title('Windowed RMSE - 1DoF Platform');
    xlabel('Window Index (10s)');
    ylabel('RMSE (mm)');
    grid on;

    % Save results
    resultsStruct.rmse1D = rmse1D;
    resultsStruct.rmse6D = rmse6D;
    resultsStruct.stats1D = stats1D;
    resultsStruct.stats6D = stats6D;
    resultsStruct.repeatability1D = rc1D;
    resultsStruct.repeatability6D = rc6D;
    resultsStruct.LoA1D = LoA1D;
    resultsStruct.LoA6D = LoA6D;
    resultsStruct.meanDiff1D = meanDiff1D;
    resultsStruct.meanDiff6D = meanDiff6D;
    resultsStruct.stdDiff1D = stdDiff1D;
    resultsStruct.stdDiff6D = stdDiff6D;
    resultsStruct.perc1_1D = perc1_1D;
    resultsStruct.perc99_1D = perc99_1D;
    resultsStruct.perc1_6D = perc1_6D;
    resultsStruct.perc99_6D = perc99_6D;
    resultsStruct.meanRMSE1D = mean(rmse1D);
    resultsStruct.meanRMSE6D = mean(rmse6D);
    resultsStruct.meanCrossCorrLag1D = mean(lag1D);
    resultsStruct.meanCrossCorrLag6D = mean(lag6D);
    resultsStruct.windowedRMSE1D_mean = rmseWindowedMean1D;
    resultsStruct.windowedRMSE6D_mean = rmseWindowedMean6D;

    avgPercent1D_under1mm = mean(percent1D_under1mm_all);
    avgPercent6D_under1mm = mean(percent6D_under1mm_all);
    
    fprintf('\n--- Average Percentage of Time with Error <= 1 mm Across All Runs ---\n');
    fprintf('  6DoF platform: %.2f%%\n', avgPercent6D_under1mm);
    fprintf('  1DoF platform: %.2f%%\n', avgPercent1D_under1mm);

    avgPercent1D_under2mm = mean(percent1D_under2mm_all);
    avgPercent6D_under2mm = mean(percent6D_under2mm_all);
    
    fprintf('\n--- Average Percentage of Time with Error <= 2 mm Across All Runs ---\n');
    fprintf('  6DoF platform: %.2f%%\n', avgPercent6D_under2mm);
    fprintf('  1DoF platform: %.2f%%\n', avgPercent1D_under2mm);
    
end

function resultsStruct = runSingleMotionAnalysis(inputFiles, outputFiles, cropIndices, trace)
    % Load input signal (programmed motion)
    [tracesx, tracesy] = read1Dfile(inputFiles.oneD_resp);
    [time6D_in, data6D_in] = read6Dfile(inputFiles.sixD_input, 4);

    % Load output signal (measured motion)
    traces1DTime = readTimeFile(outputFiles.time, 0);
    traces1DDist_output = readDistFile(outputFiles.oneD_dist, 0);
    [time6D_out, data6D_out] = read6Dfile(outputFiles.sixD_dist, 3);

    % Crop output signals only
    traces1DTime = traces1DTime(cropIndices(1):cropIndices(2)) - traces1DTime(cropIndices(1));
    traces1DDist_output = traces1DDist_output(cropIndices(1):cropIndices(2));

    % Filter 6DoF input and output (not currently implemented)
    ind_in = 1:length(time6D_in);
    time6D_in_filtered = time6D_in(ind_in);
    data6D_in_filtered = data6D_in(ind_in);

    ind_out = 1:length(time6D_out);
    time6D_out_filtered = time6D_out(ind_out);
    data6D_out_filtered = data6D_out(ind_out);

    % 1DoF shift and interpolation
    fprintf('\n--- 1DoF Shift Results ---\n');
    [shift1D, shift1D_y, results1D, diff1D, trace1Doutput, trace1Dinput, time1Dout, time1Din] = findOptimalShift(tracesx, tracesy, traces1DTime, traces1DDist_output, -20, 20);
    disp(results1D);

    % 6DoF shift and interpolation
    fprintf('\n--- 6DoF Shift Results ---\n');
    [shift6D, shift6D_y, results6D, diff6D, trace6Doutput, trace6Dinput, time6Dout, time6Din] = findOptimalShift(time6D_in_filtered, data6D_in_filtered, time6D_out_filtered, data6D_out_filtered, -20, 20);
    disp(results6D);

    % Stats
    rmse1D = sqrt(mean((trace1Dinput-trace1Doutput).^2));
    rmse6D = sqrt(mean((trace6Dinput-trace6Doutput).^2));

    % Percentage of time with error <= 1 mm
    percent1D_under1mm = sum(abs(trace1Dinput - trace1Doutput) <= 1) / length(trace1Dinput) * 100;
    percent6D_under1mm = sum(abs(trace6Dinput - trace6Doutput) <= 1) / length(trace6Dinput) * 100;
    
    fprintf('\nPercentage of time with error <= 1 mm:\n');
    fprintf('  6DoF platform: %.2f%%\n', percent6D_under1mm);
    fprintf('  1DoF platform: %.2f%%\n', percent1D_under1mm);

    % Percentage of time with error <= 2 mm
    percent1D_under2mm = sum(abs(trace1Dinput - trace1Doutput) <= 2) / length(trace1Dinput) * 100;
    percent6D_under2mm = sum(abs(trace6Dinput - trace6Doutput) <= 2) / length(trace6Dinput) * 100;
    
    fprintf('\nPercentage of time with error <= 2 mm:\n');
    fprintf('  6DoF platform: %.2f%%\n', percent6D_under2mm);
    fprintf('  1DoF platform: %.2f%%\n', percent1D_under2mm);

    fs = 10;  % Sampling frequency (Hz)

    % Resample both signals to uniform time grid
    t_uniform = time1Dout(1):1/fs:time1Dout(end);
    input_resampled = interp1(time1Din, trace1Dinput, t_uniform, 'linear');
    output_resampled = interp1(time1Dout, trace1Doutput, t_uniform, 'linear');  % output is already aligned

    % --- Cross-Correlation Lag (1DoF) ---
    [cc1D, lags1D] = xcorr(output_resampled - mean(output_resampled), input_resampled - mean(input_resampled), 'coeff');
    [~, idxMax1D] = max(cc1D);
    lag1D_sec = lags1D(idxMax1D) / fs;
    
    % --- Windowed RMSE (1DoF) ---
    windowSize = 10 * fs;
    numWindows1D = floor(length(input_resampled) / windowSize);
    rmseWindowed1D = zeros(numWindows1D, 1);
    
    for i = 1:numWindows1D
        idxStart = (i-1)*windowSize + 1;
        idxEnd = i*windowSize;
        rmseWindowed1D(i) = sqrt(mean((input_resampled(idxStart:idxEnd) - output_resampled(idxStart:idxEnd)).^2));
    end
    
    fprintf('1DoF Cross-Correlation Lag: %.3f s\n', lag1D_sec);
    fprintf('Windowed RMSE (1DoF):\n');
    disp(rmseWindowed1D);
    
    resultsStruct.crossCorrLag1D = lag1D_sec;
    resultsStruct.windowedRMSE1D = rmseWindowed1D;

    % --- Remove duplicate time entries for input ---
    [time6Din_unique, uniqueIdx_in] = unique(time6Din);
    trace6Dinput_unique = trace6Dinput(uniqueIdx_in);
    
    % --- Remove duplicate time entries for output ---
    [time6Dout_unique, uniqueIdx_out] = unique(time6Dout);
    trace6Doutput_unique = trace6Doutput(uniqueIdx_out);
    
    % --- Resample to uniform 10 Hz grid ---
    t6D_uniform = time6Dout_unique(1):1/fs:time6Dout_unique(end);
    input6D_resampled = interp1(time6Din_unique, trace6Dinput_unique, t6D_uniform, 'linear');
    output6D_resampled = interp1(time6Dout_unique, trace6Doutput_unique, t6D_uniform, 'linear');

    % --- Cross-Correlation Lag (6DoF) ---
    [cc6D, lags6D] = xcorr(output6D_resampled - mean(output6D_resampled), input6D_resampled - mean(input6D_resampled), 'coeff');
    [~, idxMax6D] = max(cc6D);
    lag6D_sec = lags6D(idxMax6D) / fs;
    
    fprintf('6DoF Cross-Correlation Lag: %.3f s\n', lag6D_sec);
    resultsStruct.crossCorrLag6D = lag6D_sec;

        % --- Windowed RMSE (6DoF) ---
    windowSize = 10 * fs;
    numWindows6D = floor(length(input6D_resampled) / windowSize);
    rmseWindowed6D = zeros(numWindows6D, 1);
    
    for i = 1:numWindows6D
        idxStart = (i-1)*windowSize + 1;
        idxEnd = i*windowSize;
        rmseWindowed6D(i) = sqrt(mean((input6D_resampled(idxStart:idxEnd) - output6D_resampled(idxStart:idxEnd)).^2));
    end
    
    fprintf('Windowed RMSE (6DoF):\n');
    disp(rmseWindowed6D);
    resultsStruct.windowedRMSE6D = rmseWindowed6D;

    % Bland-Altman
    fprintf('\n--- Single Run Analysis ---\n');
    fprintf('1DoF RMSE: %.3f\n', rmse1D);
    fprintf('6DoF RMSE: %.3f\n', rmse6D);
    blandAltmanAnalysis(trace1Dinput, trace1Doutput, '1DoF');
    blandAltmanAnalysis(trace6Dinput, trace6Doutput, '6DoF');

    % PSD comparison    
    % Compare PSD

    t_uniform1D = time1Dout(1):1/fs:time1Dout(end);
    input_resampled1D = interp1(time1Din, trace1Dinput, t_uniform1D, 'linear');
    output_resampled1D = interp1(time1Dout, trace1Doutput, t_uniform1D, 'linear');  % output is already aligned

    input_resampled6D = interp1(time6Din_unique, trace6Dinput_unique, t6D_uniform, 'linear');
    output_resampled6D = interp1(time6Dout_unique, trace6Doutput_unique, t6D_uniform, 'linear');  % output is already aligned

    comparePSD(input_resampled1D, output_resampled1D, input_resampled6D, output_resampled6D, fs, trace)

    % Save results
    resultsStruct.rmse1D = rmse1D;
    resultsStruct.rmse6D = rmse6D;
    resultsStruct.stats1D = results1D;
    resultsStruct.stats6D = results6D;

    % Plotting
    figure;

    subplot(2,1,1);
    hold on;
    plot(time6D_in_filtered, data6D_in_filtered, 'DisplayName', 'Input', 'Color', [0 0.4470 0.7410]);
    scatter(time6Dout, trace6Doutput, 'filled', 'DisplayName', 'Measured output', 'MarkerFaceColor', [0.9290 0.6940 0.1250]);
    title('6DoF Platform');
    legend show;
    set(gca, 'FontSize', 16, 'LineWidth', 1, 'FontName', 'Times New Roman');
    hold off;

    subplot(2,1,2);
    hold on;
    plot(tracesx, tracesy, 'DisplayName', 'Input', 'Color', [0 0.4470 0.7410]);
    scatter(time1Dout, trace1Doutput, 'filled', 'DisplayName', 'Measured output', 'MarkerFaceColor', [0.9290 0.6940 0.1250]);
    title('1DoF Platform');
    legend show;
    set(gca, 'FontSize', 16, 'LineWidth', 1, 'FontName', 'Times New Roman');
    hold off;

    % --- Plot Windowed RMSE for 1DoF and 6DoF ---
    figure;
    
    % 6DoF subplot
    subplot(2,1,1);
    hold on;
    plot(1:length(rmseWindowed6D), rmseWindowed6D, '-o');
    title('Windowed RMSE - 6DoF Platform');
    xlabel('Window Index (10-second intervals)');
    ylabel('RMSE (mm)');
    xlim([1, length(rmseWindowed6D)])
    grid on;
    hold off;
    
    % 1DoF subplot
    subplot(2,1,2);
    hold on;
    plot(1:length(rmseWindowed1D), rmseWindowed1D, '-o');
    title('Windowed RMSE - 1DoF Platform');
    xlabel('Window Index (10-second intervals)');
    ylabel('RMSE (mm)');
    xlim([1, length(rmseWindowed1D)])
    grid on;
    hold off;

    % --- Estimate closed-loop bandwidth ---
    n = length(input_resampled);
    f = (0:n/2-1)*(fs/n);
    input_fft = fft(input_resampled);
    output_fft = fft(output_resampled);
    
    gain = abs(output_fft(1:n/2)) ./ abs(input_fft(1:n/2));
    gain_db = 20 * log10(gain);
    
    % Find -3 dB point
    idx = find(gain_db <= -3, 1);
    if ~isempty(idx)
        bandwidth = f(idx);
        fprintf('Estimated -3 dB closed-loop bandwidth: %.2f Hz\n', bandwidth);
    else
        fprintf('Bandwidth could not be determined (no -3 dB drop found).\n');
    end
    
    % Plot gain vs frequency
    figure;
    plot(f, gain_db);
    hold on;
    yline(-3, 'r--', 'DisplayName', '-3 dB Threshold');
    xlabel('Frequency (Hz)');
    ylabel('Gain (dB)');
    title('Closed-loop Frequency Response');
    legend;
    grid on;

    %%% plots
    figure();
    
    inputColor = [0 0.4470 0.7410];  
    outputColor1 = [0.9290 0.6940 0.1250];  
    outputColor2 = [0.9290 0.6940 0.1250];  
    velocityColor = [0.4660 0.6740 0.1880];  % green for velocity
    
    % --- Compute velocity profile for 6DoF input motion ---
    velocity_6D_input = gradient(input6D_resampled, 1/fs);  % mm/s
    % === 6DoF Platform Plot with Velocity Overlay ===
    subplot(2,1,1)
    yyaxis left
    hold on;

    p2 = plot(t6D_uniform, input6D_resampled, 'DisplayName', 'Input', 'Color', inputColor);
    s2 = scatter(t6D_uniform, output6D_resampled, 'filled', ...
                 'DisplayName', 'Measured output', 'MarkerFaceColor', outputColor2);
    ylabel('Displacement (mm)');
    
    annotation('textbox', [0.04, 0.5, 0, 0], 'string', 'Displacement (mm)', ...
               'FontSize', 35, 'FontName', 'Times New Roman', ...
               'EdgeColor', 'none', 'HorizontalAlignment', 'center', ...
               'VerticalAlignment', 'middle', 'Rotation', 90);
    
    yyaxis right
    plot(t6D_uniform, velocity_6D_input, 'Color', velocityColor, ...
         'LineWidth', 1.5, 'DisplayName', 'Input Velocity');
    ylabel('Velocity (mm/s)');
    
    xlabel('Time (s)');
    title('6DoF Platform');
    legend show;
    set(gca, 'FontSize', 32, 'LineWidth', 1.5, 'FontName', 'Times New Roman');
    grid off;
    hold off;
    
    % === 1DoF Platform Plot ===
    subplot(2,1,2)
    yyaxis left

    hold on;

    p1 = plot(t_uniform, input_resampled, 'DisplayName', 'Input', 'Color', inputColor);
    s1 = scatter(t_uniform, output_resampled, 'filled', ...
                 'DisplayName', 'Measured output', 'MarkerFaceColor', outputColor1);
    ylabel('Displacement (mm)');
    grid off;
    
    yyaxis right
    velocity_input = gradient(input_resampled, 1/fs);  % mm/s
    plot(t_uniform, velocity_input+10, 'Color', velocityColor, 'LineWidth', 1.5, 'DisplayName', 'Input Velocity');
    ylabel('Velocity (mm/s)');
    
    xlabel('Time (s)', 'FontSize', 5);
    title('1DoF Platform');
    legend show;
    set(gca, 'FontSize', 32, 'LineWidth', 1.5, 'FontName', 'Times New Roman');
    ylim([min(velocity_input), max(velocity_input)+ 15]);
    hold off;
end

function blandAltmanAnalysis(inputData, outputData, label)
    % Ensure vectors are column vectors
    inputData = inputData(:);
    outputData = outputData(:);

    % Calculate mean and difference
    meanVals = (inputData + outputData) / 2;
    diffVals = outputData - inputData;

    bias = mean(diffVals);
    sd_diff = std(diffVals);
    loa_upper = bias + 1.96 * sd_diff;
    loa_lower = bias - 1.96 * sd_diff;

    % Plot Bland-Altman
    figure;
    scatter(meanVals, diffVals, 'filled');
    hold on;
    yline(bias, 'r-', 'Bias');
    yline(loa_upper, 'g--', '+1.96 SD');
    yline(loa_lower, 'g--', '-1.96 SD');
    xlabel('Mean of Input and Output');
    ylabel('Difference (Output - Input)');
    title(['Bland-Altman Plot: ', label]);
    grid on;
    set(gca, 'FontSize', 14);
    hold off;

    % Print stats
    fprintf('\n--- Bland-Altman Analysis (%s) ---\n', label);
    fprintf('Bias: %.3f\n', bias);
    fprintf('Standard Deviation of Differences: %.3f\n', sd_diff);
    fprintf('95%% Limits of Agreement: [%.3f, %.3f]\n', loa_lower, loa_upper);
end

function comparePSD(inputSignal1D, outputSignal1D, inputSignal6D, outputSignal6D, fs, label)
    % fs: sampling frequency in Hz

    % Detrend signals
    inputSignal1D = detrend(inputSignal1D);
    outputSignal1D = detrend(outputSignal1D);
    inputSignal6D = detrend(inputSignal6D);
    outputSignal6D = detrend(outputSignal6D);

    % Compute PSD using Welch's method
    [pxx_in1D, f_in1D] = pwelch(inputSignal1D, [], [], [], fs);
    [pxx_out1D, f_out1D] = pwelch(outputSignal1D, [], [], [], fs);

    [pxx_in6D, f_in6D] = pwelch(inputSignal6D, [], [], [], fs);
    [pxx_out6D, f_out6D] = pwelch(outputSignal6D, [], [], [], fs);

    % Plot
    fig = figure;
    subplot(2,1,1)
    semilogy(f_in6D, pxx_in6D, 'b', 'DisplayName', 'Input');
    hold on;
    semilogy(f_out6D, pxx_out6D, 'r', 'DisplayName', 'Measured Output');
    xlim([0.05, 1]);  % Focus on respiratory band
    xlabel('Frequency (Hz)');
    %ylabel('Power/Frequency (dB/Hz)');
    title(['PSD Comparison: 6DoF ', label]);
    legend;
    grid on;
    set(gca, 'FontSize', 14);
    ylims = ylim;  % Get current y-axis limits
    fill([0.15 0.4 0.4 0.15], [ylims(1) ylims(1) ylims(2) ylims(2)], ...
        [0.9 0.9 0.9], 'EdgeColor', 'none', 'FaceAlpha', 0.3, 'DisplayName', 'Respiratory Band');
    subplot(2,1,2)
    semilogy(f_in1D, pxx_in1D, 'b', 'DisplayName', 'Input');
    hold on
    semilogy(f_out1D, pxx_out1D, 'r', 'DisplayName', 'Measured Output');
    xlim([0.05, 1]);  % Focus on respiratory band
    xlabel('Frequency (Hz)');
    title(['PSD Comparison: 1DoF ', label]);
    legend;
    grid on;
    set(gca, 'FontSize', 14);
    ylims = ylim;  % Get current y-axis limits
    fill([0.15 0.4 0.4 0.15], [ylims(1) ylims(1) ylims(2) ylims(2)], ...
        [0.9 0.9 0.9], 'EdgeColor', 'none', 'FaceAlpha', 0.3, 'DisplayName', 'Respiratory Band');
    han = axes(fig, 'Visible','off');
    han.YLabel.Visible = 'on';
    ylabel(han,'Power/Frequency (dB/Hz)', 'FontSize',16);

    % compute power in respiratory band
    band = (f_in6D >= 0.15) & (f_in6D <= 0.4);
    power_in_6D = trapz(f_in6D(band), pxx_in6D(band));
    power_out_6D = trapz(f_out6D(band), pxx_out6D(band));
    power_ratio_6D = power_out_6D / power_in_6D;
    fprintf('\n--- PSD Analysis 6DoF (%s) ---\n', label);
    fprintf('Power in 0.15–0.4 Hz band:\n');
    fprintf('Input: %.3f\n', power_in_6D);
    fprintf('Output: %.3f\n', power_out_6D);
    fprintf('Power Ratio: %.3f\n', power_ratio_6D);

    band = (f_in1D >= 0.15) & (f_in1D <= 0.4);
    power_in_1D = trapz(f_in1D(band), pxx_in1D(band));
    power_out_1D = trapz(f_out1D(band), pxx_out1D(band));
    power_ratio_1D = power_out_1D / power_in_1D;
    fprintf('\n--- PSD Analysis 1DoF (%s) ---\n', label);
    fprintf('Power in 0.15–0.4 Hz band:\n');
    fprintf('Input: %.3f\n', power_in_1D);
    fprintf('Output: %.3f\n', power_out_1D);
    fprintf('Power Ratio: %.3f\n', power_ratio_1D);

    fprintf('~=1 means high fidelity, <1 means attenuation, >1 could indicate amplification or noise')
end

function [detectedEvents, numEvents] = detectRapidDiaphragmShifts(timeVec, dispVec, threshold_mm, window_sec)
    if nargin < 3
        threshold_mm = 5;
    end
    if nargin < 4
        window_sec = 0.5;
    end

    fs = 1 / mean(diff(timeVec));  % Estimate sampling frequency
    gapTolerance = round(3);       % Allow up to 3 sample gaps between drift points

    driftIndices = [];
    i = 1;

    while i < length(timeVec)
        start_time = timeVec(i);
        end_idx = find(timeVec >= start_time + window_sec, 1);

        if isempty(end_idx)
            break;
        end

        window_disp = dispVec(i:end_idx);
        disp_range = max(window_disp) - min(window_disp);

        if disp_range >= threshold_mm
            driftIndices = [driftIndices, i];
        end

        i = i + 1;
    end

    % Group drift indices into events
    if isempty(driftIndices)
        detectedEvents = [];
        numEvents = 0;
        return;
    end

    eventStart = driftIndices(1);
    eventIndices = {};
    currentGroup = eventStart;

    for j = 2:length(driftIndices)
        if driftIndices(j) - driftIndices(j-1) <= gapTolerance
            currentGroup = [currentGroup, driftIndices(j)];
        else
            eventIndices{end+1} = currentGroup;
            currentGroup = driftIndices(j);
        end
    end
    eventIndices{end+1} = currentGroup;

    % Convert grouped indices to event info with duration
    detectedEvents = zeros(length(eventIndices), 4);  % [start_time, end_time, disp_range, duration]
    for k = 1:length(eventIndices)
        idxGroup = eventIndices{k};
        start_time = timeVec(idxGroup(1));
        end_time = timeVec(idxGroup(end));
        disp_range = max(dispVec(idxGroup)) - min(dispVec(idxGroup));
        duration = end_time - start_time;
        detectedEvents(k, :) = [start_time, end_time, disp_range, duration];
    end

    numEvents = size(detectedEvents, 1);
end