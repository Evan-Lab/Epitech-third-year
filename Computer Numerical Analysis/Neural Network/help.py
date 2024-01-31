#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## neuralnetwork
## File description:
## help
##

def display_help() -> None:
    print("USAGE")
    print("\t./my_torch [--new\tIN_LAYER [HIDDEN_LAYERS...] OUT_LAYER | --load LOADFILE]")
    print("\t[--train | --predict] [--save SAVEFILE] FILE\n")
    print("DESCRIPTION")
    print("\t--new\tCreates a new\tneural network with random weights.")
    print("Each subsequent number represents the number of neurons on each layer, from left")
    print("to right. For example, ./my_torch --new 3 4 5 will create a neural network with")
    print("an input layer of 3 neurons, a hidden layer of 4 neurons, and an output layer of 5")
    print("neurons.")
    print("\t--load\tLoads an existing neural network from LOADFILE.")
    print("\t--train\tLaunches the neural network in training mode. Each board in FILE")
    print("must contain inputs to send to the neural network, as well as the expected output.")
    print("\t--predict\tLaunches the neural network in prediction mode. Each board in FILE")
    print("must contain inputs to send to the neural network, and optionally an expected")
    print("\toutput.")
    print("\t--save\tSave neural network internal state into SAVEFILE.")
    print("\tFILE\tFILE containing chessboards")
