##
## EPITECH PROJECT, 2023
## neuralnetwork
## File description:
## load_file
##

import numpy as np
import sys
from prediction import fen_to_matrix
from error_handling import check_file

def load_data_from_file(file_path) -> (np.ndarray, np.ndarray):

    X, y = [], []

    if check_file(file_path) == False:
        sys.exit(84)

    with open(file_path, 'r') as file:
        for line in file:
            if line.strip() == '':
                continue
            if line.startswith('FEN:'):
                fen = line.split(' ')[1].strip()
                X.append(fen_to_matrix(fen))
            elif line.startswith('RES:'):
                result = line.split(' ')[1].strip()
                if result == '1/2-1/2':
                    y.append([0, 0.5])
                elif result == '0-1':
                    y.append([0, 1])
                elif result == '1-0':
                    y.append([1, 0])

    return np.array(X), np.array(y)
