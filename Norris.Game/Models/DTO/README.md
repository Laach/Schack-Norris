# MovePlanDTO
A DTO with all the information necessary for Norris.Game to check if a 
move is possible, and to do the move. 

## Contains:
- string[,]    Board: The game board.
- string        From: A position on the board (eg. a4).
- string          To: A position on the board (eg. a4).
- string PlayerColor: Char representing the color of current player ('w' or 'b').

## Used in:
```
Norris.Game.IsValidMove(MovePlanDTO)
Norris.Game.DoMove(MovePlanDTO)
```


# SelectedPieceDTO
A DTO with all the information necessary for Norris.Game to fetch all possible 
actions for the selected piece.

## Contains:
- string[,]    Board: The game board.
- string    Selected: A position on the board (eg. a4).
- string PlayerColor: Char representing the color of current player ('w' or 'b').

## Used in:
```
Norris.Game.GetPossibleMoves(SelectedPieceDTO)
```

# PossibleMovesDTO
A DTO with all the information necessary for Norris.Game to fetch all possible 
actions for the selected piece.

## Contains:
- List\<string> PositionsPieceCanMoveTo: The positions the selected piece can move to.
- List\<string> PositionsPieceCanKillAt: The positions the selected piece can move to and take an enemy piece.

## Returned from:
```
Norris.Game.GetPossibleMoves(SelectedPieceDTO)
```
