using Norris.Data.Models.DTO;
using Norris.UI.Models.ManageViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Norris.UI.Models
{
    public class GameViewModel
    {
        public FriendsPartialViewModel FriendsAndGames { get; set; }
        public ChessboardPartialViewModel Board { get; set; }

    }
}
