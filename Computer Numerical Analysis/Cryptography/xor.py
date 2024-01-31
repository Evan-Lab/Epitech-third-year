#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## my_PGP
## File description:
## XOR algorithm
##

import sys
from sys import argv
from sys import stderr
from math import *

def xor_cipher(message: str, key: str) -> str:

    message_bytes = bytes.fromhex(message)
    key_bytes = bytes.fromhex(key)

    encrypted_bytes = bytes([m ^ k for m, k in zip(message_bytes, key_bytes)])
    encrypted_message = encrypted_bytes.hex()
    return encrypted_message