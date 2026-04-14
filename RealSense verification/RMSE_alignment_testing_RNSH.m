% =========================================================================
% RealSense 1DoF Platform Verification Script
% =========================================================================
%
% EXPERIMENTAL PROTOCOL (RNSH, Thursday 19 March)
% -------------------------------------------------
% 1. SETUP
%    - Assemble 1DoF motion platform with wooden block load.
%    - Mount RealSense D415 camera on tripod; position to view platform.
%
% 2. CAMERA WARM-UP & STATIC DRIFT RECORDING
%    - Power on RealSense camera.
%    - Begin static recording (before camera has warmed up).
%    - Record for 25-30 minutes to characterise thermal drift.
%    - This establishes a baseline for any systematic offset in measurements.
%
% 3. MOTION TRACE RECORDINGS (each trace ~5 minutes)
%    - For each trace, run the motion platform using the prepared Python
%      acquisition script.
%    - At the end of each trace, enter the output filename into this script
%      (readTimeFile / readDistFile calls) and run to plot and compute the
%      input-output correlation metric.
%    - Traces to run (set active trace below):
%        LIGHT SABR: Regular Motion 1, Large Spike, High Motion,
%                    Erratic, Low Correlation, Regular Motion 2
%        LARK:       Regular Motion 1, Regular Motion 2, Regular Motion 3
%%%%%%%%%%%%%%%%%%% NOTE - LARK REGULAR MOTION 2 TRACE SHOULD BE THIS ONE:
%%%%%%%%%%%%%%%%%%% 'Traces for RTF IEC experiment\new LARK Traces - Feb 2026\LARK_Regular_motion_2_1DoF_resp_trace.txt'
%
% 4. POST-TRACE STATIC RECORDING
%    - After all motion traces are complete, record the static platform
%      again (same duration as warm-up if feasible).
%    - Allows comparison of drift before and after experiment.
%
% OUTPUTS
%    - Time-aligned overlay plot of 1DoF platform input vs RealSense output.
%    - Pearson correlation coefficient, RMSE, mean/std difference,
%      1st/99th percentile difference between input and output signals.
%
% NOTES
%    - RealSense distance is negated on load (camera orientation convention).
%    - Optimal time shift is found by minimising RMSE over [-10, +10] s.
%    - Optimal amplitude (DC) shift is found analytically at each time shift.
%    - See findOptimalShift() below for full alignment algorithm.
%
% TODO: Confirm which RealSense .txt filenames correspond to each trace and
%       populate the readTimeFile / readDistFile calls in Section 1 below.
%       The trace filenames recorded during the experiment need to be matched
%       to the active read1Dfile call for the corresponding input trace.
% =========================================================================

clear; close all;

addpath("C:\Users\akac0297\source\repos\IntERAct\Post processing\")

% Verify required functions are on the path
which read1Dfile
which read6Dfile
which readDistFile
which readTimeFile

% -------------------------------------------------------------------------
% CONFIGURATION
% -------------------------------------------------------------------------

folder       = 'C:\Users\akac0297\source\repos\IntERAct\RealSense verification\';
scale_factor = 1.0;  % Reserved for future amplitude scaling; currently unused

% -------------------------------------------------------------------------
% SECTION 1: LOAD REALSENSE OUTPUT (measured platform motion)
% -------------------------------------------------------------------------
% Uncomment the readTimeFile / readDistFile pair corresponding to the trace
% being analysed. The filename inside strcat() must be filled in once the
% recorded .txt filenames from the experiment are known.

% -------------------------------------------------------------------------
% SECTION 2: LOAD 1DOF PLATFORM INPUT TRACE
% -------------------------------------------------------------------------


% Uncomment the read1Dfile call corresponding to the trace being analysed.
% These are the commanded motion signals sent to the 1DoF platform.

inputTraceFolder_LIGHTSABR = ['C:\Users\akac0297\OneDrive - The University of Sydney (Staff)\' ...
    'Documents\Traces for RTF IEC experiment\new LIGHT SABR Traces - 6 Jan 26\'];

inputTraceFolder_LARK_Jan  = ['C:\Users\akac0297\OneDrive - The University of Sydney (Staff)\' ...
    'Documents\Traces for RTF IEC experiment\new LARK Traces - 6 Jan 26\'];

inputTraceFolder_LARK_Feb  = ['C:\Users\akac0297\OneDrive - The University of Sydney (Staff)\' ...
    'Documents\Traces for RTF IEC experiment\new LARK Traces - Feb 2026\'];
% Static recording (warm-up drift)
trace = 'Static test at RNSH (warm-up draft)';
traces1DRealSenseTime = readTimeFile(strcat(folder, 'time_array20260319_111736.txt'), 0);
traces1DRealSenseDist = -readDistFile(strcat(folder, 'dist_array20260319_111736.txt'), 0);
tracesx=traces1DRealSenseTime;
tracesy=zeros(1,length(traces1DRealSenseDist));

% TEST trace
% % trace = '6DoF sinusoidal test';
% % traces1DRealSenseTime = readTimeFile(strcat(folder, 'time_array20260319_113850.txt'), 0);  % 112705 attempt one - small motion, 113236 similar
% % traces1DRealSenseDist = -readDistFile(strcat(folder, 'dist_array20260319_113850.txt'), 0);
% % [tracesx, tracesy] = read1Dfile('C:\Users\akac0297\OneDrive - The University of Sydney (Staff)\Documents\Traces for RTF IEC experiment\TRACES FOR MAR 2026 EXPT\6DoF_sinusoidal_1DoF_resp_trace.txt');
% 
% % % LIGHT SABR traces
% % trace = 'LIGHT SABR Regular 1';
% % traces1DRealSenseTime = readTimeFile(strcat(folder, 'time_array20260319_120922.txt'), 0);
% % traces1DRealSenseDist = -readDistFile(strcat(folder, 'dist_array20260319_120922.txt'), 0);
% % [tracesx, tracesy] = read1Dfile([inputTraceFolder_LIGHTSABR, 'Regular_motion_1_1DoF_resp_trace.txt']);
% %
% % trace = 'LIGHT SABR Large Spike';
% % traces1DRealSenseTime = readTimeFile(strcat(folder, 'time_array20260319_122229.txt'), 0);
% % traces1DRealSenseDist = -readDistFile(strcat(folder, 'dist_array20260319_122229.txt'), 0);
% % [tracesx, tracesy] = read1Dfile([inputTraceFolder_LIGHTSABR, 'Large_spike_1DoF_resp_trace.txt']);
% %
% % trace = 'LIGHT SABR High Motion';
% % traces1DRealSenseTime = readTimeFile(strcat(folder, 'time_array20260319_123540.txt'), 0);
% % traces1DRealSenseDist = -readDistFile(strcat(folder, 'dist_array20260319_123540.txt'), 0);
% % [tracesx, tracesy] = read1Dfile([inputTraceFolder_LIGHTSABR, 'High_motion_1DoF_resp_trace.txt']);
% %
% % trace = 'LIGHT SABR Erratic';
% % traces1DRealSenseTime = readTimeFile(strcat(folder, 'time_array20260319_122857.txt'), 0);
% % traces1DRealSenseDist = -readDistFile(strcat(folder, 'dist_array20260319_122857.txt'), 0);
% % [tracesx, tracesy] = read1Dfile([inputTraceFolder_LIGHTSABR, 'Erratic_1DoF_resp_trace.txt']);
% % %
% % trace = 'LIGHT SABR Low Correlation';
% % traces1DRealSenseTime = readTimeFile(strcat(folder, 'time_array20260319_121552.txt'), 0);
% % traces1DRealSenseDist = -readDistFile(strcat(folder, 'dist_array20260319_121552.txt'), 0);
% % [tracesx, tracesy] = read1Dfile([inputTraceFolder_LIGHTSABR, 'Regular_motion_2_1DoF_resp_trace.txt']);
% %
% 
% % LARK traces
% trace = 'LARK Regular Motion 1';
% traces1DRealSenseTime = readTimeFile(strcat(folder, 'time_array20260319_114557.txt'), 0);
% traces1DRealSenseDist = -readDistFile(strcat(folder, 'dist_array20260319_114557.txt'), 0);
% [tracesx, tracesy] = read1Dfile([inputTraceFolder_LARK_Jan,  'LARK_Regular_motion_1_1DoF_resp_trace.txt']);
% %
% trace = 'LARK Regular Motion 2';
% traces1DRealSenseTime = readTimeFile(strcat(folder, 'time_array20260319_115444.txt'), 0);
% traces1DRealSenseDist = -readDistFile(strcat(folder, 'dist_array20260319_115444.txt'), 0);
% [tracesx, tracesy] = read1Dfile([inputTraceFolder_LARK_Feb,  'LARK_Regular_motion_2_1DoF_resp_trace.txt']); % Corrected trace - verify before use
% %
% trace = 'LARK Regular Motion 3';
% traces1DRealSenseTime = readTimeFile(strcat(folder, 'time_array20260319_120251.txt'), 0);
% traces1DRealSenseDist = -readDistFile(strcat(folder, 'dist_array20260319_120251.txt'), 0);
% [tracesx, tracesy] = read1Dfile([inputTraceFolder_LARK_Jan,  'LARK_Regular_motion_3_1DoF_resp_trace.txt']);
% %
% % Static recording (post-experiment)
% trace = 'Static test at RNSH (post-experiment)';
% traces1DRealSenseTime = readTimeFile(strcat(folder, 'time_array20260319_124232.txt'), 0);
% traces1DRealSenseDist = -readDistFile(strcat(folder, 'dist_array20260319_124232.txt'), 0);
% tracesx=traces1DRealSenseTime;
% tracesy=zeros(1,length(traces1DRealSenseDist));

% -------------------------------------------------------------------------
% SECTION 3: ALIGNMENT & CORRELATION
% -------------------------------------------------------------------------

[shift1D, shift1D_y, stats] = findOptimalShift( ...
    tracesx, tracesy, ...
    traces1DRealSenseTime, traces1DRealSenseDist, ...
    trace);

interp_output = traces1DRealSenseDist - shift1D_y;

% -------------------------------------------------------------------------
% SECTION 4: PEAK / TROUGH ANALYSIS
% -------------------------------------------------------------------------

[input_pks,  input_pk_locs]  = findpeaks( tracesy);
[output_pks, output_pk_locs] = findpeaks( interp_output);

[input_trgs,  input_trg_locs]  = findpeaks(-tracesy);
[output_trgs, output_trg_locs] = findpeaks(-interp_output);

% Truncate to equal lengths for paired comparison
len = min(length(input_trgs), length(input_pks));
input_trgs    = input_trgs(1:len);
input_pks     = input_pks(1:len);
input_trg_locs  = input_trg_locs(1:len);
input_pk_locs   = input_pk_locs(1:len);
input_diff    = abs(input_pks - input_trgs);

len = min(length(output_trgs), length(output_pks));
output_trgs   = output_trgs(1:len);
output_pks    = output_pks(1:len);
output_trg_locs = output_trg_locs(1:len);
output_pk_locs  = output_pk_locs(1:len);
output_diff   = abs(output_pks - output_trgs);

% -------------------------------------------------------------------------
% SECTION 5: PLOT INPUT vs OUTPUT
% -------------------------------------------------------------------------

figure;
hold on;
plot(tracesx, tracesy, ...
    'DisplayName', '1DoF platform input');
scatter(traces1DRealSenseTime - shift1D, traces1DRealSenseDist - shift1D_y, ...
    'filled', 'DisplayName', '1DoF platform output (RealSense)');
xlabel('Time (s)');
ylabel('Displacement (mm)');
legend('show');
title(['1DoF Platform Motion: ', trace]);
grid on;
set(gca, 'FontSize', 12, 'LineWidth', 1.5);
hold off;


% =========================================================================
% LOCAL FUNCTIONS
% =========================================================================

function [timeShift, amplitudeShift, stats] = findOptimalShift( ...
        dataHexaTimestamps, dataHexaY, ...
        dataKIMTimestamps,  dataKIMYCent, trace)
% FINDOPTIMALSHIFT  Find the time and amplitude shift minimising RMSE
%   between the 1DoF platform input and RealSense output signals.
%
%   The function performs a grid search over time shifts in [-10, +10] s
%   (step 0.05 s) and, at each time shift, analytically finds the DC
%   amplitude offset that minimises RMSE. Pearson correlation and
%   percentile-based difference statistics are also reported.

    % Remove duplicate timestamps in the input (hexa/platform) signal
    [uniqueHexaTimestamps, uniqueIndices] = unique(dataHexaTimestamps);
    uniqueHexaY = dataHexaY(uniqueIndices);

    % Grid search parameters
    timeShiftValues = -10:0.05:10;
    numTimeShifts   = length(timeShiftValues);

    % Pre-allocate
    rmseValues             = inf(numTimeShifts, 1);
    optimalAmplitudeShifts = zeros(numTimeShifts, 1);
    meanDiff  = zeros(numTimeShifts, 1);
    stdDiff   = zeros(numTimeShifts, 1);
    perc1Diff = zeros(numTimeShifts, 1);
    perc99Diff = zeros(numTimeShifts, 1);
    corrs = zeros(numTimeShifts, 1);
    pvals = zeros(numTimeShifts, 1);

    for i = 1:numTimeShifts

        % Apply candidate time shift to the output (RealSense) signal
        shiftedKIMTimestamps = dataKIMTimestamps - timeShiftValues(i);
        validIndices = shiftedKIMTimestamps >= min(dataHexaTimestamps) & ...
                       shiftedKIMTimestamps <= max(dataHexaTimestamps);

        if ~any(validIndices)
            continue;
        end

        shiftedKIMTimestamps = shiftedKIMTimestamps(validIndices);
        shiftedKIMYCent      = dataKIMYCent(validIndices);

        % Interpolate input signal at shifted output timestamps
        interpHexaY = interp1(uniqueHexaTimestamps, uniqueHexaY, ...
                              shiftedKIMTimestamps, 'linear', 'extrap');

        % Grid search over DC amplitude shift
        minAmpShift = min(shiftedKIMYCent - interpHexaY);
        maxAmpShift = max(shiftedKIMYCent - interpHexaY);
        amplitudeShiftRange = minAmpShift:0.01:maxAmpShift;

        rmseAmplitude = inf;
        for amplitudeShift = amplitudeShiftRange
            adjustedKIMYCent = shiftedKIMYCent - amplitudeShift;
            tempRmse = sqrt(mean((interpHexaY - adjustedKIMYCent).^2));
            if tempRmse < rmseAmplitude
                rmseAmplitude = tempRmse;
                optimalAmplitudeShifts(i) = amplitudeShift;
            end
        end
        rmseValues(i) = rmseAmplitude;

        % Statistics at the best amplitude shift for this time shift
        adjustedKIMYCent = shiftedKIMYCent - optimalAmplitudeShifts(i);
        difference = adjustedKIMYCent - interpHexaY;

        meanDiff(i)   = mean(difference);
        stdDiff(i)    = std(difference);
        perc1Diff(i)  = prctile(difference, 1);
        perc99Diff(i) = prctile(difference, 99);

        [corr_coef, pval] = corrcoef(adjustedKIMYCent, interpHexaY);
        corrs(i) = abs(corr_coef(1,2));
        pvals(i) = pval(1,2);
    end

    % Select the time shift that minimises RMSE
    [~, optimalIndex] = min(rmseValues);
    timeShift      = timeShiftValues(optimalIndex);
    amplitudeShift = optimalAmplitudeShifts(optimalIndex);

    % Package output statistics
    stats.meanDiff  = meanDiff(optimalIndex);
    stats.stdDiff   = stdDiff(optimalIndex);
    stats.perc1Diff = perc1Diff(optimalIndex);
    stats.perc99Diff = perc99Diff(optimalIndex);
    stats.cor  = corrs(optimalIndex);
    stats.pval = pvals(optimalIndex);

    % Console summary
    fprintf('--- Trace: %s ---\n', trace);
    fprintf('  Optimal time shift:       %.3f s\n',  timeShift);
    fprintf('  Optimal amplitude shift:  %.3f mm\n', amplitudeShift);
    fprintf('  Mean difference:          %.3f mm\n', stats.meanDiff);
    fprintf('  Std deviation:            %.3f mm\n', stats.stdDiff);
    fprintf('  1st percentile diff:      %.3f mm\n', stats.perc1Diff);
    fprintf('  99th percentile diff:     %.3f mm\n', stats.perc99Diff);
    fprintf('  Pearson r:                %.4f\n',    stats.cor);
    fprintf('  p-value:                  %.4g\n',    stats.pval);
end

function result = rmse(x, y)
% RMSE  Root mean square error between vectors x and y.
    result = sqrt(sum((x - y).^2) / length(x));
end