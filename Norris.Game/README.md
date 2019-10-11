# bool TryDoMove(BoardMoveModel)
### Description
Takes in all the necessary information to attempt a move. If the move 
succeeds it returns true and has changed the positions inside BoardModel.
It does not touch the booleans in Tile.

## DTO information
- BoardModel: The game board. Found in `Norris.Data.Models.Board`
- MoveModel : The move from a position, to a position. Found in `Norris.Data.Models.Board`
- Color     : Color of the player making the move. Found in `Norris.Data.Models.Board`


# void FillPossibleMoves(BoardMoveModel)
## Description
Sets the bools in the Tiles to true if they are able to do the respective actions.

## DTO information
- BoardModel: The game board. Found in `Norris.Data.Models.Board`
- MoveModel : The move from a position, to a position. Found in `Norris.Data.Models.Board`
- Color     : Color of the player making the move. Found in `Norris.Data.Models.Board`