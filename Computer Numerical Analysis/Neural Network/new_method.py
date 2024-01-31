#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## neuralnetwork
## File description:
## new_method
##

import sys
from error_handling import check_digit
from neural_network import NeuralNetwork

def new_method(argv, argc) -> None:

    if (argc != 5):
        print("Error: Invalid number of arguments")
        sys.exit(84)

    if not check_digit(argv[2]) or not check_digit(argv[3]) or not check_digit(argv[4]):
        print("Error: Invalid number of arguments")
        sys.exit(84)

    input_size = int(argv[2])
    if (input_size != 64):
        print("Error: Invalid input size")
        sys.exit(84)
    hidden_size = int(argv[3])
    output_size = int(argv[4])
    if (output_size != 2):
        print("Error: Invalid output size")
        sys.exit(84)

    nn = NeuralNetwork(input_size, hidden_size, output_size, loss= "mse")
    print("A new neural network has been created with the following parameters:")
    print(f"Input size: {input_size}")
    print(f"Hidden size: {hidden_size}")
    print(f"Output size: {output_size}")
    sys.exit(0)

def new_method_save_case(argv, argc) -> None:

    if not check_digit(argv[2]) or not check_digit(argv[3]) or not check_digit(argv[4]):
        print("Error: Invalid number of arguments")
        sys.exit(84)

    input_size = int(argv[2])
    if (input_size != 64):
        print("Error: Invalid input size")
        sys.exit(84)
    hidden_size = int(argv[3])
    output_size = int(argv[4])
    if (output_size != 2):
        print("Error: Invalid output size")
        sys.exit(84)

    if (argc != 7):
        print("Error: Invalid number of arguments")
        sys.exit(84)
    else:
        save_filename = argv[6]
        nn = NeuralNetwork(input_size, hidden_size, output_size, loss= "mse")
        nn.save(save_filename)
        print("Neural network created and saved on the file", save_filename)
