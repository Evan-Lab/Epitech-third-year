#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## Gomoku
## File description:
## turn command
##

def turn_command(turn, action) -> bool:

    try:
        x, y = map(int, action[1].split(','))
        if not is_valid_position(x, y, turn.size_row, turn.size_col):
            print("ERROR - out of range", flush=True)
            return False
        if turn.gomoku_board[x][y] != turn.none:
            print(f"ERROR - space already used", flush=True)
            return False
        turn.actual_turn += 1
        turn.gomoku_board[x][y] = turn.ennemy
        turn.actual_turn += 1
        return turn.calcul_minmax()
    except Exception as e:
        print("ERROR - Not a positive integer", flush=True)
        return False

def is_valid_position(x, y, size_row, size_col):
    return 0 <= x < size_row and 0 <= y < size_col