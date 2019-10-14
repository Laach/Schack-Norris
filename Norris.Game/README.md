# bool IsValidMove(BoardMoveModel)
## Description
Checks if a given move is valid for a player on a given board.

## DTO information
[BoardMoveModel](/Norris.Game/Models/README.md)

# BoardModel DoMove(BoardMoveModel)
## Description
Moves whatever is on a given position to a new position. 
**Disclaimer!** It does not check if it is a valid move. `IsValidMove(...)` is 
expected to be run beforehand.


## DTO information
[BoardMoveModel](/Norris.Game/Models/README.md)

# BoardModel FillPossibleMoves(BoardMoveModel)
## Description
Sets the bools in the Tiles to true if they are able to do the respective actions.
Returns the new BoardModel with the Tiles modified.

## DTO information
[BoardMoveModel](/Norris.Game/Models/README.md)