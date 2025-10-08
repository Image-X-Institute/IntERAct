function [traces6DTime, tracesAP] = read6Dfile(filename, dimension)
    fileID = fopen(filename, 'r');
    
    traces6D = textscan(fileID, '%f %f %f %f %f %f %f');

    fclose(fileID);
    
    traces6DTime = traces6D{1}; 
    tracesAP = traces6D{dimension};    
end
