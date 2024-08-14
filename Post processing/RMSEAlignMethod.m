addpath('PathFolder')
which read1Dfile
which read6Dfile.m
which readDistFile.m
which readTimeFile.m

[tracesx, tracesy] = read1Dfile("1DInputPathFile");

%The second parameter of these functions is set to shift by a constant
%value t or y coordinates. 

traces1DRealSenseTime1 = readTimeFile("1DTimestampsOutputPathFile", 0);
traces1DRealSenseDist1 = readDistFile("1DisplacementOutputPathFile", 0);

traces6DRealSenseTime1 = readTimeFile("6DTimestampsOutputPathFile", 0);
traces6DRealSenseDist1 = readDistFile("6DDisplacementOututPathFile", 0);

[traces6DTimeInput, traces6DAPInput] = read6Dfile('6DOutputPathFile.txt');

%Call findOptimalShift in order to get the optimal shift for 1D and 6D
%motion traces. The algorithm minimises the RMSE between interpolated input
%and output.



[shift1D, shift1D_y] = findOptimalShift(tracesx, tracesy,traces1DRealSenseTime1, traces1DRealSenseDist1);
[shift6D, shift6D_y] = findOptimalShift(traces6DTimeInput, traces6DAPInput, traces6DRealSenseTime1, traces6DRealSenseDist1);
latency = shift6D - shift1D;
disp("latency : " + latency);

figure;
subplot(2,1,1)
hold on;
plot(tracesx, tracesy,'DisplayName', '1DoF platform input' );
scatter(traces1DRealSenseTime1 - shift1D, traces1DRealSenseDist1 - shift1D_y , 'filled', 'DisplayName', '1DoF platform output');
xlabel('Time (s)');
ylabel('Displacement (mm)');
legend show;
title('1DoF Platform Motion');
grid on;
set(gca, 'FontSize', 12, 'LineWidth', 1.5);

hold off;

subplot(2,1,2)
hold on;
plot(traces6DTimeInput, traces6DAPInput,'DisplayName', '6DoF platform input');
scatter(traces6DRealSenseTime1 - shift6D, traces6DRealSenseDist1 - shift6D_y, 'filled', 'DisplayName', '6DoF platform output');
xlabel('Time (s)');
ylabel('Displacement (mm)');
legend show;
title('6DoF Platform Motion');
grid on;
set(gca, 'FontSize', 12, 'LineWidth', 1.5);

hold off;




function [timeShift, amplitudeShift, stats] = findOptimalShift(dataHexaTimestamps, dataHexaY, dataKIMTimestamps, dataKIMYCent)
    [uniqueHexaTimestamps, uniqueIndices] = unique(dataHexaTimestamps);
    uniqueHexaY = dataHexaY(uniqueIndices);
    % Define the range of time shifts to test
    timeShiftValues = 6:0.01:7;
    numTimeShifts = length(timeShiftValues);
    
    % Initialize RMSE values for time and amplitude shifts
    rmseValues = inf(numTimeShifts, 1);
    optimalAmplitudeShifts = zeros(numTimeShifts, 1);
    
    % Initialize statistics
    meanDiff = zeros(numTimeShifts, 1);
    stdDiff = zeros(numTimeShifts, 1);
    perc1Diff = zeros(numTimeShifts, 1);
    perc99Diff = zeros(numTimeShifts, 1);

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

    % Display the results
    disp(['Optimal Time Shift: ', num2str(timeShift)]);
    disp(['Optimal Amplitude Shift: ', num2str(amplitudeShift)]);
    disp(['Mean Difference: ', num2str(stats.meanDiff)]);
    disp(['Standard Deviation: ', num2str(stats.stdDiff)]);
    disp(['1st Percentile Difference: ', num2str(stats.perc1Diff)]);
    disp(['99th Percentile Difference: ', num2str(stats.perc99Diff)]);
end


function result = rmse(x, y)
    result = sqrt(sum((x - y) .^ 2) / length(x));
end
