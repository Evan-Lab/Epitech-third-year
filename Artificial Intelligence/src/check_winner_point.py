#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## B-AIA-500-PAR-5-1-gomoku-evan.labourdette
## File description:
## check_winner_point
##

def check_point_4(board, friend: int, none: int, optimal_move) -> bool:
    # Horizontal
    for y in range(len(board)):
        for x in range(len(board[y]) - 4):
            winner, point = 0, 0
            for i in range(0, 5):
                if board[y][x + i] == friend:
                    winner+=1
                if point == 0 and board[y][x + i] == none:
                    point, y_coord, x_coord = point+1, y, x + i
            if winner == 5 or (winner == 4 and point == 1):
                return True, (y_coord, x_coord)

    # Vertical
    for y in range(len(board) - 4):
        for x in range(len(board[y])):
            winner, point = 0, 0
            for i in range(0, 5):
                if board[y + i][x] == friend:
                    winner+=1
                if point == 0 and board[y + i][x] == none:
                    point, y_coord, x_coord = point+1, y + i, x
            if winner == 5 or (winner == 4 and point == 1):
                return True, (y_coord, x_coord)

    # Haut gauche -> bas droite
    for y in range(len(board) - 4):
        for x in range(len(board[y]) - 4):
            winner, point = 0, 0
            for i in range(0, 5):
                if board[y + i][x + i] == friend:
                        winner+=1
                if point == 0 and board[y + i][x + i] == none:
                        point, y_coord, x_coord = point+1, y + i, x + i
            if winner == 5 or (winner == 4 and point == 1):
                return True, (y_coord, x_coord)

    # # Haut droite -> bas gauche
    for y in range(len(board) -4):
        for x in range(4, len(board[y])):
            winner, point = 0, 0
            for i in range(0, 5):
                if board[y + i][x - i] == friend:
                        winner+=1
                if point == 0 and board[y + i][x - i] == none:
                        point, y_coord, x_coord = point+1, y + i, x - i
            if winner == 5 or (winner == 4 and point == 1):
                return True, (y_coord, x_coord)

    return False, optimal_move


def check_point_3(board, friend: int, none: int, optimal_move) -> bool:
    # Horizontal
    for y in range(len(board)):
        for x in range(len(board[y]) - 4):
            winner, point = 0, 0
            for i in range(0, 5):
                if point == 1 and winner == 0 and board[y][x + i] == none:
                    break
                if point == 1 and winner != 0 and board[y][x + i] == none:
                    point = 2
                if board[y][x + i] == friend:
                    winner+=1
                if point == 0 and board[y][x + i] == none:
                    point, y_coord, x_coord = 1, y, x + i
            if winner == 3 and point == 2:
                return True, (y_coord, x_coord)

    # Vertical
    for y in range(len(board) - 4):
        for x in range(len(board[y])):
            winner, point = 0, 0
            for i in range(0, 5):
                if point == 1 and winner == 0 and board[y + i][x] == none:
                    break
                if point == 1 and winner != 0 and board[y + i][x] == none:
                    point = 2
                if board[y + i][x] == friend:
                    winner+=1
                if point == 0 and board[y + i][x] == none:
                    point, y_coord, x_coord = point+1, y + i, x
            if winner == 3 and point == 2:
                return True, (y_coord, x_coord)

    # Haut gauche -> bas droite
    for y in range(len(board) - 4):
        for x in range(len(board[y]) - 4):
            winner, point = 0, 0
            for i in range(0, 5):
                if point == 1 and winner == 0 and board[y + i][x + i] == none:
                    break
                if point == 1 and winner != 0 and board[y + i][x + i] == none:
                    point = 2
                if board[y + i][x + i] == friend:
                        winner+=1
                if point == 0 and board[y + i][x + i] == none:
                        point, y_coord, x_coord = point+1, y + i, x + i
            if winner == 3 and point == 2:
                return True, (y_coord, x_coord)

    # # Haut droite -> bas gauche
    for y in range(len(board) -4):
        for x in range(4, len(board[y])):
            winner, point = 0, 0
            for i in range(0, 5):
                if point == 1 and winner == 0 and board[y + i][x - i] == none:
                    break
                if point == 1 and winner != 0 and board[y + i][x - i] == none:
                    point = 2
                if board[y + i][x - i] == friend:
                        winner+=1
                if point == 0 and board[y + i][x - i] == none:
                        point, y_coord, x_coord = point+1, y + i, x - i
            if winner == 3 and point == 2:
                return True, (y_coord, x_coord)

    return False, optimal_move



def check_pre_4(board, friend: int, none: int, optimal_move) -> bool:
    
                                # 0 X 0 X X 0
                                # 0 X X 0 X 0
    # Horizontal
    for y in range(len(board)):
        for x in range(len(board[y]) - 5):
            if board[y][x] == none and board[y][x+1] == friend and board[y][x+2] == none and board[y][x+3] == friend and board[y][x+4] == friend and board[y][x+5] == none:
                return True, (y, x+2)
            if board[y][x] == none and board[y][x+1] == friend and board[y][x+2] == friend and board[y][x+3] == none and board[y][x+4] == friend and board[y][x+5] == none:
                return True, (y, x+3)

    # Vertical
    for y in range(len(board) - 5):
        for x in range(len(board[y])):
            if board[y][x] == none and board[y+1][x] == friend and board[y+2][x] == none and board[y+3][x] == friend and board[y+4][x] == friend and board[y+5][x] == none:
                return True, (y+2, x)
            if board[y][x] == none and board[y+1][x] == friend and board[y+2][x] == friend and board[y+3][x] == none and board[y+4][x] == friend and board[y+5][x] == none:
                return True, (y+3, x)

    # Haut gauche -> bas droite
    for y in range(len(board) - 5):
        for x in range(len(board[y]) - 5):
            if board[y][x] == none and board[y+1][x+1] == friend and board[y+2][x+2] == none and board[y+3][x+3] == friend and board[y+4][x+4] == friend and board[y+5][x+5] == none:
                return True, (y+2, x+2)
            if board[y+3][x+3] == none and board[y+1][x+1] == friend and board[y+2][x+2] == friend and board[y+3][x+3] == none and board[y+4][x+4] == friend and board[y+5][x+5] == none:
                return True, (y+3, x+3)

    # # Haut droite -> bas gauche
    for y in range(len(board) -5):
        for x in range(5, len(board[y])):
            if board[y][x] == none and board[y+1][x-1] == friend and board[y+2][x-2] == none and board[y+3][x-3] == friend and board[y+4][x-4] == friend and board[y+5][x-5] == none:
                return True, (y+2, x-2)
            if board[y][x] == none and board[y+1][x-1] == friend and board[y+2][x-2] == friend and board[y+3][x-3] == none and board[y+4][x-4] == friend and board[y+5][x-5] == none:
                return True, (y+3, x-3)

    return False, optimal_move

def check_winner_point(board, friend: int, ennemy: int, none: int, optimal_move) -> bool:
    status, optimal_move = check_point_4(board, friend, none, optimal_move)
    if status:
        return True, optimal_move
    status, optimal_move = check_point_4(board, ennemy, none, optimal_move)
    if status:
        return True, optimal_move


    status, optimal_move = check_pre_4(board, friend, none, optimal_move)
    if status:
        return True, optimal_move
    status, optimal_move = check_pre_4(board, ennemy, none, optimal_move)
    if status:
        return True, optimal_move


    status, optimal_move = check_point_3(board, friend, none, optimal_move)
    if status:
        return True, optimal_move
    status, optimal_move = check_point_3(board, ennemy, none, optimal_move)
    if status:
        return True, optimal_move
    return False, optimal_move
