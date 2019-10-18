using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Norris.Data.Models.Board;
using Norris.Game.Models;

namespace Norris.Game{
  static class Test{

    public static void RunGameSample(){
      ChessBoard board = new ChessBoard(Chess.InitBoard());
      var data = System.IO.File.ReadAllLines("schackexempel.txt");
      Color turn = Color.White;
      foreach(var s in data){
        System.Console.WriteLine(s);
        var (from, to) = Utils.MoveToPoints(s);
        if(!Logic.IsValidMove(board, from, to, turn)){
          Console.WriteLine("Invalid move: " + s);
          Utils.PrettyPrint(board);
          return;
        }
        board = Logic.DoMove(board, from, to);
        turn = turn == Color.White ? Color.Black : Color.White;
      }

      Utils.PrettyPrint(board);
    }

    public static void PlayGame(){
      ChessBoard board = new ChessBoard(Chess.InitBoard());
      Color turn = Color.White;
      string s = "";
      while(true){
        Utils.PrettyPrint(board);
        Console.WriteLine($"{turn}, input move: ");
        s = Console.ReadLine();
  
        var (from, to) = Utils.MoveToPoints(s);
        if(!Logic.IsValidMove(board, from, to, turn )){
          Console.Clear();
          Console.WriteLine("Invalid move: " + s);
          // Utils.PrettyPrint(new ChessBoard(board));
          continue;
        }
        else{
          Console.Clear();
          Console.WriteLine();
        }
        board = Logic.DoMove(board, to, from);
        turn = turn == Color.White ? Color.Black : Color.White;
      }
    }







  }
}