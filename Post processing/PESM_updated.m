%% PESM_updated.m
% Authors: Alicja Kaczynska, Chris Kuban
% Purpose: Code to generate figures of the input and output (measured)
% motion traces in the testing of the IntERAct combined 6DoF and 1DoF
% motion platform as tested in the laboratory and the clinic.

clear; close all;

figure_no = '5'; % <-- user input ('5' or '6')
fig_no = '5b'; % <---- user input ('5a', '5b', '6a', or '6b')
addpath("C:\Users\akac0297\source\repos\IntERAct\Post processing\") % <--- user input

%% FIGURE 6
if strcmp(figure_no,'6')
    if strcmp(fig_no, '6a')
        %%% Figure 6a - lung mean motion 1
        oneD_resp_file = "Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 1\1.Mean_Motion_resp_shifted_rescaled_gradual_start.txt";
        time_file = "Figure 6 - Table 3\LIGHT SABR Lung Mean Motion 1\time_array1.Mean_Motion.txt";
        oneD_dist_file = "Figure 6 - Table 3\LIGHT SABR Lung Mean Motion 1\dist_array1.Mean_Motion.txt";
        sixD_dist_file = "Figure 6 - Table 3\LIGHT SABR Lung Mean Motion 1\MotionData_1.Mean_Motion_gradual_start_030724-0553.txt";
        sixD_input_file = "Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 1\1.Mean_Motion_gradual_start.txt";
    elseif strcmp(fig_no, '6b')
        %% Figure 6b - liver mean motion 1
        oneD_resp_file ="Figure 5 - Table 2\LARK Liver Mean Motion 1\3.Mean_motion_1_092_resp_shifted_rescaled_gradual_start.txt";
        time_file = "Figure 6 - Table 3\LARK Liver Mean Motion 1\time_array3.Mean_Motion.txt";
        oneD_dist_file = "Figure 6 - Table 3\LARK Liver Mean Motion 1\dist_array3.Mean_Motion.txt";
        sixD_dist_file = "Figure 6 - Table 3\LARK Liver Mean Motion 1\MotionData_3.Mean_motion_1_092_gradual_start_030724-0603.txt";
        sixD_input_file = "Figure 5 - Table 2\LARK Liver Mean Motion 1\3.Mean_motion_1_092_gradual_start.txt";
    end

    [tracesx, tracesy] = read1Dfile(oneD_resp_file);
    
    traces1DRealSenseTime1 = readTimeFile(time_file, 0);
    traces1DRealSenseDist1 = readDistFile(oneD_dist_file, 0);
    [traces6DTimeInput, traces6DAPInput] = read6Dfile(sixD_input_file, 4);
    [traces6DTimeOutput, traces6DAPOutput] = read6Dfile(sixD_dist_file, 3);
    
    a = 0;
    b = 500;
    
    ind_input = (traces6DTimeInput >= a) & (traces6DTimeInput <= b);
    traces6DTimeInput_filtered = traces6DTimeInput(ind_input);
    traces6DAPInput_filtered = traces6DAPInput(ind_input);
    
    a = 0;
    b = 500;
    ind_output = (traces6DTimeOutput >= a) & (traces6DTimeOutput <= b);
    traces6DTimeOutput_filtered = traces6DTimeOutput(ind_output);
    traces6DAPOutput_filtered = traces6DAPOutput(ind_output);
    
    if strcmp(fig_no,'6a')
        % for fig 6a:
        traces1DRealSenseTime1=traces1DRealSenseTime1(3021:5273)-traces1DRealSenseTime1(3021);
        traces1DRealSenseDist1=traces1DRealSenseDist1(3021:5273);
    
        tracesx = tracesx(1:263);
        tracesy = tracesy(1:263);
    elseif strcmp(fig_no,'6b')
        % for fig 6b:
        traces1DRealSenseTime1=traces1DRealSenseTime1(5750:7050)-traces1DRealSenseTime1(5750);
        traces1DRealSenseDist1=traces1DRealSenseDist1(5750:7050);
    
        tracesx = tracesx(1:263);
        tracesy = tracesy(1:263);
        %
    end
    [shift1D,shift1D_y] = findOptimalShift(tracesx, tracesy,traces1DRealSenseTime1, traces1DRealSenseDist1,-10,10);
    [shift6D, shift6D_y] = findOptimalShift(traces6DTimeInput, traces6DAPInput, traces6DTimeOutput_filtered, traces6DAPOutput_filtered, 0,10);
    
    a = 0;
    b = length(tracesx);
    
    ind_input = (tracesx >= a) & (tracesx <= b);
    tracesx_filtered = tracesx(ind_input);
    tracesy_filtered = tracesy(ind_input);
    
    X = traces1DRealSenseTime1 - shift1D;
    
    a = 0;
    b = length(traces1DRealSenseDist1);
    
    ind_output = (X >=a) & (X <= b);
    traces1DRealSenseTime1_filtered = X(ind_output);
    traces1DRealSenseDist1_filtered = traces1DRealSenseDist1(ind_output);
    
    figure(1);
    
    inputColor = [0 0.4470 0.7410];  
    outputColor1 = [0.9290 0.6940 0.1250];  
    outputColor2 = [0.9290 0.6940 0.1250];  
    
    subplot(2,1,2)
    hold on;
    p1 = plot(tracesx_filtered, tracesy_filtered, 'DisplayName', 'Input', 'Color', inputColor);
    s1 = scatter(traces1DRealSenseTime1_filtered, traces1DRealSenseDist1_filtered - shift1D_y, 'filled', ...
                 'DisplayName', 'Measured output', 'MarkerFaceColor', outputColor1);
    
    grid off;
    xlabel('Time (s)', 'FontSize', 5);
    % Compute 1DoF velocity
    dt1D = diff(tracesx_filtered);
    dy1D = diff(tracesy_filtered);
    velocity1D = dy1D ./ dt1D;
    velocityTime1D = tracesx_filtered(1:end-1);
    
    % Overlay velocity on right y-axis
    yyaxis right
    plot(velocityTime1D, velocity1D, 'DisplayName', 'Input velocity');
    ylabel('Velocity (mm/s)',Color='k');
    ax1 = gca;       % Get current axes
    ax1.YAxis(2).Color = 'k'; % Set right y-axis color to black
    
    
    % Restore left y-axis
    yyaxis left
    
    if strcmp(fig_no,'6a')
        %%% fig 6a
        xlim([5,40]);
        ylim([-1,8]);
        yticks(-1:3:8);
    elseif strcmp(fig_no,'6b')
        % fig 6b
        xlim([15,50]);
        ylim([-2,13]);
        yticks(-1:5:14);
    end
    
    title('1DoF Platform');
    legend show;
    set(gca, 'FontSize', 24, 'LineWidth', 1.5, 'FontName', 'Times New Roman');
    hold off;
    
    subplot(2,1,1)
    hold on;
    p2 = plot(traces6DTimeInput_filtered, traces6DAPInput_filtered, 'DisplayName', 'Input', 'Color', inputColor);
    s2 = scatter(traces6DTimeOutput_filtered, traces6DAPOutput_filtered, 'filled', ...
                 'DisplayName', 'Measured output', 'MarkerFaceColor', outputColor2);
    
    annotation('textbox', [0.04, 0.5, 0, 0], 'string', 'Displacement (mm)', ...
               'FontSize', 24, 'FontName', 'Times New Roman', ...
               'EdgeColor', 'none', 'HorizontalAlignment', 'center', ...
               'VerticalAlignment', 'middle', 'Rotation', 90);
    
    % Compute 6DoF velocity
    dt6D = diff(traces6DTimeInput_filtered);
    dy6D = diff(traces6DAPInput_filtered);
    velocity6D = dy6D ./ dt6D;
    velocityTime6D = traces6DTimeInput_filtered(1:end-1);
    
    % Overlay velocity on right y-axis
    yyaxis right
    plot(velocityTime6D, velocity6D, 'DisplayName', 'Input velocity');
    ylabel('Velocity (mm/s)', Color='k');
    ax2 = gca;       % Get current axes
    ax2.YAxis(2).Color = 'k'; % Set right y-axis color to black
    
    % Restore left y-axis
    yyaxis left
    grid off
    
    if strcmp(fig_no,'6a')
        %%% fig 6a
        xlim([5,40]);
        ylim([-5,1]);
        yticks(-5:2:1);
    elseif strcmp(fig_no,'6b')
        % fig 6b
        xlim([15,50]);
        ylim([-11,4]);
        yticks(-11:5:4);
    end
    
    title('6DoF Platform');
    legend show;
    set(gca, 'FontSize', 24, 'LineWidth', 1.5, 'FontName', 'Times New Roman');
    hold off;

%% FIGURE 5
elseif strcmp(figure_no, '5')
    if strcmp(fig_no,'5a')
        % %%% Figure 5a - lung mean motion 1
        oneD_resp_file = "Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 1\1.Mean_Motion_resp_shifted_rescaled_gradual_start.txt";
        time_file = "Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 1\Test 2\time_array20240722_141350.txt";
        oneD_dist_file = "Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 1\Test 2\dist_array1D20240722_141350.txt";
        sixD_dist_file = "Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 1\Test 2\dist_array6D20240722_141350.txt";
        sixD_input_file = "Figure 5 - Table 2\LIGHT_SABR Lung Mean Motion 1\1.Mean_Motion_gradual_start.txt";
    elseif strcmp(fig_no, '5b')
        % Figure 5b - liver mean motion 1
        oneD_resp_file = "Figure 5 - Table 2\LARK Liver Mean Motion 1\3.Mean_motion_1_092_resp_shifted_rescaled_gradual_start.txt";
        time_file = "Figure 5 - Table 2\LARK Liver Mean Motion 1\Test 2\time_array20240722_145136.txt";
        oneD_dist_file = "Figure 5 - Table 2\LARK Liver Mean Motion 1\Test 2\dist_array1D20240722_145136.txt";
        sixD_dist_file = "Figure 5 - Table 2\LARK Liver Mean Motion 1\Test 2\dist_array6D20240722_145136.txt";
        sixD_input_file = "Figure 5 - Table 2\LARK Liver Mean Motion 1\3.Mean_motion_1_092_gradual_start.txt";
    end
    
    [tracesx, tracesy] = read1Dfile(oneD_resp_file);
    
    traces1DRealSenseTime1 = readTimeFile(time_file, 0);
    traces1DRealSenseDist1 = readDistFile(oneD_dist_file, 0);
    
    traces6DRealSenseTime1 = traces1DRealSenseTime1;
    traces6DRealSenseDist1 = readDistFile(sixD_dist_file, 0);
    
    [traces6DTimeInput, traces6DAPInput] = read6Dfile(sixD_input_file,4);
    
    [shift1D,shift1D_y] = findOptimalShift(tracesx, tracesy,traces1DRealSenseTime1, traces1DRealSenseDist1, 0,10);
    [shift6D, shift6D_y] = findOptimalShift(traces6DTimeInput, traces6DAPInput, traces6DRealSenseTime1, traces6DRealSenseDist1, 0,10);
    latency = shift6D - shift1D;
    disp("latency: " + latency);
    
    outputColor2 = [0.9290 0.6940 0.1250];
    
    figure(1);
    subplot(2,1,2)
    hold on;
    plot(tracesx, tracesy,'DisplayName', 'Input' );
    scatter(traces1DRealSenseTime1 - shift1D, traces1DRealSenseDist1 - shift1D_y, 'filled', 'DisplayName', 'Measured output', 'MarkerFaceColor', outputColor2);
    
    % Compute 1DoF velocity
    dt1D = diff(tracesx);
    dy1D = diff(tracesy);
    velocity1D = dy1D ./ dt1D;
    velocityTime1D = tracesx(1:end-1);
    
    % Overlay velocity on right y-axis
    yyaxis right
    plot(velocityTime1D, velocity1D, 'DisplayName', 'Input velocity');
    ylabel('Velocity (mm/s)', 'Color', 'k');
    ax1 = gca;
    ax1.YAxis(2).Color = 'k';
    
    % Restore left y-axis
    yyaxis left
    
    legend show;
    if strcmp(fig_no, '5a')
        xlim([5,40]);
        ylim([-1,8]);
        yticks(-1:3:8);
    elseif strcmp(fig_no, '5b')
        xlim([15,50]);
        ylim([-2,13]);
        yticks(-1:5:14);
    end
    
    xlabel('Time (s)', 'FontSize', 10);
    title('1DoF Platform');
    grid off;
    set(gca, 'FontSize', 24, 'LineWidth', 1.5, 'FontName', 'Times New Roman');
    
    hold off;
    
    subplot(2,1,1)
    hold on;
    plot(traces6DTimeInput, traces6DAPInput, 'DisplayName', 'Input');
    scatter(traces6DRealSenseTime1 - shift6D, traces6DRealSenseDist1 - shift6D_y, 'filled', 'DisplayName', 'Measured output', 'MarkerFaceColor', outputColor2);
    
    % Compute 6DoF velocity
    dt6D = diff(traces6DTimeInput);
    dy6D = diff(traces6DAPInput);
    velocity6D = dy6D ./ dt6D;
    velocityTime6D = traces6DTimeInput(1:end-1);
    
    % Overlay velocity on right y-axis
    yyaxis right
    plot(velocityTime6D, velocity6D, 'DisplayName', 'Input velocity');
    ylabel('Velocity (mm/s)', 'Color', 'k');
    ax2 = gca;
    ax2.YAxis(2).Color = 'k';
    
    % Restore left y-axis
    yyaxis left
    
    annotation('textbox', [0.04, 0.5, 0, 0], 'string', 'Displacement (mm)', ...
               'FontSize', 24, 'FontName', 'Times New Roman', ...
               'EdgeColor', 'none', 'HorizontalAlignment', 'center', ...
               'VerticalAlignment', 'middle', 'Rotation', 90);
    
    legend show;
    title('6DoF Platform');
    grid off;
    if strcmp(fig_no, '5a')
        xlim([5,40]);
        ylim([-7,2]);
        yticks(-7:3:2);
    elseif strcmp(fig_no, '5b')
        xlim([15,50]);
        ylim([-11,4]);
        yticks(-11:5:4);
    end
    
    set(gca, 'FontSize', 24, 'LineWidth', 1.5, 'FontName', 'Times New Roman');
    
    hold off;
end

function [timeShift, amplitudeShift] = findOptimalShift(dataHexaTimestamps, dataHexaY, dataKIMTimestamps, dataKIMYCent, x, y)
    [uniqueHexaTimestamps, uniqueIndices] = unique(dataHexaTimestamps);
    uniqueHexaY = dataHexaY(uniqueIndices);
    % Define the range of time shifts to test
    timeShiftValues = x:0.01:y;
    numTimeShifts = length(timeShiftValues);
    
    % Initialize RMSE values for time and amplitude shifts
    rmseValues = inf(numTimeShifts, 1);
    optimalAmplitudeShifts = zeros(numTimeShifts, 1);

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
    end

    % Find the optimal time shift
    [~, optimalIndex] = min(rmseValues);
    timeShift = timeShiftValues(optimalIndex);
    amplitudeShift = optimalAmplitudeShifts(optimalIndex);
end
