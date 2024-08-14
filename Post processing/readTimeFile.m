function traces1DRealSenseTime = readTimeFile(filename, shift)
    fileID = fopen(filename, 'r');
    traces1DRealSenseTime = fscanf(fileID, '%f');
    fclose(fileID);

    traces1DRealSenseTime = traces1DRealSenseTime - shift;
end