EPITECH Neural Network

-----------SUBJECT ------------
You need to create neural network that can take a chess board as input, and outputs the status of the game:
either a player wins (checkmate), a player has the other player’s king checked, there is a draw (stalemate) or
the game is still on.

------ USAGE --------

USAGE
	./my_torch [--new IN_LAYER [HIDDEN_LAYERS...] OUT_LAYER | --load LOADFILE]
	[--train | --predict] [--save SAVEFILE] FILE

DESCRIPTION
	--new   Creates a new neural network with random weights.
	        Each subsequent number represents the number of neurons on each layer, from left
	        to right. For example, ./my_torch --new 3 4 5 will create a neural network with
	        an input layer of 3 neurons, a hidden layer of 4 neurons, and an output layer of 5
	        neurons.
	--load  Loads an existing neural network from LOADFILE.
	--train Launches the neural network in training mode. Each board in FILE
	        must contain inputs to send to the neural network, as well as the expected output.
	--predict   Launches the neural network in prediction mode. Each board in FILE
	        must contain inputs to send to the neural network, and optionally an expected
	        output.
	--save  Save neural network internal state into SAVEFILE.
	FILE    FILE containing chessboards


------ NEW  and SAVE --------

• To launch it tape ./my_torch --new [input_size] [hidden_size] [output_size] --save [file]

Neural network created and saved on the file x

------ PREDICT --------

• To launch it tape ./my_torch --predict [your training data]

• You will have these output

Epoch 780/1000, Loss: 0.000987433910180244
Epoch 800/1000, Loss: 0.0009648767806181757
Epoch 820/1000, Loss: 0.00094337749684493
Epoch 840/1000, Loss: 0.0009228621259641967
Epoch 860/1000, Loss: 0.0009032635305177043
Epoch 880/1000, Loss: 0.0008845206005865598
Epoch 900/1000, Loss: 0.0008665775881694425
Epoch 920/1000, Loss: 0.0008493835282384741
Epoch 940/1000, Loss: 0.0008328917335416006
Epoch 960/1000, Loss: 0.0008170593523846293
Epoch 980/1000, Loss: 0.0008018469803901501
Epoch 1000/1000, Loss: 0.0007872183186758123

 +------------------------------------------------------+
 |                       Prédiction                     |
 +------------------------------------------------------+

  FEN testé: 8/8/8/1kP5/N6q/1K6/8/8 w - - 9 64
  Taux de confiance de la victoire des noires de 3.866830805348732%
  Taux de confiance de la victoire des blancs de 90.0580293379171%
  Avantage de environ 86.19119853256836% des Blancs
  Vainqueur prédit : Blancs
  Résultat prédit : Échec et mat
  Échec et mat prédit ? : Oui
  Confiance de l'échec et mat : 90.0580293379171%

 +------------------------------------------------------+

• Here you will have the training progress of the neural network over several epochs (simulation if you prefer) and the loss which is a measure of how well the neural network is performing on the training data. A lower loss indicates better performance. Our neural network is set to train for a period of 1000 and each epoch give us the loss of each epoch

• We have after the FEN test ( A FEN string represents the state of the chessboard, including the positions of pieces, castling availability, en passant target square, halfmove clock, and fullmove number) that the neural network is training with. For our neural network a random fan is choose between all the data and after this he effectue his own calculation.

• We have the confidence of victory of each faction and the advantage for the one with the highest probability of win

• Our neural network will after predict our winner based on the data and said if it was a check mate or not.

------- DESIGN CHOICES -----

Why these specifics [input_size] [hidden_size] [output_size] you will tell me ?

We are doing a chess neuralnetwork so we have to choose the value for it.

• So since a standard chessboard is 8x8, with 64 squares, having an input_size of 64 suggests that each node in the input layer represents a feature related to a specific square on the chessboard.

• The choice of 32 nodes in the hidden layer is purely based by the input_size, we should take any number but based of the input layer we decide to split in two the number of layer for the hidden one.

Because with a higher value of hidden layer ( > 45) the loss are too big and the advantage tend to exponential and it will always result on a check mate so for training and result purpose it is not revelant.

We have the same problem with a lower value of hidden layer, the loss are too small so the result is not revelant, we will always have a check mate and one party that will have all the advantage calculation.

So the perfect half is just good enough, we have balanced advantage and confidence and sometime some data result with a non check mate.

• Having an output_size of 2 suggests that the neural network is designed for a binary classification task so it will predict one of two outcomes. SO it is perfect for chess, so this could be predict whether Black or White has an advantage, whether a move leads to checkmate or not, or similar binary outcomes. We do not need more or less output_size because it will always be a choice between two outcomes.

• 1000 epoch was choose for our training because it is not overfitting and convergence that have minimal impact.If it is too low the neural network might underfit the data and if it is too much the result will have minimal impact and may take too much time to process