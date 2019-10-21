
namespace Norris.Data.Models.Board{
  public class Tile{
    public PieceModel Piece {get; set;}
    public bool IsSelected  {get; set;}
    public bool SelectedCanMoveTo   {get; set;}
    public bool SelectedCanTake {get; set;}
  }
}