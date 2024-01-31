##
## EPITECH PROJECT, 2023
## Pywork-Private
## File description:
## Makefile
##

NAME	=	pbrain-gomoku-ai

rm		=	rm -f

SRC		=	src/main.py	\
			src/gomoku_ai.py \
	  		src/gomoku_board.py \
	  		src/minmax_algo.py \
			src/check_winner_point.py\
	  		src/commands

$(NAME):
	cp -r $(SRC) .
	mv main.py $(NAME)
	chmod 755 pbrain-gomoku-ai

all: $(NAME)

clean:
	$(RM) -r __pycache__
	$(RM) -r ./pbrain-gomoku-ai.spec

fclean: clean
	$(RM) $(NAME)
	rm -rf gomoku_ai.py gomoku_board.py commands minmax_algo.py main.py check_winner_point.py

re: fclean all

.PHONY: all clean fclean re