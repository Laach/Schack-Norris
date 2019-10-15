using System;
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

    public static BoardModel IsValidMove(BoardMoveModel data){
      throw new NotImplementedException();
      // false if from position does not match color
    }

    public static BoardModel DoMove(BoardMoveModel data){
      BoardModel b = data.Board;
      var from = Utils.PositionModelToPoint(data.Move.From);
      var to   = Utils.PositionModelToPoint(data.Move.To  );

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