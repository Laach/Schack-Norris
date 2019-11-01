using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Norris.Data.Models.DTO
{
    public class ArchivedGamesDTO
    {
        public string GameID { get; set; }
        public bool AmIWinner { get; set; }
        public string OpponentName { get; set; }
    }
}