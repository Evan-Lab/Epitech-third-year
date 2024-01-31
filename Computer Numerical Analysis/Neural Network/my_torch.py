#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## neuralnetwork
## File description:
## my_torch
##

import sys
from sys import argv
from help import display_help
from error_handling import check_arguments
from prediction_method import prediction_algo
from new_method import new_method, new_method_save_case
from load_method import load_method

if __name__ == "__main__" :

    argc = len(argv)

    if argc == 2 and argv[1] == "--help":
        display_help()
        sys.exit(0)
    if not check_arguments(argv, argc):
        sys.exit(84)

    command = argv[1]

    if (command == "--predict"):
        prediction_algo()
    elif (command == "--new"):
        if (argc == 5):
            new_method(argv, argc)
        elif (argc > 5):
            new_method_save_case(argv, argc)
    elif (command == "--load"):
        load_method(argv, argc)
    sys.exit(0)