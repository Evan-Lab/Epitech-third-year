#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## Gomoku
## File description:
## Board command
##

def board_command_error_value(x: int, y: int, field: int, max_x: int, max_y: int, none: int, friend: int, ennemy: int) -> bool:
    if (x > max_x or x < 0) or (y > max_y or y < 0):
        print("ERROR invalid size")
        return False
    if field != none and field != friend and field != ennemy:
        print("ERROR invalid value")
        return False
    return True

def board_command(board) -> None:
    max_x = board.size_row
    max_y = board.size_col

    try:
        while True:
            current_line = input()
            if current_line == "DONE":
                board.actual_turn += 1
                return board.calcul_minmax()
            value_line = current_line.split(",")
            if len(value_line) != 3:
                return
            x = int(value_line[0])
            y = int(value_line[1])
            field = int(value_line[2])
            if board_command_error_value(x, y, field, max_x, max_y, board.none, board.friend, board.ennemy):
                board.actual_turn += 1
                board.gomoku_board[x][y] = field
            else:
                return
    except ValueError:
        print("ERROR invalid integer in BOARD command")
        return