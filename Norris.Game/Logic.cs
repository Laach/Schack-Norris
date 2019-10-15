using System;
using System.Linq;
using System.Collections.Generic;
using Norris.Data.Models.Board;

namespace Norris.Game {

  static class Logic {

    // Ychange and Xchange are how much the point is moved each step.
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


    public static IEnumerable<Point> RookMoves(
      ChessBoard board, 
      Color player, 
      Point point){
      return  LinearMovement(board, player, point, y => y + 1, x => x    )
      .Concat(LinearMovement(board, player, point, y => y - 1, x => x    ))
      .Concat(LinearMovement(board, player, point, y => y    , x => x - 1))
      .Concat(LinearMovement(board, player, point, y => y    , x => x + 1));
    }

    public static IEnumerable<Point> BishopMoves(
      ChessBoard board, 
      Color player, 
      Point point){
      return  LinearMovement(board, player, point, y => y + 1, x => x + 1)
      .Concat(LinearMovement(board, player, point, y => y + 1, x => x - 1))
      .Concat(LinearMovement(board, player, point, y => y - 1, x => x + 1))
      .Concat(LinearMovement(board, player, point, y => y - 1, x => x - 1));
    }

    public static IEnumerable<Point> QueenMoves(
      ChessBoard board, 
      Color player, 
      Point point){

      return RookMoves(board, player, point)
      .Concat(BishopMoves(board, player, point));
    }


    public static IEnumerable<Point> GetMovesFor(
      ChessBoard board, 
      Color player, 
      Point point){
      
      switch(board[point]?.Piece.Type){
        case PieceType.King  : return KingMoves  (board, player, point);
        case PieceType.Queen : return QueenMoves (board, player, point);
        case PieceType.Rook  : return RookMoves  (board, player, point);
        case PieceType.Bishop: return BishopMoves(board, player, point);
        default: return new List<Point>();
      }

    }

    public static bool IsChecked(BoardModel b, Color color){
      ChessBoard board = new ChessBoard(b);
      IEnumerable<Point> enemyMoves = Utils.GetAllMovesFor(
                                            board, t => t.Piece.Color != color);
      Point king = Utils.FindKing(board, color);

      return enemyMoves.Contains(king);
    }

  }
}