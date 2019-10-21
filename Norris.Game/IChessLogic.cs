using System;
using System.Linq;
using System.Collections.Generic;
using Norris.Game.Models;
using Norris.Game.Models.DTO;


namespace Norris.Game {
  public interface IChessLogic{
    bool IsValidMove(MovePlanDTO data);

    string[,] DoMove(MovePlanDTO data);

    PossibleMovesDTO GetPossibleMoves(SelectedPieceDTO data);

    bool IsWhiteChecked(string[,] board);

    bool IsBlackChecked(string[,] board);

    bool IsWhiteCheckMate(string[,] board);

    bool IsBlackCheckMate(string[,] board);

    string[,] InitBoard();

  }


}