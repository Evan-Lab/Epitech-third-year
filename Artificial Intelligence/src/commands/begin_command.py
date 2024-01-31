#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## Gomoku
## File description:
## Begin command
##

def begin_command(begin) -> bool:

    begin.actual_turn += 1
    temp = begin.friend
    begin.friend = begin.ennemy
    begin.ennemy = temp
    return begin.calcul_minmax()