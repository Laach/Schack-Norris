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
          board[p   ] = NewTile(PieceType.King, Color.White);
          board[2, 2] = NewTile(PieceType.King, Color.White); 
          board[5, 7] = NewTile(PieceType.Pawn, Color.Black); 
          board[0, 7] = NewTile(PieceType.King, Color.Black); 
          
          // var xs = Utils.GetAllMovesFor(board, t => t.Piece.Color == Color.White);
          // var xs = Logic.GetMovesFor(board, Color.White, p);
          // var xs = Logic.LinearMovement(board, Color.White, p, y => y + 1, x => x + 1, 1);
          // Utils.PrintBoard(board, xs, Color.White);

        }

        public static Tile NewTile(PieceType type, Color color){
          return new Tile(){
            Piece = new PieceModel(){
              Type = type, Color = color
            }
          };
        }
    }
}
