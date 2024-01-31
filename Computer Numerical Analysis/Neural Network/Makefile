##
## EPITECH PROJECT, 2023
## neuralnetwork
## File description:
## Makefile
##

SRC = my_torch.py
MAIN = my_torch.py
NAME = my_torch

all: $(NAME)

$(NAME): $(SRC)
	cp $(MAIN) $(NAME)
	chmod +x $(NAME)

clean:
	rm -f $(NAME)

fclean: clean
	rm -f $(NAME)
	rm -rf __pycache__
	rm -rf training_progress.json

re: fclean all

.PHONY: all clean fclean re