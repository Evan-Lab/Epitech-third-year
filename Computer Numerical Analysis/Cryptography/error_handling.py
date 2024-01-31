#!/usr/bin/env python3
import sys
from sys import argv
from sys import stderr
from math import *

def print_error(msg: str) -> None:
    return print(f'Error: {msg}', file=stderr, flush=True)

def valid_arguments(argv):
    if argv[1] not in ["-xor", "-aes", "-rsa", "-pgp"]:
       print_error("Invalid first argument, please use -xor, -aes, -rsa", "pgp")
       sys.exit(84)

    if (argv[1] == "-xor" or argv[1] == "-aes"):
        if (argv[2] == "-c" or argv[2] == "-d"):
            if (len(argv) != 4 and len(argv) != 5):
                print_error("Invalid number of argument, please use 4 or 5")
                sys.exit(84)
            if (argv[3] == "-b" and len(argv) != 5):
                print_error("Invalid block mode when argv[3] == -b")
                sys.exit(84)
            elif (argv[3] != "-b" and len(argv) != 4):
                print_error("Invalid block mode when argv[3] != -b")
                sys.exit(84)

    if (argv[1] == "-rsa"):
        if (argv[2] != "-c" and argv[2] != "-d" and (argv[2] != "-g")):
            print_error("Invalid operator, please use -g, -c or -d")
            sys.exit(84)

    if (argv[1] == "-rsa"):
       if (argv[2] == "-g" and len(argv) != 5):
            print_error("wrong number of argument for -g")
            sys.exit(84)
       if (argv[2] == "-c" or argv[2] == "-d") and len(argv) != 4:
            print(len(argv))
            print_error("wrong number of argument for -c or -d")
            sys.exit(84)

    if (argv[1] == '-pgp'):
        if (argv[2] != "-c" and argv[2] != "d" or len(argv) != 5):
            print_error("Invalid operation or wrong number of argument, please use -c or -d")
            sys.exit(84)
    return True
