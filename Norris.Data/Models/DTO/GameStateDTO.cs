using System;
using System.Collections.Generic;

namespace Norris.Data.Models.DTO
{
    public class GameStateDTO
    {
        public List<string> Log       {get; set;}
        public char ActivePlayerColor {get; set;}
        public string[,] Board        {get; set;}
        public int MovesCounter { get; set; }
    }
}
