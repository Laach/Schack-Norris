using System;
using System.Collections.Generic;
using Norris.Data.Models.Board;



namespace Norris.Game{
  class ChessBoard {


    public BoardModel board;

    public ChessBoard(BoardModel b){
      board = b;
    }

    public Tile this[Point p]{
      get{ return board.Board[p.Y, p.X] ;}
      set{ board.Board[p.Y, p.X] = value;}
    }

    public Tile this[int y, int x]{
      get{ return board.Board[y, x] ;}
      set{ board.Board[y, x] = value;}
    }


  }


}