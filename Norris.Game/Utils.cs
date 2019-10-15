using System;
using System.Linq;
using System.Collections.Generic;
using Norris.Data.Models.Board;

namespace Norris.Game {
  static class Utils {

    public static void PrintBoard(ChessBoard board, IEnumerable<Point> arr, Color player){
      var enemy = player == Color.White ? Color.Black : Color.White;
      for(int i = 0; i < 8; i++){
        for(int j = 0; j < 8; j++){
          if((board[i,j] == null || board[i,j].Piece.Color == enemy) && arr.Contains(new Point{Y=i,X=j})){
            Console.Write(" x ");
          }
          else{
            Console.Write(board[i,j] != null ? " p " :  " - ");
          }
        }
        Console.WriteLine();
      }
    }

    public static Point PositionModelToPoint(PositionModel position){
      Point p = new Point();
      p.X = (int)position.File; // File integers already represents the right index.
      p.Y = 7 - (int)position.Rank; // Rank starts at 1 in bottom left.
      return p;
    }

    public static Color GetOppositeColor(Color color){
      return color == Color.White ? Color.Black : Color.White;
    }
    
    public static Point FindKing(ChessBoard board, Color player){
      for(int i = 0; i < 8; i++){
        for(int j = 0; j < 8; j++){
          if(board[i,j]?.Piece.Type == PieceType.King && board[i,j].Piece.Color == player){
            return new Point(){ Y = i, X = j};
          }
        }
      }
      throw new Exception($"\nError, {player} king not found");
      
    }


    public static ChessBoard CloneBoard(ChessBoard board){

      throw new NotImplementedException();
    }

    



    
  }
}