using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Norris.Data.Models.Board;
using Norris.Game.Models;

namespace Norris.Game{
  static class Test{

    public static void RunGameSample(){
      BoardModel board = Chess.InitBoard();
      var data = System.IO.File.ReadAllLines("schackexempel.txt");
      Color turn = Color.White;
      foreach(var s in data){
        System.Console.WriteLine(s);
        MoveModel move = Chess.StringToMove(s);
        var movemodel = new BoardMoveModel();
        movemodel.Board  = board;
        movemodel.Player = turn;
        movemodel.Move   = move;
        if(!Chess.IsValidMove(movemodel)){
          Console.WriteLine("Invalid move: " + s);
          Utils.PrettyPrint(new ChessBoard(board));
          return;
        }
        board = Chess.DoMove(movemodel);
        turn = turn == Color.White ? Color.Black : Color.White;
      }

      Utils.PrettyPrint(new ChessBoard(board));
    }

    public static void PlayGame(){
      BoardModel board = Chess.InitBoard();
      Color turn = Color.White;
      string s = "";
      while(true){
        Utils.PrettyPrint(new ChessBoard(board));
        Console.WriteLine($"{turn}, input move: ");
        s = Console.ReadLine();
  
        MoveModel move = Chess.StringToMove(s);
        var movemodel = new BoardMoveModel();
        movemodel.Board  = board;
        movemodel.Player = turn;
        movemodel.Move   = move;
        if(!Chess.IsValidMove(movemodel)){
          Console.Clear();
          Console.WriteLine("Invalid move: " + s);
          // Utils.PrettyPrint(new ChessBoard(board));
          continue;
        }
        else{
          Console.Clear();
          Console.WriteLine();
        }
        board = Chess.DoMove(movemodel);
        turn = turn == Color.White ? Color.Black : Color.White;
      }
    }








  }
}