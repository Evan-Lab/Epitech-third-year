#!/usr/bin/env python3
import sys
from sys import argv
from sys import stderr
from math import *

def display_help() -> None:
    print("USAGE")
    print("\t./mypgp [-xor | -aes | -rsa | -pgp] [-c | -d] [-b] KEY")
    print("\tthe MESSAGE is read from standard input\n")
    print("DESCRIPTION\n")
    print("\t-xor\tcomputation using XOR algorithm")
    print("\t-aes\tcomputation using AES algorithm")
    print("\t-rsa\tcomputation using RSA algorithm")
    print("\t-pgp\tcomputation using both RSA and AES algorithm")
    print("\t-c\tMESSAGE is clear and we want to cipher it")
    print("\t-d\tMESSAGE is ciphered and we want to decipher it")
    print("\t-b\tblock mode: for xor and aes, only works on one block")
    print("\t\tMESSAGE and KEY must be of the same size")
    print("\t-g P Q\tfor RSA only: generate a public and private key pair from the prime number P and Q")
