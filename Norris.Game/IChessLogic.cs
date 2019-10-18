using System;
using System.Linq;
using System.Collections.Generic;
using Norris.Game.Models;
using Norris.Game.Models.DTO;


namespace Norris.Game {
  public interface IChessLogic{
    bool IsValidMove(MovePlanDTO data);

    string[,] DoMove(MovePlanDTO data);

    PossibleMovesDTO FillPossibleMoves(SelectedPieceDTO data);

    bool IsWhiteChecked(string[,] board);

    bool IsBlackChecked(string[,] board);

    string[,] InitBoard();

  }


}