using Norris.Data.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Norris.UI.Models
{
    public class ArchivedGamesViewModel
    {
        public IEnumerable<ArchivedGamesDTO> ArchivedGames;
        public ChessboardPartialViewModel Board;
    }
}
