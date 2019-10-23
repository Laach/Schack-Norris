using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Norris.Data;
using Norris.Data.Data.Entities;
using Norris.Data.Models.DTO;
using Norris.Game;
using Norris.Game.Models.DTO;
using Norris.UI.Models;

namespace Norris.UI.Controllers
{
    public class GameController : Controller
    {
        private readonly UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly IGameRepository _GameRepo;
        private IChessLogic _chessLogicManager;


        public GameController(SignInManager<User> sim, IGameRepository GameRepo, UserManager<User> userManager, IChessLogic chessLogicManager)
        {
            _signInManager = sim;
            _GameRepo = GameRepo;
            _userManager = userManager;
            _chessLogicManager = chessLogicManager;
        }

        public IActionResult Index(string gameId)
        {
            ViewData["Message"] = "Game view.";
            var friends = _GameRepo.GetFriendList(2);
            var gamestate = _GameRepo.GetGamestate(gameId);
            var emptyStringList = new List<string>();
            var board = new BoardViewModel { GameState = gamestate, SelectedTile = null, CanMoveToAndTakeTiles = emptyStringList, CanMoveToTiles = emptyStringList, GameId = gameId };
            return View(new GameViewModel { UserList = friends, Board = board});
        }

        public IActionResult ClickedTile(string clickedPosition, string gameId, char activePlayerColor, List<string> log,  string selectedTile)
        {
            if(selectedTile == null)
            {
                char userColor = activePlayerColor;
                var piece = _GameRepo.GetGamestate(gameId).Board[7 - (clickedPosition[1] - 49), 7 - (clickedPosition[0] - 97)];

                if (userColor == piece[0])
                {
                    selectedTile = clickedPosition;
                }
            } else if (selectedTile == clickedPosition)
            {
                selectedTile = null;
            } else
            {
                //has selected a tile and has clicked any other tile
                //Can the selected piece move there?
                
                MovePlanDTO movePlan = new MovePlanDTO
                {
                    Board = _GameRepo.GetGamestate(gameId).Board,
                    From = selectedTile,
                    To = clickedPosition,
                    PlayerColor = activePlayerColor
                };
                if (_chessLogicManager.IsValidMove(movePlan))
                {
                    string[,] newBoard = _chessLogicManager.DoMove(movePlan);
                    NewMoveDTO newMove = new NewMoveDTO
                    {
                        CurrentBoard = newBoard,
                        From = selectedTile,
                        To = clickedPosition,
                        GameID = gameId
                    };
                    _GameRepo.AddNewMove(newMove);
                }
                else
                {
                    selectedTile = null;
                }
            }

            List<string> canMove = new List<string>();
            List<string> canTake = new List<string>();

            if (selectedTile != null)
            {
                //Get the positions we can move to
                SelectedPieceDTO selectedPiece = new SelectedPieceDTO
                {
                    Board = _GameRepo.GetGamestate(gameId).Board,
                    PlayerColor = activePlayerColor,
                    Selected = selectedTile
                };
                PossibleMovesDTO possibleMoves = _chessLogicManager.GetPossibleMoves(selectedPiece);
                canMove = possibleMoves.PositionsPieceCanMoveTo;
                canTake = possibleMoves.PositionsPieceCanKillAt;
            } 

            var gamestate = _GameRepo.GetGamestate(gameId);
            var friends = _GameRepo.GetFriendList(2);
            var board = new BoardViewModel { GameState = gamestate, SelectedTile = selectedTile, CanMoveToAndTakeTiles = canTake, CanMoveToTiles = canMove, GameId = gameId };
            return View("Index", new GameViewModel { UserList = friends, Board = board } );

        }
    }
}