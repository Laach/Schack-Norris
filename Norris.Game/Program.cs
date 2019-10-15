using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Norris.Data.Models.Board;

namespace Norris.Game
{
    public class Program
    {
        public static void Main(string[] args)
        {
          var board = new ChessBoard(new BoardModel(){Board = new Tile[8,8]});
          var p = new Point{Y=4, X=4};
          board[p   ] = Utils.NewTile(PieceType.Pawn, Color.White);
          board[2, 2] = Utils.NewTile(PieceType.King, Color.Black); 

          var xs = new List<Point>();
          
          Utils.PrintBoard(board, xs, Color.White);

        }

    }
}
