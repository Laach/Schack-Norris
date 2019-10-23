
namespace Norris.Data.Models.DTO
{
    public class UserActiveGamesDTO
    {
        public string GameID       { get; set; }
        public bool IsMyTurn       { get; set; }
        public string OpponentName { get; set; }
    }
}