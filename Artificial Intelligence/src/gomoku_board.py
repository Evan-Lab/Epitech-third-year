#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## Gomoku
## File description:
## Gomoku board
##

def board_check(x, y, len_x, len_y) -> bool:
    return 0 <= x < len_x and 0 <= y < len_y

def check_line(board, x, y, dx, dy, check, board_value, new_value) -> int:

    size, multiplier = 1, 0
    if board_check(x - dx, y - dy, len(board), len(board[0])) and board[x - dx][y - dy] not in [board_value, check]:
        multiplier += 1
    for i in range(1, 6):
        size = i
        if not board_check(x + i * dx, y + i * dy, len(board), len(board[0])) or board[x + i * dx][y + i * dy] != check:
            break
    if board_check(x + size * dx, y + size * dy, len(board), len(board[0])) and board[x + size * dx][y + size * dy] != board_value:
        multiplier += 1
    if board_check(x - dx, y - dy, len(board), len(board[0])) and board[x - dx][y - dy] == check:
        multiplier = 0
    return float('inf') if new_value[multiplier][size] == float('inf') else new_value[multiplier][size]

def check_board(board, check, board_value) -> int:

    score, new_value = 0, [[0, 0, 0, 0, 0, float('inf')], [0, 1, 4, 16, 64, float('inf')], [0, 2, 8, 32, 1000, float('inf')]]
    try:
        for y in range(len(board[0])):
            for x in range(len(board)):
                if board[x][y] == check:
                    for dx, dy in [(0, 1), (1, 0), (-1, 1), (1, 1)]:
                        score += check_line(board, x, y, dx, dy, check, board_value, new_value)
    except Exception as e:
        print(f"ERROR error = {e}", flush=True)
    return score
