#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## my_PGP
## File description:
## RSA encryption
##

from converter import hex_little_endian_to_hex_big_endian
from converter import hex_big_endian_to_int
from converter import big_to_little_endian
from converter import int_to_big_endian_hex

def rsa_decryption(ciphertext, private_key):

    d, n = private_key.split('-')
    ciphertext = ciphertext.strip()

    ciphertext = hex_little_endian_to_hex_big_endian(ciphertext)
    ciphertext = hex_big_endian_to_int(ciphertext)
    d = hex_little_endian_to_hex_big_endian(d)
    d = hex_big_endian_to_int(d)
    n = hex_little_endian_to_hex_big_endian(n)
    n = hex_big_endian_to_int(n)

    decrypted_message = pow(ciphertext, d, n)
    decrypted_message = int_to_big_endian_hex(decrypted_message)
    decrypted_message = big_to_little_endian(decrypted_message)
    return decrypted_message

