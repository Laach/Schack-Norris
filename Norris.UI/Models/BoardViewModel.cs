using Norris.Data.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Norris.UI.Models
{
    public class BoardViewModel
    {
        public GameStateDTO GameState;
        public string SelectedTile;
        public List<string> CanMoveToTiles;
        public List<string> CanMoveToAndTakeTiles;
        public char PlayerColor;
        public string GameId { get; set; }

    }
}
