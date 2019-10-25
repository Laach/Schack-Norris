
namespace Norris.Data.Models.DTO
{
    public class NewMoveDTO
    {
        public string GameID          { get; set; }
        public string[,] NewBoard { get; set; }
        public string From            { get; set; }
        public string To              { get; set; }
    }
}
