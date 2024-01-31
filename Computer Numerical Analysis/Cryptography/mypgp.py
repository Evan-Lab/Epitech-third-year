#!/usr/bin/env python3
import sys
from sys import argv
from sys import stderr
from math import *
from help import display_help
from error_handling import print_error, valid_arguments
from xor import xor_cipher
from selector import selector

if __name__ == "__main__":
    argc = len(argv)
    if (argc == 1):
        print_error("Not enough arguments")
        sys.exit(84)
    if argc == 2 and argv[1] == "-h":
        display_help()
        sys.exit(0)
    if not valid_arguments(argv):
        sys.exit(84)
    selector(argv)
    sys.exit(0)