function [tracesx, tracesy] = read1Dfile(filename)
    fileID = fopen(filename, 'r');
    traces = textscan(fileID, '%f %f');
    fclose(fileID);

    tracesx = traces{1};
    tracesy = traces{2};

    min_traces_input = min(tracesy);

    tracesy = tracesy - min_traces_input;
end
