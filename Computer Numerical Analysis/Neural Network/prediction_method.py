#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## neuralnetwork
## File description:
## prediction_method
##

import sys
import numpy as np
import random
from neural_network import NeuralNetwork
from prediction import fen_to_matrix
from load_file import load_data_from_file

def print_table(fen_test, predicted_output, advantage, predicted_winner, predicted_outcome, is_checkmate, confidence_checkmate):

    if (is_checkmate == False):
        is_checkmate = "Non"
    else:
        is_checkmate = "Oui"

    print("\n +--------------------------------------------------------+")
    print(" |                        Prédiction                      |")
    print(" +--------------------------------------------------------+\n")
    print(" +---------------------------------------------------------+")
    print(f" | FEN testé: {fen_test.strip()}\t   |")
    print(f" | Taux de confiance de la victoire des noires de {predicted_output[0][0] * 100:.4}%   |")
    print(f" | Taux de confiance de la victoire des blancs de {predicted_output[1][0] * 100:.4}%   |")
    print(f" | Avantage de environ {advantage[0] * 100:.5}% des {predicted_winner}\t\t   |")
    print(f" | Vainqueur prédit : {predicted_winner}\t\t\t\t   |")
    print(f" | Résultat prédit : {predicted_outcome}\t\t\t   |")
    print(f" | Échec et mat prédit ? : {is_checkmate}\t\t\t\t   |")
    print(f" | Confiance de l'échec et mat : {confidence_checkmate:.5f}%\t\t   |")
    print(" +---------------------------------------------------------+")

def prediction_algo() -> None:

    input_size = 64
    hidden_size = 10
    output_size = 2
    fen_lines = []
    file_path = sys.argv[2]
    X_data, y_data = load_data_from_file(file_path)
    nn = NeuralNetwork(input_size, hidden_size, output_size, loss= "mse")

    for i, (X, y) in enumerate(zip(X_data, y_data)):
        nn.train(X.reshape(-1, 1), np.array([y]).reshape(-1, 1), epochs=1000)
        if (i + 1) % 20 == 0:
            print(f"Progression en cours : {i + 1} / {len(X_data)}")

    with open(file_path, "r") as file:
        for line in file:
            if line.startswith("FEN"):
                fen_lines.append(line)

    fen_test = random.choice(fen_lines)
    X_test = fen_to_matrix(fen_test).reshape(-1, 1)
    predicted_output = nn.predict(X_test)
    predicted_winner = "Noirs" if predicted_output[0] > predicted_output[1] else "Blancs"

    if np.all(predicted_output < 0.3):
        predicted_outcome = "Partie de chess sans résultat décisif"
    elif np.all(predicted_output[:2] < 0.5):
        predicted_outcome = "Partie en cours"
        if np.all(predicted_output[:2] < 0.4):
            predicted_outcome = "Pat possible"
    else:
        if np.any(predicted_output[:2] > 0.5):
            predicted_outcome = "Échec et mat"
        else:
            predicted_outcome = "Autre résultat non décisif (abandon, etc.)"

    advantage = abs(predicted_output[0] - predicted_output[1])
    is_checkmate = np.any(predicted_output > 0.5)
    confidence_checkmate = predicted_output.max() * 100 if is_checkmate else 0

    index = fen_test.find("FEN: ")
    if index != -1:
        fen_part = fen_test[index + len("FEN: "):]

    print_table(fen_part, predicted_output, advantage, predicted_winner, predicted_outcome, is_checkmate, confidence_checkmate)

def prediction_algo_from_save(filepath, stockage_table) -> None:

    input_size = stockage_table[0]
    if (input_size != 64):
        print("Input size is not compatble with the save file")
        sys.exit(84)

    output_size = stockage_table[2]
    if (output_size != 2):
        print("Output size is not compatble with the save file")
        sys.exit(84)

    fen_lines = []
    hidden_size = stockage_table[1]
    filepath = sys.argv[4]
    X_data, y_data = load_data_from_file(filepath)
    nn = NeuralNetwork(input_size, hidden_size, output_size, loss= "mse")

    for i, (X, y) in enumerate(zip(X_data, y_data)):
        nn.train(X.reshape(-1, 1), np.array([y]).reshape(-1, 1), epochs=1000)
        if (i + 1) % 20 == 0:
            print(f"Progression en cours : {i + 1} / {len(X_data)}")

    with open(filepath, "r") as file:
        for line in file:
            if line.startswith("FEN"):
                fen_lines.append(line)

    fen_test = random.choice(fen_lines)
    X_test = fen_to_matrix(fen_test).reshape(-1, 1)
    predicted_output = nn.predict(X_test)
    predicted_winner = "Noirs" if predicted_output[0] > predicted_output[1] else "Blancs"

    if np.all(predicted_output < 0.3):
        predicted_outcome = "Partie de chess sans résultat décisif"
    elif np.all(predicted_output[:2] < 0.5):
        predicted_outcome = "Partie en cours"
        if np.all(predicted_output[:2] < 0.4):
            predicted_outcome = "Pat possible"
    else:
        if np.any(predicted_output[:2] > 0.5):
            predicted_outcome = "Échec et mat"
        else:
            predicted_outcome = "Autre résultat non décisif (abandon, etc.)"

    advantage = abs(predicted_output[0] - predicted_output[1])
    is_checkmate = np.any(predicted_output > 0.5)
    confidence_checkmate = predicted_output.max() * 100 if is_checkmate else 0

    index = fen_test.find("FEN: ")
    if index != -1:
        fen_part = fen_test[index + len("FEN: "):]

    print_table(fen_part, predicted_output, advantage, predicted_winner, predicted_outcome, is_checkmate, confidence_checkmate)
    print(f"Input Size taken: {input_size}")
    print(f"Output Size taken: {output_size}")
    print(f"Hidden Size taken: {hidden_size}")

