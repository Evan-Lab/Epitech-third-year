#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## Gomoku
## File description:
## Gomoku AI
##

from math import inf
import time
from minmax_algo import minmax_calculate
from commands.start_command import start_command
from commands.turn_command import turn_command
from commands.begin_command import begin_command
from commands.board_command import board_command
from commands.info_command import info_command
from commands.about_command import about_command


from check_winner_point import check_winner_point

def get_gomoku_board(gomoku_board, row, col) -> list:

    gomoku_len_row, gomoku_len_col = len(gomoku_board), len(gomoku_board[0])
    new_gomoku_board = []
    for i in range(-2, 3):
        new_row = []
        for j in range(-2, 3):
            if 0 <= row + i < gomoku_len_row and 0 <= col + j < gomoku_len_col:
                new_row.append(gomoku_board[row + i][col + j])
            else:
                new_row.append(0)
        new_gomoku_board.append(new_row)
    return new_gomoku_board

def middle_check(row, col, gomoku_board) -> int:

    middle_of_row = len(gomoku_board[0]) // 2
    middle_of_col = len(gomoku_board) // 2
    return abs(row - middle_of_row) + abs(col - middle_of_col)

class Gomoku_class:

    def __init__(gomoku):
        gomoku.gomoku_board = []
        gomoku.actual_turn = 0
        gomoku.size_row = 0
        gomoku.size_col = 0
        gomoku.none = 0
        gomoku.ennemy = 1
        gomoku.friend = 2

    def command_parser(gomoku, command):
        if command[0] == "START":
            start_command(gomoku, command)
        elif command[0] == "TURN":
            turn_command(gomoku, command)
        elif command[0] == "BEGIN":
            begin_command(gomoku)
        elif command[0] == "BOARD":
            board_command(gomoku)
        elif command[0] == "INFO":
            info_command()
        elif command[0] == "ABOUT":
            about_command()
        else:
            print("ERROR Unknown command", flush=True)

    def init_c(gomoku, row: int, col: int) -> int:

        return_value: int = 0

        for nb in get_gomoku_board(gomoku.gomoku_board, row, col):
            if (any([gomoku.friend, gomoku.ennemy]) in nb):
                return_value += 1
        return return_value

    def check_if_set_value(gomoku, value, optimal_value, assign_move: bool, optimal_move: tuple[int, int], row: int, col: int) -> tuple[int, tuple[int, int], bool]:
        if (value > optimal_value or (value == optimal_value and middle_check(row, col, gomoku.gomoku_board) < middle_check(optimal_move[0], optimal_move[1], gomoku.gomoku_board)) or assign_move == False):
            optimal_value = value
            optimal_move = (row, col)
            assign_move = True
        return optimal_value, optimal_move, assign_move

    def calcul_minmax_set_value(gomoku, time_now: int, row: int, col: int) -> int:
        gomoku.gomoku_board[row][col] = gomoku.friend
        value = minmax_calculate(gomoku.gomoku_board, 1, False, gomoku.friend, gomoku.ennemy, -inf, inf, 5000, time_now)
        gomoku.gomoku_board[row][col] = gomoku.none
        return value

    def calcul_minmax_algo(gomoku, time_now: int, optimal_value, assign_move: bool, optimal_move: tuple[int, int], row: int, col: int) -> tuple[int, tuple[int, int], bool]:
        c: int = gomoku.init_c(row, col)

        if (c != 0):
            value = gomoku.calcul_minmax_set_value(time_now, row, col)
            optimal_value, optimal_move, assign_move = gomoku.check_if_set_value(value, optimal_value, assign_move, optimal_move, row, col)
        return optimal_value, optimal_move, assign_move


    def check_assign_move(gomoku, assign_move: bool) -> None:
        if (assign_move == False):
            print("MESSAGE No move found", flush=True)


    def set_optimal_move(gomoku, optimal_move: tuple[int, int]) -> None:
        print(f"{optimal_move[0]},{optimal_move[1]}", flush=True)
        gomoku.gomoku_board[optimal_move[0]][optimal_move[1]] = gomoku.friend


    def init_value(gomoku) -> tuple[int, int, bool, tuple[int, int]]:
        time_now = int(time.time() * 1000)
        optimal_value = -inf
        assign_move = False
        optimal_move = (len(gomoku.gomoku_board[0]) // 2, len(gomoku.gomoku_board) // 2)
        return time_now, optimal_value, assign_move, optimal_move


    def calcul_minmax(gomoku) -> bool:
        time_now, optimal_value, assign_move, optimal_move = gomoku.init_value()



        value, optimal_move = check_winner_point(gomoku.gomoku_board, gomoku.friend, gomoku.ennemy, gomoku.none, optimal_move)



        if not value:
            for row in range(0, gomoku.size_row, 1):
                for col in range(0, gomoku.size_col, 1):
                    if (gomoku.gomoku_board[row][col] == gomoku.none):
                        optimal_value, optimal_move, assign_move = gomoku.calcul_minmax_algo(time_now, optimal_value, assign_move, optimal_move, row, col)
            gomoku.check_assign_move(assign_move)
        gomoku.set_optimal_move(optimal_move)

