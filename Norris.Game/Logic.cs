using System;
using System.Linq;
using System.Collections.Generic;
using Norris.Data.Models.Board;

namespace Norris.Game {

  static class Logic {

    public static IEnumerable<Point> LinearMovement(
      ChessBoard board, Color player, 
      Point p, 
      Func<int,int> Ychange, 
      Func<int,int> Xchange,
      int steps = 8){

      Point xy = p;
      do{
        xy.Y = Ychange(xy.Y);
        xy.X = Xchange(xy.X);
        steps--;
        if(!Inbounds(xy)) { yield break; }

        if(IsEnemy(board, xy, player)){
          yield return xy;
          yield break;
        }
        else if (board[xy] != null) { yield break; }

        yield return xy;
      } while(steps > 0);
    }


    public static bool Inbounds(Point p){
      return p.X < 8 && p.X >= 0 && p.Y < 8 && p.Y >= 0;
    }


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

    static bool CanGoTo(ChessBoard board, Point point, Color player){
      return board[point] == null || board[point].Piece.Color != player;
    }

    static bool IsEnemy(ChessBoard board, Point point, Color player){
      return board[point] != null && board[point].Piece.Color != player;
    }






    public static IEnumerable<Point> KingMoves(
      ChessBoard board, 
      Color player, 
      Point point){

      var arr = new Point[]{
        new Point(){ Y = point.Y + 1, X = point.X + 1},
        new Point(){ Y = point.Y + 1, X = point.X - 1},
        new Point(){ Y = point.Y - 1, X = point.X + 1},
        new Point(){ Y = point.Y - 1, X = point.X - 1},

        new Point(){ Y = point.Y + 1, X = point.X    },
        new Point(){ Y = point.Y - 1, X = point.X    },
        new Point(){ Y = point.Y    , X = point.X + 1},
        new Point(){ Y = point.Y    , X = point.X - 1},
      };
      return arr.Where(xy => Inbounds(xy) && CanGoTo(board, xy, player));
    }


    public static IEnumerable<Point> RookMove(
      ChessBoard board, 
      Color player, 
      Point point){
      return  LinearMovement(board, player, point, y => y + 1, x => x    )
      .Concat(LinearMovement(board, player, point, y => y - 1, x => x    ))
      .Concat(LinearMovement(board, player, point, y => y    , x => x - 1))
      .Concat(LinearMovement(board, player, point, y => y    , x => x + 1));

    }

    public static IEnumerable<Point> BishopMove(
      ChessBoard board, 
      Color player, 
      Point point){
      return  LinearMovement(board, player, point, y => y + 1, x => x + 1)
      .Concat(LinearMovement(board, player, point, y => y + 1, x => x - 1))
      .Concat(LinearMovement(board, player, point, y => y - 1, x => x + 1))
      .Concat(LinearMovement(board, player, point, y => y - 1, x => x - 1));
    }

    public static IEnumerable<Point> QueenMove(
      ChessBoard board, 
      Color player, 
      Point point){

      return RookMove(board, player, point)
      .Concat(BishopMove(board, player, point));
    }


  }
}