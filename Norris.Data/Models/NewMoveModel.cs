using Norris.Data.Models.Board;

namespace Norris.Data.Models
{
    public class NewMoveModel
    {
        public string GameID { get; set; }
        public BoardModel CurrentBoard { get; set; }
        public MoveModel NewMove { get; set; }
    }
}
