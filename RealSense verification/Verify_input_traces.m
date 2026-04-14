% =========================================================================
% Plot Input Traces for Visual Identification
% =========================================================================
%
% PURPOSE
%   Run this script BEFORE the experiment (or at any time) to inspect the
%   commanded 1DoF platform input traces. Use the plots to confirm which
%   traces you want to test during the RealSense verification experiment,
%   and to identify which filenames correspond to which motion type.
%
% USAGE
%   Just run the script. All available traces are loaded and plotted in
%   separate figures with labelled titles.
% =========================================================================

clear; close all;

% -------------------------------------------------------------------------
%% FOLDER PATHS
% -------------------------------------------------------------------------

folderLIGHTSABR = ['C:\Users\akac0297\OneDrive - The University of Sydney (Staff)\' ...
    'Documents\Traces for RTF IEC experiment\new LIGHT SABR Traces - 6 Jan 26\'];

folderLARK_Jan  = ['C:\Users\akac0297\OneDrive - The University of Sydney (Staff)\' ...
    'Documents\Traces for RTF IEC experiment\new LARK Traces - 6 Jan 26\'];

folderLARK_Feb  = ['C:\Users\akac0297\OneDrive - The University of Sydney (Staff)\' ...
    'Documents\Traces for RTF IEC experiment\new LARK Traces - Feb 2026\'];

folderLIGHTSABR_new = ['C:\Users\akac0297\OneDrive - The University of Sydney (Staff)\' ...
        'Documents\Traces for RTF IEC experiment\new LIGHT SABR Traces - Feb 26\'];

addpath("C:\Users\akac0297\source\repos\IntERAct\Post processing\")

% -------------------------------------------------------------------------
%% TRACE DEFINITIONS
% -------------------------------------------------------------------------
% Each entry: {display label, full file path}

traces = {
    'LIGHT SABR – Regular Motion 1',  [folderLIGHTSABR, 'Regular_motion_1_1DoF_resp_trace.txt'];
    'LIGHT SABR – Large Spike',        [folderLIGHTSABR, 'Large_spike_1DoF_resp_trace.txt'];
    'LIGHT SABR – High Motion',        [folderLIGHTSABR, 'High_motion_1DoF_resp_trace.txt'];
    'LIGHT SABR – Erratic',            [folderLIGHTSABR, 'Erratic_1DoF_resp_trace.txt'];
    'LIGHT SABR – Regular Motion 2',   [folderLIGHTSABR, 'Regular_motion_2_1DoF_resp_trace.txt'];
    'LARK – Regular Motion 1',         [folderLARK_Jan,  'LARK_Regular_motion_1_1DoF_resp_trace.txt'];
    'LARK – Regular Motion 2 (Feb)',   [folderLARK_Feb,  'LARK_Regular_motion_2_1DoF_resp_trace.txt'];  % Corrected trace
    'LARK – Regular Motion 2 (INCORRECT)',   [folderLARK_Jan,  'LARK_Regular_motion_2_1DoF_resp_trace.txt.txt'];  % INCORRECT trace
    'LIGHT SABR – Regular Motion 1 (Feb)',   [folderLIGHTSABR_new,  'Regular_motion_1_1DoF_resp_trace.txt'];  % INCORRECT trace - large initial offset from 0
    'LARK – Regular Motion 3',         [folderLARK_Jan,  'LARK_Regular_motion_3_1DoF_resp_trace.txt'];
};

% -------------------------------------------------------------------------
%% PLOT ALL TRACES
% -------------------------------------------------------------------------

nTraces = size(traces, 1);

for k = 1:nTraces
    label    = traces{k, 1};
    filepath = traces{k, 2};

    if ~isfile(filepath)
        warning('File not found, skipping: %s', filepath);
        continue;
    end

    [x, y] = read1Dfile(filepath);

    figure('Name', label, 'NumberTitle', 'off');
    plot(x, y, 'LineWidth', 1.2);
    xlabel('Time (s)');
    ylabel('Displacement (mm)');
    title(label);
    grid on;
    set(gca, 'FontSize', 12, 'LineWidth', 1.5);
end

fprintf('Plotted %d traces. Check figures to confirm which traces to use.\n', nTraces);