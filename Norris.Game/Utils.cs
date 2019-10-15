using System;
using System.Linq;
using System.Collections.Generic;
using Norris.Data.Models.Board;

namespace Norris.Game {
  static class Utils {

    static string GetStringRep(PieceType type){
      switch(type){
        case PieceType.Bishop: return "B";
        case PieceType.Pawn  : return "P";
        case PieceType.Rook  : return "R";
        case PieceType.Knight: return "N";
        case PieceType.Queen : return "Q";
        case PieceType.King  : return "K";
        default: return "";
      }
    }

    static string GetColorString(Color c) => c == Color.White ? "W" : "B";

    public static void PrettyPrint(ChessBoard board){
      for(int i = 0; i < 8; i++){
        Console.Write("\n-------------------------\n|");
        for(int j = 0; j < 8; j++){

          string a = board[i,j] == null ? "  " : GetColorString(
            board[i,j].Piece.Color) + GetStringRep(board[i,j].Piece.Type
          );

          Console.Write(a + "|");
        }
      }
      Console.WriteLine("\n-------------------------");

    }

    public static void PrintMoveset(
      ChessBoard board, IEnumerable<Point> arr, Color player){
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

    public static Point FindFirst(ChessBoard board, Func<Tile, bool> f){
      for(int i = 0; i < 8; i++){
        for(int j = 0; j < 8; j++){
          if(board[i,j] != null && f(board[i,j])){
            return new Point(){ Y = i, X = j};
          }
        }
      }
      throw new Exception($"\nError, tile not found");
 
    }

    public static Point FindKing(ChessBoard board, Color player){
      try{
        return FindFirst(
          board, t => t.Piece.Type == PieceType.King && t.Piece.Color == player
        );
      }
      catch{
        throw new Exception($"\nError, {player} king not found");
      }
    }

    static Tile CloneTile(Tile tile){
      return new Tile(){
        Piece = new PieceModel(){
          Type = tile.Piece.Type,
          Color = tile.Piece.Color
        }
      };
    }

    public static ChessBoard CloneBoard(ChessBoard board){

      BoardModel clone = new BoardModel(){Board = new Tile[8,8]};
      for(int i = 0; i < 8; i++){
        for(int j = 0; j < 8; j++){
          if(board[i,j] != null){
            clone.Board[i,j] = CloneTile(board[i,j]);
          }
        }
      }

      return new ChessBoard(clone);
    }

    
    public static IEnumerable<Point> FoldIEnum(IEnumerable<Point>[] arr){
      IEnumerable<Point> list = new List<Point>();
      foreach(var ienum in arr){
        list = list.Concat(ienum);
      }
      return list;
    }

    public static IEnumerable<Point> GetAllMovesFor(ChessBoard board, Func<Tile, bool> f){
      IEnumerable<Point> list = new List<Point>(){};
      for(int i = 0; i < 8; i++){
        for(int j = 0; j < 8; j++){
          if(board[i,j] != null && f(board[i, j])){
            var p = new Point(){ Y = i, X = j};
            list = list.Concat(
              Logic.GetMovesFor(board, board[i,j].Piece.Color, p)
            );
          }
        }
      }
      return list;
    }

    public static Tile NewTile(PieceType type, Color color){
      return new Tile(){
        Piece = new PieceModel(){
          Type = type, Color = color
        }
      };
    }
    
  }
}