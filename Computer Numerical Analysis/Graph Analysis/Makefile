##
## EPITECH PROJECT, 2023
## game_of_graph
## File description:
## Makefile
##

SRC = main.py
MAIN = main.py
NAME = game_of_graphs

all: $(NAME)

$(NAME): $(SRC)
	cp $(MAIN) $(NAME)
	chmod +x $(NAME)

clean:
	rm -f $(NAME)

fclean: clean
	rm -f $(NAME)

re: fclean all

.PHONY: all clean fclean re