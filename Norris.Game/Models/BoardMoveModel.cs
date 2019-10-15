namespace Norris.Game.Models{
  using Norris.Data.Models.Board;

  public class BoardMoveModel{
    public BoardModel Board {get; set;}
    public MoveModel Move   {get; set;}
    public Color Player     {get; set;}
  }
}