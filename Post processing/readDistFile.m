function traces1DRealSenseDist = readDistFile(filename, shift)
    fileID = fopen(filename, 'r');
    traces1DRealSenseDist = fscanf(fileID, '%f');
    fclose(fileID);

    traces1DRealSenseDist = traces1DRealSenseDist - shift;
end