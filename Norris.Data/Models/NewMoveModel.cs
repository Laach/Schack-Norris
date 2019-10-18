
namespace Norris.Data.Models
{
    public class NewMoveModel
    {
        public string GameID { get; set; }
        public string[,] CurrentBoard { get; set; }
        public string NewMove { get; set; }
    }
}
