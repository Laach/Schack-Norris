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
            // BuildWebHost(args).Run();
          var board = new ChessBoard(new BoardModel(){Board = new Tile[8,8]});
          var p = new Point{Y=4, X=4};
          // var p = new Point{Y=2, X=5};
          board[p   ] = NewTile(PieceType.King, Color.White);
          board[3, 3] = NewTile(PieceType.Pawn, Color.White); 
          board[5, 5] = NewTile(PieceType.Pawn, Color.Black); 
          
          // var xs = Logic.LinearMovement(board, Color.White, p, y => y + 1, x => x + 1, 1);
          Logic.PrintBoard(board, xs, Color.White);

        }

        public static Tile NewTile(PieceType type, Color color){
          return new Tile(){
            Piece = new PieceModel(){
              Type = type, Color = color
            }
          };
        }

      // public static IWebHost BuildWebHost(string[] args) =>
      //     WebHost.CreateDefaultBuilder(args)
      //         .UseStartup<Startup>()
      //         .Build();
    }
}
