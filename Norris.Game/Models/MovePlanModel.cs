namespace Norris.Game.Models{
  using Norris.Data.Models.Board;

  public class MovePlanModel{
    public string[,] Board {get; set;}
    public string Move     {get; set;}
    public char Player     {get; set;}
  }
}