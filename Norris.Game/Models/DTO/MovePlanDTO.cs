namespace Norris.Game.Models.DTO{
  using Norris.Game.Models;

  public class MovePlanDTO{
    public string[,] Board  {get; set;}
    public string From      {get; set;}
    public string To        {get; set;}
    public char PlayerColor {get; set;}
  }
}