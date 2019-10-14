# BoardMoveModel
A DTO with all the information necessary for Norris.Game to check if a 
move is possible, and to do the move. 

## Contains:
- BoardModel: The game board. Found in `Norris.Data.Models.Board`
- MoveModel : The move from a position, to a position. Found in `Norris.Data.Models.Board`
- Color     : Color of the player making the move. Found in `Norris.Data.Models.Board`

## Used in:
```
Norris.Game.IsValidMove(BoardMoveModel)
Norris.Game.DoMove(BoardMoveModel)
```

## Makes use of:
- `Norris.Data.Models.Board`