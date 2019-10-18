using System.Collections.Generic;

namespace Norris.Game.Models{
  public class PossibleMovesDTO{
    public List<string> PositionsPieceCanMoveTo {get; set;}
    public List<string> PositionsPieceCanKillAt {get; set;}
  }
}