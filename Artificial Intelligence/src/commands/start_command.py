#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## Gomoku
## File description:
## start command
##

def start_command(start, action) -> None:

    try:
        size = int(action[1])
        if size <= 0:
            print("ERROR message - unsupported size or other error", flush=True)
            return
        start.size_row = size
        start.size_col = size
        start.gomoku_board = [[start.none for _ in range(size)] for _ in range(size)]
        print("OK", flush=True)
        return
    except Exception as e:
        print("ERROR message - unsupported size or other error", flush=True)
        return
