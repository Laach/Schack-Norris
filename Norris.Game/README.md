# bool IsValidMove(BoardMoveModel)
### Description
Checks if a given move is valid for a player on a given board.

## DTO information
- BoardModel: The game board. Found in `Norris.Data.Models.Board`
- MoveModel : The move from a position, to a position. Found in `Norris.Data.Models.Board`
- Color     : Color of the player making the move. Found in `Norris.Data.Models.Board`

# BoardModel DoMove(BoardMoveModel)
## Description
Moves whatever is on a given position to a new position. 
**Disclaimer!** It does not check if it is a valid move. `IsValidMove(...)` is 
expected to be run beforehand.


## DTO information
- BoardModel: The game board. Found in `Norris.Data.Models.Board`
- MoveModel : The move from a position, to a position. Found in `Norris.Data.Models.Board`
- Color     : Color of the player making the move. Found in `Norris.Data.Models.Board`


# BoardModel FillPossibleMoves(BoardMoveModel)
## Description
Sets the bools in the Tiles to true if they are able to do the respective actions.
Returns the new BoardModel with the Tiles modified.

## DTO information
- BoardModel: The game board. Found in `Norris.Data.Models.Board`
- MoveModel : The move from a position, to a position. Found in `Norris.Data.Models.Board`
- Color     : Color of the player making the move. Found in `Norris.Data.Models.Board`