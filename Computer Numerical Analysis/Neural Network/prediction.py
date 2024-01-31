#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## neuralnetwork
## File description:
## prediction
##

import numpy as np
from math import *

def sigmoid(x) -> float:
    return 1 / (1 + np.exp(-x))

def sigmoid_derivative(x) -> float:
    return x * (1 - x)

def fen_to_matrix(fen) -> np.ndarray:
    piece_to_num = {'p': -1, 'r': -2, 'n': -3, 'b': -4, 'q': -5, 'k': -6,
                    'P': 1, 'R': 2, 'N': 3, 'B': 4, 'Q': 5, 'K': 6, '.': 0, ' ': 0, 'w': 0, 'b': 0, '-': 0,
                    'a': 0, 'b': 1, 'c': 2, 'd': 3, 'e': 4, 'f': 5, 'g': 6, 'h': 7, 'F': 0, ':': 0, 'E': 0, 'N': 0,
                    }

    matrix = []

    for row in fen.split('/'):
        matrix_row = []
        for char in row:
            if char in piece_to_num:
                matrix_row.append(piece_to_num[char])
            elif char.isdigit():
                matrix_row.extend([0] * int(char))
            else:
                pass
        matrix_row.extend([0] * (8 - len(matrix_row)))
        matrix.append(matrix_row)

    matrix = [row[:8] for row in matrix]
    matrix.extend([[0] * 8] * (8 - len(matrix)))

    return np.array(matrix).flatten()