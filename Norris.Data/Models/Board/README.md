# ----- Norris.Data.Models.Board -----
# Enum File
The characters representing the horizontal axis on the chess board.

# Enum Rank
The numbers representing vertical axis on the chess board.

The standard contemporary way to record moves and refer to the squares of the 
chessboard is algebraic chess notation The files are identified by the letters 
a to h, from left to right from the white player's point of view, and the ranks 
by the numbers 1 to 8, with 1 being closest to the white player. Each square 
on the board is identified by a unique coordinate pairing, from a1 to h8.
- From https://www.wikiwand.com/en/Chessboard

# PositionModel
A position on the board.

# MoveModel
A move from one position to another.

# PieceType
The different chess pieces.

# Color
Black or white. To represent the two players.

# PieceModel
A chess piece of a type and a color.

# Tile
One position on the board. Contains a PieceModel and some booleans
about what actions are possible for that square.

# BoardModel
A representation of the chess board, as seen from white players perspective 
(meaning the white player is on the bottom rows).