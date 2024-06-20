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

shift1D = findClosestSI(tracesx, tracesy,traces1DRealSenseTime1, traces1DRealSenseDist1);
shift6D = findClosestSI(traces6DTimeInput, traces6DAPInput, traces6DRealSenseTime1, traces6DRealSenseDist1);
latency = shift6D - shift1D;
disp("latency : " + latency);

figure;
subplot(2,1,1)
hold on;
plot(tracesx, tracesy,'DisplayName', '1DoF platform input' );
scatter(traces1DRealSenseTime2 - shift1D2, traces1DRealSenseDist2, 'filled', 'DisplayName', '1DoF platform output');
xlabel('Time (s)');
ylabel('Displacement (mm)');
legend show;
title('1DoF Platform Aligned ');
grid on;
set(gca, 'FontSize', 12, 'LineWidth', 1.5);

hold off;

subplot(2,1,2)
hold on;
plot(traces6DTimeInput, traces6DAPInput,'DisplayName', '6DoF platform input');
scatter(traces6DRealSenseTime2 - shift6D2, traces6DRealSenseDist2, 'filled', 'DisplayName', '6DoF platform output');
xlabel('Time (s)');
ylabel('Displacement (mm)');
legend show;
title('6DoF Platform Aligned ');
grid on;
set(gca, 'FontSize', 12, 'LineWidth', 1.5);

hold off;




function shift = findClosestSI(dataHexaTimestamps, dataHexaY, dataKIMTimestamps, dataKIMYCent)

    shiftValues = 0:0.01:40;
    rmseValues = ones(1, length(shiftValues));;

    for n = 1:length(shiftValues)
        B = dataKIMTimestamps - shiftValues(n);
        validIndices = find(B >= 0 & B <= 299.8);

        dataKIMTimestampsShifted = B(validIndices);
        dataKIMYCentShifted = dataKIMYCent(validIndices);

        interpHexaY = interp1(dataHexaTimestamps, dataHexaY, dataKIMTimestampsShifted);
        rmseValues(n) = rmse(dataKIMYCentShifted, interpHexaY);


    end
    [min_rmse, indice_min] = min(rmseValues);
    shift = shiftValues(indice_min);
    disp("mean : " +  meanDiff(indice_min));
    disp("std : " + stdDiff(indice_min));
    disp("1st perc : " + perc1Diff(indice_min));    
    disp("99th perc : " + perc99Diff(indice_min));
end

function result = rmse(x, y)
    result = sqrt(sum((x - y) .^ 2) / length(x));
end
