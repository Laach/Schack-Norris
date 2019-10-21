# [bool IsValidMove(MovePlanModel)](/Norris.Game/Chess.cs)
## Description
Checks if a given move is valid for a player on a given board.

## DTO information
[MovePlanModel](/Norris.Game/Models/README.md)


# [BoardModel DoMove(MovePlanModel)](/Norris.Game/Chess.cs)
## Description
Moves whatever is on a given position to a new position. 
**Disclaimer!** It does not check if it is a valid move. `IsValidMove(...)` is 
expected to be run beforehand.

## DTO information
[MovePlanModel](/Norris.Game/Models/README.md)


# [BoardModel FillPossibleMoves(MovePlanModel)](/Norris.Game/Chess.cs)
## Description
Sets the bools in the Tiles to true if they are able to do the respective actions.
Returns the new BoardModel with the Tiles modified.

## DTO information
[MovePlanModel](/Norris.Game/Models/README.md)


# [PositionModel StringToPosition(string)](/Norris.Game/Chess.cs)
## Description
Takes in a string and returns it as a [PositionModel](/Norris.Data/Models/Board/README.md).
The string has to be in the format **RankFile**, eg. **a2**. 

## Exceptions
- ArgumentException if the string is not in the correct format.


# [MoveModel StringToMove(string)](/Norris.Game/Chess.cs)
## Description 
Takes in a string containing two moves and returns it as a 
[MoveModel](/Norris.Data/Models/Board/README.md). The string has to be 
in the format **RankFile RankFile**, eg. **a2 a4**. 

## Exceptions
- ArgumentException if the string is not in the correct format.


# [bool IsWhiteChecked(BoardModel)](/Norris.Game/Chess.cs)
## Description
Takes in a board and returns if white king is checked.


# [bool IsBlackChecked(BoardModel)](/Norris.Game/Chess.cs)
## Description
Takes in a board and returns if black king is checked.