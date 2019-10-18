
namespace Norris.Game.Models{

  public class SelectedPieceDTO{
    public string[,] Board  {get; set;}
    public string Selected  {get; set;}
    public char PlayerColor {get; set;}
  }
}