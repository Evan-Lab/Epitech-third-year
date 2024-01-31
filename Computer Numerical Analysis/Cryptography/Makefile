##
## EPITECH PROJECT, 2023
## my_PGP
## File description:
## Makefile
##

SRC = mypgp.py
MAIN = mypgp.py
NAME = mypgp

all: $(NAME)

$(NAME): $(SRC)
	cp $(MAIN) $(NAME)
	chmod +x $(NAME)

clean:
	rm -f $(NAME)

fclean: clean
	rm -f $(NAME)
	rm -rf __pycache__

re: fclean all

.PHONY: all clean fclean re