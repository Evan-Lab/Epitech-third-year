#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## neuralnetwork
## File description:
## error_handling
##

import os

def print_error(error) -> int:
    print("Error: {}".format(error))
    return 84

def check_file(argv_number) -> bool:

    if not os.path.isfile(argv_number):
        print_error("Invalid file path")
        return False
    elif os.path.getsize(argv_number) == 0:
        print_error("Empty file")
        return False
    return True

def check_digit(nb) -> bool:

    if not nb.isdigit():
        print_error("Invalid number")
        return False
    return True

def check_arguments(argv, argc) -> bool:

    if (len(argv) < 2):
        print_error("Not enough arguments")
        return False

    first_commands = ["--help, ", "--new", "--load", "--predict"]
    second_commands = ["--save", "--train"]

    if (argv[1] not in first_commands):
        print_error("Invalid command")
        return False

    if (argc > 5):
        if (argv[5] not in second_commands):
            print_error("Invalid command")
            return False

    return True
