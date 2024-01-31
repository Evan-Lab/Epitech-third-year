#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## Gomoku
## File description:
## Gomoku board
##

from gomoku_board import check_board
import time

def check_neighbour(board: list, x_coord: int, y_coord: int) -> bool:

    len_x, len_y = len(board), len(board[y_coord])

    return (
        (x_coord > 0 and board[y_coord][x_coord - 1]) or
        (y_coord > 0 and board[y_coord - 1][x_coord]) or
        (len_x - 1 != x_coord and board[y_coord][x_coord + 1]) or
        (len_y - 1 != y_coord and board[y_coord + 1][x_coord]) or
        (x_coord > 0 and y_coord > 0 and board[y_coord - 1][x_coord - 1]) or
        (x_coord > 0 and len_x - 1 != y_coord and board[y_coord + 1][x_coord - 1]) or
        (len_x - 1 != x_coord and len_y - 1 != y_coord and board[y_coord + 1][x_coord + 1]) or
        (len_x - 1 != x_coord and y_coord > 0 and board[y_coord - 1][x_coord + 1])
    )

def minmax_return_choice(is_maximizing: bool, alpha, beta) -> int:
    if is_maximizing:
        return alpha
    else:
        return beta

def minmax_choice(is_maximizing: bool, alpha, beta, value) -> tuple:
    if is_maximizing:
        alpha = max(alpha, value)
    else:
        beta = min(beta, value)
    return alpha, beta

def minmax_beta_calcul(board: list, is_maximizing: bool, row: int, col: int, player, opponent) -> list:
    if is_maximizing:
        board[row][col] = player
    else:
        board[row][col] = opponent
    return board

def calculate_board_value(board, player, opponent) -> int:
    player_score = check_board(board, player, opponent)
    opponent_score = check_board(board, opponent, player)
    return player_score - opponent_score

def minmax_calculate(board: list, depth: int, is_maximizing: bool, player, opponent, alpha, beta, timelimit, start_stamp) -> int:

    if depth == 0:
        return calculate_board_value(board, player, opponent)
    for row in range(0, len(board), 1):
        for col in range(0, len(board[row]), 1):
            if board[row][col] == 0 and check_neighbour(board, col, row):
                board = minmax_beta_calcul(board, is_maximizing, row, col, player, opponent)
                value = minmax_calculate(board, depth - 1, not is_maximizing, player, opponent, alpha, beta, timelimit, start_stamp)
                alpha, beta = minmax_choice(is_maximizing, alpha, beta, value)
                board[row][col] = 0
                if alpha >= beta or int(time.time()) * 1000 - start_stamp > timelimit * 0.9:
                    return minmax_return_choice(is_maximizing, alpha, beta)
    return minmax_return_choice(is_maximizing, alpha, beta)
