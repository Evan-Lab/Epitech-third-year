#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## my_PGP
## File description:
## Converter
##

def hex_little_endian_to_hex_big_endian(hex_str) -> str:

    hex_str = hex_str.zfill((len(hex_str) + 1) // 2 * 2)
    big_endian = ''.join([hex_str[i:i+2] for i in range(0, len(hex_str), 2)][::-1])
    return  big_endian

def hex_big_endian_to_int(hex_str) -> int:

    hex_str = hex_str.lstrip("0x")
    int_value = int(hex_str, 16)
    return int_value

def int_to_big_endian_hex(int_value) -> str:

    hex_big_endian = hex(int_value)
    return hex_big_endian

def big_to_little_endian(hex_str) -> str:

    hex_str = hex_str.zfill((len(hex_str) + 1) // 2 * 2)
    little_endian = ''.join([hex_str[i:i+2] for i in range(0, len(hex_str), 2)][::-1])
    little_endian = little_endian[:-2]
    return little_endian

def int_to_little_endian_hex(num) -> str:

    little_endian_hex = num.to_bytes((num.bit_length() + 7) // 8, 'little').hex()
    return little_endian_hex

def int_to_little_endian_hex(num) -> str:

    little_endian_hex = num.to_bytes((num.bit_length() + 7) // 8, 'little').hex()
    return little_endian_hex
