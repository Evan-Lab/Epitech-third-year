#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## B-CNA-500-PAR-5-1-graphanalysis-evan.labourdette
## File description:
## main.py
##

import sys
from sys import argv
from sys import stderr
from math import *

## ==============================================================
## =========================[UTILS]==============================
## ==============================================================

def display_help () -> None:
    print("USAGE\n")
    print ("\t./game_of_graphs [--links fr p1 p2 | --plots fr cr n]\n")
    print("")
    print("DESCRIPTION\n")
    print("\tfr\tfile containing friendship relations between people\n")
    print("\tpi\tname of someone in the friendships file\n")
    print("\tcr\tfile containing conspiracies intentions\n")
    print("\tn\tmaximum length of friendship paths\n")

def check_file(argv) -> bool:
    file_content = ''.join(simple_read(argv[2]))
    if not file_content:
        print_error("File is empty")
        sys.exit(84)
    elif "Cersei Lannister" not in file_content:
        print_error("Cersei is not in the file")
        sys.exit(84)
    if (argv[1] == "--plots"):
        file_content = ''.join(simple_read(argv[3]))
        if not file_content:
            print_error("File is empty")
            sys.exit(84)

def print_error(msg: str) -> None:
    return print(f'Error: {msg}', file=stderr, flush=True)

def check_unknown_conspirators(friends_file, plots_file):

    friends = set()
    with open(friends_file, 'r') as file:
        for line in file:
            parts = line.split(" is friends with ")
            if len(parts) == 2:
                friend_name = parts[0]
                friends.add(friend_name.strip())

    with open(plots_file, 'r') as file:
        for line in file:
            parts = line.split(" is plotting against ")
            if len(parts) == 2:
                conspirator_name = parts[0]
                if conspirator_name.strip() not in friends:
                    return 84

    return 0

def valid_arguments(argv: list) -> None:
    check_file(argv)
    if len(argv) == 5 and argv[1] == "--links":
        return True
    elif len(argv) == 5 and argv[1] == "--plots":
        if (check_unknown_conspirators(argv[2], argv[3]) == 84):
            print_error("Unknown conspirator")
            sys.exit(84)
        if argv[4].isdigit() == False:
            print_error("Invalid argument")
            sys.exit(84)
        return True
    elif len(argv) < 5:
        print_error("Not enough arguments")
        sys.exit(84)
    elif len(argv) > 5:
        print_error("Too many arguments")
        sys.exit(84)
    elif argv[1] != "--links" and argv[1] != "--plots":
        print_error("Invalid argument")
        sys.exit(84)
    return True


## ==============================================================
## =========================[READ]===============================
## ==============================================================

def read_file(filename: str) -> str:
    try:
        with open(filename, "r") as file:
            lines = file.readlines()
    except FileNotFoundError:
        print("Error: could not open file " + filename)
        sys.exit(84)

    names = set()
    for line in lines:
        names.update(name.strip() for name in line.split(" is friends with ")) and name.update(name.strip() for name in line.split(" is plotting against "))

    tab = '\n'.join(sorted(names))
    print (tab)
    return tab

def simple_read(filename: str) -> str:
    try:
        with open(filename, 'r') as file:
            return [line.strip() for line in file.readlines()]
    except FileNotFoundError:
        print("Error: could not open file " + filename)
        sys.exit(84)
    return lines

## ==============================================================
## =========================[GRAPH]==============================
## ==============================================================

def build_graph(filename):
    graph = {}
    with open(filename, 'r') as file:
        for line in file:
            parts = line.strip().split(' is friends with ')
            if len(parts) == 2:
                character1, character2 = parts
                if character1 not in graph:
                    graph[character1] = []
                if character2 not in graph:
                    graph[character2] = []
                graph[character1].append(character2)
                graph[character2].append(character1)
    return graph

def find_degree_of_separation(graph, character1, character2):
    link = set()
    queue = [(character1, 0)]

    while queue:
        current, degree = queue.pop(0)
        link.add(current)
        if current == character2:
            return degree
        for node in graph[current]:
            if node not in link:
                queue.append((node, degree + 1))
    return -1

## ==============================================================
## =========================[MATRIX]=============================
## ==============================================================

def relationship_matrix(graph, characters, max_degree):
    matrix = [['X' for _ in range(len(characters))] for _ in range(len(characters))]

    for i in range(len(characters)):
        for j in range(i, len(characters)):
            character1 = characters[i]
            character2 = characters[j]
            degree = find_degree_of_separation(graph, character1, character2)
            if degree > max_degree:
                matrix[i][j] = '0'
                matrix[j][i] = '0'
            else:
                matrix[i][j] = str(degree)
                matrix[j][i] = str(degree)

    return matrix

## ==================================================================
## =========================[CONSPIRACY]=============================
## ==================================================================


## ============================================================
## =========================[MAIN]=============================
## ============================================================

if __name__ == "__main__":
    argc = len(argv)
    if (argc == 1):
        print_error("Not enough arguments")
        sys.exit(84)
    if argc == 2 and argv[1] == "--help":
        display_help()
        sys.exit(0)
    if (argc == 2 and argv[1] != "--help"):
        print_error("Not enough arguments")
        sys.exit(84)
    if not valid_arguments(argv):
        sys.exit(84)
    filename = argv[2]
    character1 = argv[3]
    character2 = argv[4]
    if argv[1] == "--links":
        graph = build_graph(filename)
        degree = find_degree_of_separation(graph, character1, character2)
        if degree is not None:
            print(f"Degree of separation between {character1} and {character2}: {degree}")
        elif degree is None:
            print(f"Degree of separation between {character1} and {character2}: -1")
        elif degree == 0:
            print(f"Degree of separation between {character1} and {character2}: 0")
    else:
        file1 = argv[2]
        file2 = argv[3]
        max_degree = int(argv[4])
        print("Names:")
        lines = read_file(file1)
        print()
        print("Relationships:")
        graph = build_graph(file1)
        matrix = relationship_matrix(graph, lines.split('\n'), max_degree)
        for line in matrix:
            print(' '.join(line))
    sys.exit(0)