function [traces6DTime, tracesAP] = read6Dfile(filename)
    % Ouvrir le fichier
    fileID = fopen(filename, 'r');
    
    % Lire les données du fichier avec 'textscan'
    traces6D = textscan(fileID, '%f %f %f %f %f %f %f');
    % Fermer le fichier
    fclose(fileID);
    
    % Extraire les colonnes nécessaires
    traces6DTime = traces6D{1}; % Première colonne
    tracesAP = traces6D{4};     % Quatrième colonne
end
