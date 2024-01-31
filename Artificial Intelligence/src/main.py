#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## Gomoku
## File description:
## Main function
##

from gomoku_ai import Gomoku_class

def main() -> int:

    try:
        Gomoku_ai = Gomoku_class()

        while True:
            Ai_command = input().split()
            if len(Ai_command) == 1 and Ai_command[0] == "END":
                return 0
            Gomoku_ai.command_parser(Ai_command)
    except Exception as error:
        print(f"ERROR error = {error}", flush=True)

if __name__ == '__main__':
    main()
else :
    print("ERROR: main.py is not a module", flush=True)
    exit(84)