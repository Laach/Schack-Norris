# [IChessLogic](/Norris.Game/IChessLogic.cs)
## Description
An interface to represent the ruleset of a chess game.

# [ChessLogic](/Norris.Game/ChessLogic.cs)
## Description
A class that implements IChessLogic



# [bool IsValidMove(MovePlanDTO)](/Norris.Game/ChessLogic.cs)
## Description
Checks if a given move is valid for a player on a given board.

## DTO information
[MovePlanDTO](/Norris.Game/Models/DTO/README.md)


# [string[,] DoMove(MovePlanDTO)](/Norris.Game/ChessLogic.cs)
## Description
Moves whatever is on a given position to a new position. 
**Disclaimer!** It does not check if it is a valid move. `IsValidMove(...)` is 
expected to be run beforehand.

## DTO information
[MovePlanDTO](/Norris.Game/Models/DTO/README.md)


# [PossibleMovesDTO GetPossibleMoves(SelectedPieceDTO)](/Norris.Game/ChessLogic.cs)
## Description
Returns one list containing all empty slots the selected piece can move to, and 
another list containing all non-empty slots the selected piece can move to.

## DTO information
[SelectedPieceDTO](/Norris.Game/Models/DTO/README.md)


# [bool IsWhiteChecked(string[,])](/Norris.Game/ChessLogic.cs)
## Description
Takes in a board and returns if white king is checked.


# [bool IsBlackChecked(string[,])](/Norris.Game/ChessLogic.cs)
## Description
Takes in a board and returns if black king is checked.