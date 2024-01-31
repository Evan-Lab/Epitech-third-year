#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## my_PGP
## File description:
## RSA encryption
##

from converter import hex_little_endian_to_hex_big_endian
from converter import hex_big_endian_to_int
from converter import int_to_big_endian_hex
from converter import big_to_little_endian

def rsa_encryption(message, public_key) -> int:

    e, n = public_key.split('-')

    message_big_endian = hex_little_endian_to_hex_big_endian(message)
    message_int = hex_big_endian_to_int(message_big_endian)

    e = hex_little_endian_to_hex_big_endian(e)
    e = hex_big_endian_to_int(e)
    n = hex_little_endian_to_hex_big_endian(n)
    n = hex_big_endian_to_int(n)

    ciphertext = pow(message_int, e, n)
    ciphertext = int_to_big_endian_hex(ciphertext)
    ciphertext = big_to_little_endian(ciphertext)
    return ciphertext
