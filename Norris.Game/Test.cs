using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Norris.Game.Models;

namespace Norris.Game{
  static class Test{

    public static void RunGameSample(){
      ChessLogic Chess = new ChessLogic();
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

    public static ValueTuple<ChessBoard, Color> RunInput(){
      ChessLogic Chess = new ChessLogic();
      ChessBoard board = new ChessBoard(Chess.InitBoard());
      var data = System.IO.File.ReadAllLines("testgame.txt");
      Color turn = Color.White;
      foreach(var s in data){
        if(s == ""){break;}
        var (from, to) = Utils.MoveToPoints(s);
        if(!Logic.IsValidMove(board, from, to, turn)){
          Console.WriteLine("Invalid move: " + s);
          Utils.PrettyPrint(board);
          Environment.Exit(1);
        }
        board = Logic.DoMove(board, from, to);
        turn = turn == Color.White ? Color.Black : Color.White;
      }
      return (board, turn);
    }

    public static void PlayGame(){
      ChessLogic Chess = new ChessLogic();
      ChessBoard board = new ChessBoard(Chess.InitBoard());
      Color turn = Color.White;
      // var (board, turn) = RunInput();
      string s = "";
      while(true){
        Utils.PrettyPrint(board);
        if(Logic.IsCheckMate(board, turn)){
          System.Console.WriteLine("Winner is " + Utils.GetOppositeColor(turn).ToString());
        }

        Console.WriteLine($"{turn}, input move: ");
        s = Console.ReadLine();
  
        var (from, to) = Utils.MoveToPoints(s);
        if(!Logic.IsValidMove(board, from, to, turn )){
          Console.Clear();
          Console.WriteLine("Invalid move: " + s);
          continue;
        }
        else{
          Console.Clear();
          Console.WriteLine();
        }
        board = Logic.DoMove(board, from, to);
        turn = turn == Color.White ? Color.Black : Color.White;
      }
    }


    public static void RecordGame(string path){
      ChessLogic Chess = new ChessLogic();
      ChessBoard board = new ChessBoard(Chess.InitBoard());
      Color turn = Color.White;
      // var (board, turn) = RunInput();
      string s = "";
      while(true){
        Utils.PrettyPrint(board);
        if(Logic.IsCheckMate(board, turn)){
          System.Console.WriteLine("Winner is " + Utils.GetOppositeColor(turn).ToString());
        }

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
        using (var sw = new StreamWriter(path, true)){
          sw.WriteLine(s);
        }
        board = Logic.DoMove(board, from, to);
        turn = turn == Color.White ? Color.Black : Color.White;
      }

    }







  }
}