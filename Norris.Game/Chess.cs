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

    public static string[,] InitBoard(){
      return new ChessBoard(Utils.InitBoard()).AsStringMatrix();
    }
    

  }

}