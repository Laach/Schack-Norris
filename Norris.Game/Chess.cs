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
      if(move.Length < 5){
        throw new ArgumentException($"Move string \"{move}\" too short");
      }
      MoveModel newmove = new MoveModel();
      newmove.From = StringToPosition(move.Substring(0, 2));
      newmove.To   = StringToPosition(move.Substring(3, 2));
      return newmove;
    }



    public static bool IsValidMove(MovePlanModel data){
      ChessBoard board = new ChessBoard(data.Board);
      Point from = Utils.PositionModelToPoint(data.Move.From);
      Point to   = Utils.PositionModelToPoint(data.Move.To  );

      return Logic.IsValidMove(board, from, to, data.Player);
    }



    public static BoardModel DoMove(MovePlanModel data){
      ChessBoard board = new ChessBoard(data.Board);
      Point from = Utils.PositionModelToPoint(data.Move.From);
      Point to   = Utils.PositionModelToPoint(data.Move.To  );

      return Logic.DoMove(board, from, to);
      // b.Board[to.Y, to.X] = b.Board[from.Y, from.X];
      // b.Board[from.Y, from.X] = null;
      // // ChessBoard dummy = Utils.CloneBoard(board);
      // return b;
    }



    public static BoardModel FillPossibleMoves(MovePlanModel data){
      // throw new NotImplementedException();
      ChessBoard board = new ChessBoard(data.Board);
      Point from = Utils.PositionModelToPoint(data.Move.From);


      IEnumerable<Point> moves = Logic.GetMovesFor(board, data.Player, from);

      throw new NotImplementedException();
    }



    public static bool IsWhiteChecked(BoardModel data){
      return Logic.IsChecked(data, Color.White);
    }



    public static bool IsBlackChecked(BoardModel data){
      return Logic.IsChecked(data, Color.Black);
    }


    
    public static BoardModel InitBoard(){
      Tile[,] board = new Tile[8,8];
      board[0,0] = Utils.NewTile(PieceType.Rook, Color.Black);
      board[0,7] = Utils.NewTile(PieceType.Rook, Color.Black);

      board[0,1] = Utils.NewTile(PieceType.Knight, Color.Black);
      board[0,6] = Utils.NewTile(PieceType.Knight, Color.Black);

      board[0,2] = Utils.NewTile(PieceType.Bishop, Color.Black);
      board[0,5] = Utils.NewTile(PieceType.Bishop, Color.Black);

      board[0,3] = Utils.NewTile(PieceType.Queen, Color.Black);
      board[0,4] = Utils.NewTile(PieceType.King, Color.Black );

      for(int i = 0; i < 8; i++){
        board[1,i] = Utils.NewTile(PieceType.Pawn, Color.Black); 
      }



      board[7,0] = Utils.NewTile(PieceType.Rook, Color.White);
      board[7,7] = Utils.NewTile(PieceType.Rook, Color.White);

      board[7,1] = Utils.NewTile(PieceType.Knight, Color.White);
      board[7,6] = Utils.NewTile(PieceType.Knight, Color.White);

      board[7,2] = Utils.NewTile(PieceType.Bishop, Color.White);
      board[7,5] = Utils.NewTile(PieceType.Bishop, Color.White);

      board[7,3] = Utils.NewTile(PieceType.Queen, Color.White);
      board[7,4] = Utils.NewTile(PieceType.King, Color.White );

      for(int i = 0; i < 8; i++){
        board[6,i] = Utils.NewTile(PieceType.Pawn, Color.White); 
      }

      return new BoardModel(){Board = board};

    }

  }

}