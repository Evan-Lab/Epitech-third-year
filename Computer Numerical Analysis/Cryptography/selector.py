#!/usr/bin/env python3
import sys
from sys import argv
from sys import stderr
from math import *
from xor import xor_cipher
from rsa import rsa_generate_key_g
from rsa_encryption import rsa_encryption
from rsa_decryption import rsa_decryption
from aes import encrypt

def selector(argv):
    xor_selector(argv)
    aes_selector(argv)
    rsa_selector(argv)
    pgp_selector(argv)

def hex_to_key(hex_key):
    key_bytes = bytes.fromhex(hex_key)
    key_matrix = [list(key_bytes[i:i+4]) for i in range(0, len(key_bytes), 4)]
    return key_matrix

def xor_selector(argv):
    if argv[1] == "-xor":
      if (argv[3] == "-b"):
        if argv[2] == "-c":
          key = argv[4]
          message = sys.stdin.read()
          print(xor_cipher(message, key))
        if argv[2] == "-d":
          key = argv[4]
          message = sys.stdin.read()
          print(xor_cipher(message, key))
      elif (argv[3] != "-b"):
        if argv[2] == "-c":
          key = argv[3]
          message = sys.stdin.read()
          print(xor_cipher(message, key))
        if argv[2] == "-d":
          key = argv[3]
          message = sys.stdin.read()
          print(xor_cipher(message, key))

def aes_selector(argv):
    if argv[1] == "-aes":
      if argv[2] == "-c":
          key = hex_to_key(argv[4])
          message = sys.stdin.read()
          encrypted_message = encrypt(message, key).hex()
          print(encrypted_message)
      if argv[2] == "-d":
          print("AES -d")
          key = hex_to_key(argv[4])
          message = sys.stdin.read()

def rsa_selector(argv):
    if argv[1] == "-rsa":
      if argv[2] == "-g":
        rsa_generate_key_g(argv)
      elif argv[2] == "-c":
          key = argv[3]
          message = sys.stdin.read()
          print(rsa_encryption(message, key))
      elif argv[2] == "-d":
          key = argv[3]
          message = sys.stdin.read()
          print(rsa_decryption(message, key))

def pgp_selector(argv):
    if argv[1] == "-pgp":
      if argv[2] == "-c":
          print("PGP -c")
      if argv[2] == "-d":
          print("PGP -d")
