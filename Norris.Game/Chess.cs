using System;
using System.Linq;
using System.Collections.Generic;
using Norris.Game.Models;
using Norris.Game.Models.DTO;


namespace Norris.Game {

  public static class Chess {


    public static bool IsValidMove(MovePlanDTO data){
      ChessBoard board = new ChessBoard(data.Board);
      Point from = Utils.StringToPoint(data.From);
      Point to   = Utils.StringToPoint(data.To);
      Color player = Utils.CharToColor(data.PlayerColor);

      return Logic.IsValidMove(board, from, to, player);
    }



    public static string[,] DoMove(MovePlanDTO data){
      ChessBoard board = new ChessBoard(data.Board);
      Point from = Utils.StringToPoint(data.From);
      Point to   = Utils.StringToPoint(data.To);

      return Logic.DoMove(board, from, to).AsStringMatrix();
    }



    public static PossibleMovesDTO FillPossibleMoves(SelectedPieceDTO data){
      ChessBoard board = new ChessBoard(data.Board);
      Point selected = Utils.StringToPoint(data.Selected);
      Color player = Utils.CharToColor(data.PlayerColor);

      return Logic.FillPossibleMoves(board, selected, player);
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