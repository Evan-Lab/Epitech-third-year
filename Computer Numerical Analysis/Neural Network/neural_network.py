#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## neuralnetwork
## File description:
## neural_network
##

import numpy as np
import json
from prediction import sigmoid, sigmoid_derivative

class NeuralNetwork (object):

    def __init__(self, input_size, hidden_size, output_size, loss) -> None:

        self.weights1 = np.random.rand(hidden_size, input_size)
        self.weights2 = np.random.rand(output_size, hidden_size)
        self.bias1 = np.random.rand(hidden_size, 1)
        self.bias2 = np.random.rand(output_size, 1)
        self.loss_function = self.get_loss_function(loss)
        self.loss_history = []

    def feedforward(self, X) -> np.ndarray:

        self.hidden = sigmoid(np.dot(self.weights1, X) + self.bias1)
        output = sigmoid(np.dot(self.weights2, self.hidden) + self.bias2)
        return output

    def get_loss_function(self, loss) -> np.ndarray:
        if loss == "mse":
            return self.mean_squared_error

    def mean_squared_error(self, output, y) -> float:
        return 0.5 * np.sum((output - y) ** 2)

    def train(self, X, y, epochs=1000, learning_rate=0.01) -> None:

        progress = []

        for epoch in range(epochs) :
            output = self.feedforward(X)
            loss = self.loss_function(output, y)
            self.loss_history.append(loss)
            mse = np.mean(np.square(y - output))
            progress.append({'epoch': epoch, 'mse': mse})

            if (epoch + 1) % 20 == 0:
                print(f"Epoch: {epoch + 1}/{epochs}, Loss: {loss}")

            output_error = y - output
            output_delta = output_error * sigmoid_derivative(output)
            hidden_error = np.dot(self.weights2.T, output_delta)
            hidden_delta = hidden_error * sigmoid_derivative(self.hidden)

            self.weights2 += learning_rate * np.dot(output_delta, self.hidden.T)
            self.bias2 += learning_rate * output_delta
            self.weights1 += learning_rate * np.dot(hidden_delta, X.T)
            self.bias1 += learning_rate * hidden_delta

        with open('training_progress.json', 'w') as file:
            json.dump(progress, file)

    def predict (self, X) -> np.ndarray:

        return self.feedforward(X)

    def save(self, filename):
        data = {
            "input_size": len(self.weights1[0]),
            "hidden_size": len(self.weights1),
            "output_size": len(self.weights2),
            "weights1": self.weights1.tolist(),
            "weights2": self.weights2.tolist(),
            "bias1": self.bias1.tolist(),
            "bias2": self.bias2.tolist()
        }
        with open(filename, 'w') as f:
            json.dump(data, f)

    def load(filename):
        with open(filename, 'r') as f:
            data = json.load(f)
        input_size = 64
        hidden_size = len(data["bias1"])
        output_size = len(data["weights2"])
        network = NeuralNetwork(input_size, hidden_size, output_size, 'mse')
        network.weights1 = np.array(data["weights1"])
        network.weights2 = np.array(data["weights2"])
        network.bias1 = np.array(data["bias1"])
        network.bias2 = np.array(data["bias2"])
        return network
