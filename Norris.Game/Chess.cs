using System;
using System.Linq;
using System.Collections.Generic;
using Norris.Data.Models.Board;
using Norris.Game.Models;


namespace Norris.Game {

  public static class Chess {




    // static Point StringToPoint(string move){
    //   if(move.Length < 5){
    //     throw new ArgumentException($"Move string \"{move}\" too short");
    //   }
    //   MoveModel newmove = new MoveModel();
    //   newmove.From = StringToPosition(move.Substring(0, 2));
    //   newmove.To   = StringToPosition(move.Substring(3, 2));
    //   return newmove;
    // }


    // bool IsValidMove(MovePlanDTO)
    public static bool IsValidMove(MovePlanModel data){
      ChessBoard board = new ChessBoard(data.Board);
      var (from, to) = Utils.MoveToPoints(data.Move);
      Color player = Utils.CharToColor(data.Player);

      return Logic.IsValidMove(board, from, to, player);
    }



    // string[,] DoMove(MovePlanDTO)
    public static string[,] DoMove(MovePlanModel data){
      ChessBoard board = new ChessBoard(data.Board);
      var (from, to) = Utils.MoveToPoints(data.Move);

      return Logic.DoMove(board, from, to).AsStringMatrix();
    }



    // PossibleMovesDTO FillPossibleMoves(SelectedPieceDTO)
    public static string[,] FillPossibleMoves(MovePlanModel data){
      ChessBoard board = new ChessBoard(data.Board);
      Point from = Utils.StringToPoint(data.Move);
      Color player = Utils.CharToColor(data.Player);


      IEnumerable<Point> moves = Logic.GetMovesFor(board, player, from);

      // Filters out all moves that will make players check themselves.
      IEnumerable<Point> possibleMoves = moves.Where(to => 
        !Logic.IsChecked(Logic.DummyMove(board, from, to, player), player)
      );



      IEnumerable<Point> canMoveTo = possibleMoves.Where(to => 
        board[to] == null
      );

      IEnumerable<Point> canKillAt = possibleMoves.Where(to => 
        board[to] != null && board[to].Color != player
      );



      throw new NotImplementedException();
    }



    public static bool IsWhiteChecked(string[,] board){
      return Logic.IsChecked(new ChessBoard(board), Color.White);
    }



    public static bool IsBlackChecked(string[,] board){
      return Logic.IsChecked(new ChessBoard(board), Color.Black);
    }


    
    public static PieceModel[,] InitBoard(){
      PieceModel[,] board = new PieceModel[8,8];
      board[0,0] = Utils.NewPiece(PieceType.Rook, Color.Black);
      board[0,7] = Utils.NewPiece(PieceType.Rook, Color.Black);

      board[0,1] = Utils.NewPiece(PieceType.Knight, Color.Black);
      board[0,6] = Utils.NewPiece(PieceType.Knight, Color.Black);

      board[0,2] = Utils.NewPiece(PieceType.Bishop, Color.Black);
      board[0,5] = Utils.NewPiece(PieceType.Bishop, Color.Black);

      board[0,3] = Utils.NewPiece(PieceType.Queen, Color.Black);
      board[0,4] = Utils.NewPiece(PieceType.King, Color.Black );

      for(int i = 0; i < 8; i++){
        board[1,i] = Utils.NewPiece(PieceType.Pawn, Color.Black); 
      }



      board[7,0] = Utils.NewPiece(PieceType.Rook, Color.White);
      board[7,7] = Utils.NewPiece(PieceType.Rook, Color.White);

      board[7,1] = Utils.NewPiece(PieceType.Knight, Color.White);
      board[7,6] = Utils.NewPiece(PieceType.Knight, Color.White);

      board[7,2] = Utils.NewPiece(PieceType.Bishop, Color.White);
      board[7,5] = Utils.NewPiece(PieceType.Bishop, Color.White);

      board[7,3] = Utils.NewPiece(PieceType.Queen, Color.White);
      board[7,4] = Utils.NewPiece(PieceType.King, Color.White );

      for(int i = 0; i < 8; i++){
        board[6,i] = Utils.NewPiece(PieceType.Pawn, Color.White); 
      }

      return board;

    }

  }

}