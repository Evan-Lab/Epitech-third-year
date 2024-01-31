#!/usr/bin/env python3
import sys
from sys import argv
from sys import stderr
from math import *
from utils import print_error
from graph import find_degree_of_separation
from read_sort import simple_read
from matrix import relationship_matrix

def find_conspiracies(graph, characters, max_degree, plots):
    relationships_matrix = relationship_matrix(graph, characters, max_degree)
    conspiracies = []

    for i in range(len(plots)):
        if "plotting against" in plots[i]:
            separate = plots[i].split(" is plotting against ")
            character1, character2 = separate[0], separate[1]
            if relationships_matrix[characters.index(character1)][characters.index(character2)] != 'X':
                conspiracies.append(f"{character1} -> {character2}")
    return conspiracies
