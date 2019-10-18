using System;
using System.Linq;
using System.Collections.Generic;
using Norris.Game.Models;



namespace Norris.Game{
  class ChessBoard {


    public PieceModel[,] board;

    public ChessBoard(string[,] b){
      board = new PieceModel[8,8];
      for(int i = 0; i < 8; i++){
        for(int j = 0; j < 8; j++){
          board[i,j] = StringToPiece(b[i,j]);
        }
      }
    }

    public ChessBoard(PieceModel[,] b){
      board = b;
    }

    public PieceModel this[Point p]{
      get{ return board[p.Y, p.X] ;}
      set{ board[p.Y, p.X] = value;}
    }

    public PieceModel this[int y, int x]{
      get{ return board[y, x] ;}
      set{ board[y, x] = value;}
    }

    private static string PieceToString(PieceModel p){
      string pos = "";
      pos += p.Color == Color.White ? "w" : "b";
      pos += Utils.GetStringRep(p.Type).ToLower();
      return pos;
    }

    private PieceModel StringToPiece(string p){
      PieceModel piece = new PieceModel();
      switch(p[0]){
        case 'w': piece.Color = Color.White; break;
        case 'b': piece.Color = Color.White; break;
        default: throw new ArgumentException();
      }
      switch(p[1]){
        case 'k': piece.Type = PieceType.King  ; break;
        case 'q': piece.Type = PieceType.Queen ; break;
        case 'b': piece.Type = PieceType.Bishop; break;
        case 'r': piece.Type = PieceType.Rook  ; break;
        case 'n': piece.Type = PieceType.Knight; break;
        case 'p': piece.Type = PieceType.Pawn  ; break;
        default: throw new ArgumentException();
      }
      return piece;
    }

    public string[,] AsStringMatrix(){
      string[,] matrix = new string[8,8];
      for(int i = 0; i < 8; i++){
        for(int j = 0; j < 8; j++){
          matrix[i,j] = board[i,j] != null ? PieceToString(board[i,j]) : "ee";
        }
      }

      return matrix;
    }
  }


}