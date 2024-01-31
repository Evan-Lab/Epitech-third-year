#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## neuralnetwork
## File description:
## load_method
##

import sys
import json
from neural_network import NeuralNetwork
from error_handling import check_file
from prediction_method import prediction_algo_from_save

def load_method(argv, argc):

    if not check_file(argv[2]):
        sys.exit(84)

    filename = argv[2]
    with open(filename, 'r') as f:
        data = json.load(f)

    input_size = data["input_size"]
    hidden_size = data["hidden_size"]
    output_size = data["output_size"]
    weights1 = data["weights1"]
    weights2 = data["weights2"]
    bias1 = data["bias1"]
    bias2 = data["bias2"]

    stockage_table = []
    stockage_table.append(input_size)
    stockage_table.append(hidden_size)
    stockage_table.append(output_size)
    for i in range(len(weights1)):
        stockage_table.append(weights1[i])
    for i in range(len(weights2)):
        stockage_table.append(weights2[i])
    for i in range(len(bias1)):
        stockage_table.append(bias1[i])
    for i in range(len(bias2)):
        stockage_table.append(bias2[i])

    nnn = NeuralNetwork.load(filename)

    if (argc == 5):
        if (argv[3] == "--predict"):
            filepath = argv[4]
            prediction_algo_from_save(filepath, stockage_table)
