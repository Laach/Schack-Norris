
namespace Norris.Data.Models.DTO
{
    public class UserActiveGamesDTO
    {
        public string GameID       { get; set; }
        public bool IsMyTurn       { get; set; }
        public string OpponentName { get; set; }
        public char PlayerColor { get; set; }
        public int MovesCounter { get; set; }
    }
}