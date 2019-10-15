using System;
using System.Linq;
using System.Collections.Generic;
using Norris.Data.Models.Board;
using Norris.Game.Models;


namespace Norris.Game {

  public static class Chess {

    public static PositionModel StringToPosition(string pos){
      if (pos.Length < 2){
        throw new ArgumentException($"\nString \"{pos}\" is too short");
      }
      File file; 
      switch (Char.ToLower(pos[0])){
        case 'a': file = File.A; break;
        case 'b': file = File.B; break;
        case 'c': file = File.C; break;
        case 'd': file = File.D; break;
        case 'e': file = File.E; break;
        case 'f': file = File.F; break;
        case 'g': file = File.G; break;
        case 'h': file = File.H; break;
        default: throw new ArgumentException($"\nInvalid file: {pos[0]} ");
      }
      Rank rank;
      switch(pos[1]){
        case '1': rank = Rank.One;   break;
        case '2': rank = Rank.Two;   break;
        case '3': rank = Rank.Three; break;
        case '4': rank = Rank.Four;  break;
        case '5': rank = Rank.Five;  break;
        case '6': rank = Rank.Six;   break;
        case '7': rank = Rank.Seven; break;
        case '8': rank = Rank.Eight; break;
        default: throw new ArgumentException($"\nInvalid rank: {pos[1]} ");
      }

      return new PositionModel(){File = file, Rank = rank};
    }

    public static MoveModel StringToMove(string move){
      MoveModel newmove = new MoveModel();
      newmove.From = StringToPosition(move.Substring(0, 2));
      newmove.To   = StringToPosition(move.Substring(3, 2));
      return newmove;
    }

    public static bool IsValidMove(BoardMoveModel data){
      ChessBoard board = new ChessBoard(data.Board);
      Point from = Utils.PositionModelToPoint(data.Move.From);
      Point to   = Utils.PositionModelToPoint(data.Move.To  );

      // Position moving from is not a piece or is enemy piece.
      if(board[from] == null || board[from].Piece.Color != data.Player){
        return false;
      }


      // Do a dummy move and see if player will place themselves in check.
      ChessBoard dummy = Utils.CloneBoard(board);
      dummy.board = DoMove(new BoardMoveModel(){
                            Board = dummy.board, Move=data.Move, Player=data.Player});

      if(Logic.IsChecked(dummy.board, data.Player)){
        return false;
      }

      // See if the new position is available in the moveset for that tile.
      IEnumerable<Point> moves = Logic.GetMovesFor(board, data.Player, from);
      return moves.Contains(to);
    }

    public static BoardModel DoMove(BoardMoveModel data){
      BoardModel b = data.Board;
      Point from = Utils.PositionModelToPoint(data.Move.From);
      Point to   = Utils.PositionModelToPoint(data.Move.To  );

      b.Board[to.Y, to.X] = b.Board[from.Y, from.X];
      b.Board[from.Y, from.X] = null;
      // ChessBoard dummy = Utils.CloneBoard(board);
      return b;
    }

    public static BoardModel FillPossibleMoves(BoardMoveModel data){
      throw new NotImplementedException();
    }

    public static bool IsWhiteChecked(BoardModel data){
      return Logic.IsChecked(data, Color.White);
    }

    public static bool IsBlackChecked(BoardModel data){
      return Logic.IsChecked(data, Color.Black);
    }
    // static IEnumerable<PositionModel> LinearMovement(){
    //   var a = new ChessBoard.ChessBoard();
    // }
    








  }







}