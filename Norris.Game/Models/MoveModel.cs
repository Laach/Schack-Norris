
namespace Norris.Game.Models{
  struct MoveModel{
    public Point From {get; set;}
    public Point To   {get; set;}

    public override string ToString(){
      return "From: " + From.ToString() + "|   To: " + To.ToString();
    }
  }
}