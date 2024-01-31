#!/usr/bin/env python3
##
## EPITECH PROJECT, 2023
## my_PGP
## File description:
## RSA algorithm
##

from converter import int_to_little_endian_hex
from converter import int_to_little_endian_hex

def extended_gcd(a, b) -> int:

    if b == 0:
        return a, 1, 0
    else:
        d, x, y = extended_gcd(b, a % b)
        return d, y, x - (a // b) * y

def modular_inverse(e, lambda_n) -> int:

    _, d, _ = extended_gcd(e, lambda_n)
    return d % lambda_n

def choose_public_exponent(lambda_n) -> int:

    e = 65537

    while gcd(e, lambda_n) != 1:
        e += 2
    return e

def gcd(a, b) -> int:

    while b:
        a, b = b, a % b
    return a

def lcm(a, b) -> int:

    return abs(a * b) // gcd(a, b)

def compute_carmichael_totient(p, q) -> int:

    return lcm(p - 1, q - 1)

def first_calcul(p, q) -> int:

    n = p * q
    return n

def rsa_generate_key_g(argv) -> None:

    p = int(argv[3], 16)
    q = int(argv[4], 16)

    n = first_calcul(p, q)
    lambda_n = compute_carmichael_totient(p, q)
    e = choose_public_exponent(lambda_n)
    d = modular_inverse(e, lambda_n)

    n_hex = int_to_little_endian_hex(n)
    e_hex = int_to_little_endian_hex(e)
    d_hex = int_to_little_endian_hex(d)

    public_key = e_hex + '-' + n_hex
    private_key = d_hex + '-' + n_hex

    print("Public key:", public_key)
    print("Private key:", private_key)
