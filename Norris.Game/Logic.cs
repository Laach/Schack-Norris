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

      var moves = new Point[]{
        new Point(){ Y = point.Y + 1, X = point.X + 1},
        new Point(){ Y = point.Y + 1, X = point.X - 1},
        new Point(){ Y = point.Y - 1, X = point.X + 1},
        new Point(){ Y = point.Y - 1, X = point.X - 1},

        new Point(){ Y = point.Y + 1, X = point.X    },
        new Point(){ Y = point.Y - 1, X = point.X    },
        new Point(){ Y = point.Y    , X = point.X + 1},
        new Point(){ Y = point.Y    , X = point.X - 1},
      };
      return moves.Where(xy => Inbounds(xy) && CanGoTo(board, xy, player));
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

    public static IEnumerable<Point> KnightMoves(
      ChessBoard board, 
      Color player, 
      Point point){

      var moves = new Point[]{
        new Point(){ Y = point.Y + 1, X = point.X - 2},
        new Point(){ Y = point.Y - 1, X = point.X - 2},

        new Point(){ Y = point.Y + 1, X = point.X + 2},
        new Point(){ Y = point.Y - 1, X = point.X + 2},

        new Point(){ Y = point.Y + 2, X = point.X + 1},
        new Point(){ Y = point.Y + 2, X = point.X - 1},

        new Point(){ Y = point.Y - 2, X = point.X + 1},
        new Point(){ Y = point.Y - 2, X = point.X - 1},
      };
      return moves.Where(xy => Inbounds(xy) && CanGoTo(board, xy, player));
    }

    static bool PawnCanAttack(ChessBoard board, Point p, Color c){
      return Inbounds(p) && board[p] != null && board[p].Piece.Color == c;
    }

    static IEnumerable<Point> PawnStartPositions(int y){
      List<Point> list = new List<Point>();
      for(int i = 0; i < 8; i++){
        list.Add(new Point(){Y = y, X = i});
      }
      return list;
    }

    public static IEnumerable<Point> PawnMoves(
      ChessBoard board, 
      Color player, 
      Point point){

      IEnumerable<Point> list = new List<Point>();
      if(player == Color.White){
        Point forward = new Point(){ Y = point.Y - 1, X = point.X};
        if(board[forward] == null){
          list = list.Append(forward);
        }

        var attack1 = new Point(){ Y = point.Y - 1, X = point.X + 1};
        var attack2 = new Point(){ Y = point.Y - 1, X = point.X - 1};

        if(PawnCanAttack(board, attack1, Color.Black)){
          list = list.Append(attack1);
        }
        if(PawnCanAttack(board, attack2, Color.Black)){
          list = list.Append(attack2);
        }

        IEnumerable<Point> startPositions = PawnStartPositions(6);
        if(startPositions.Contains(point)){
          list = list.Append(new Point(){ Y = point.Y - 2, X = point.X});
        }

      }
      else{
        Point forward = new Point(){ Y = point.Y + 1, X = point.X};
        if(board[forward] == null){
          list = list.Append(forward);
        }

        var attack1 = new Point(){ Y = point.Y + 1, X = point.X + 1};
        var attack2 = new Point(){ Y = point.Y + 1, X = point.X - 1};

        if(PawnCanAttack(board, attack1, Color.White)){
          list = list.Append(attack1);
        }
        if(PawnCanAttack(board, attack2, Color.White)){
          list = list.Append(attack2);
        }

        IEnumerable<Point> startPositions = PawnStartPositions(1);
        if(startPositions.Contains(point)){
          list = list.Append(new Point(){ Y = point.Y + 2, X = point.X});
        }

      }
      return list;

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
        case PieceType.Knight: return KnightMoves(board, player, point);
        case PieceType.Pawn  : return PawnMoves  (board, player, point);
        default: return new List<Point>();
      }

    }

    public static bool IsChecked(ChessBoard board, Color color){
      IEnumerable<Point> enemyMoves = Utils.GetAllMovesFor(
                                            board, t => t.Piece.Color != color);
      Point king = Utils.FindKing(board, color);

      return enemyMoves.Contains(king);
    }



    public static ChessBoard DoMove(ChessBoard board, Point from, Point to){

      board[to] = board[from];
      board[from] = null;
      // ChessBoard dummy = Utils.CloneBoard(board);
      return board;
    }

    public static ChessBoard DummyMove(
      ChessBoard board, 
      Point from, 
      Point to, 
      Color player){

      return Logic.DoMove(Utils.CloneBoard(board), from, to);
    }

    public static bool IsValidMove(ChessBoard board, Point from, Point to, Color player){
      // Position moving from is not a piece or is enemy piece.
      if(board[from] == null || board[from].Piece.Color != player){
        return false;
      }


      // Do a dummy move and see if player will place themselves in check.
      if(Logic.IsChecked(DummyMove(board, from, to, player), player)){
        return false;
      }

      // See if the new position is available in the moveset for that tile.
      IEnumerable<Point> moves = Logic.GetMovesFor(board, player, from);
      return moves.Contains(to);
    }

  }
}