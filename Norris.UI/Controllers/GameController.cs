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
            RefreshUser(User);
            var userId = _signInManager.UserManager.GetUserId(User);
            var friends = _GameRepo.GetFriendList(userId);
            //var chat = _GameRepo.GetMessageLog(gameId);
            var games = _GameRepo.GetUserGameList(userId);
            var gamestate = _GameRepo.GetGamestate(gameId);
            var emptyStringList = new List<string>();

            FriendsPartialViewModel friendsAndGames = new FriendsPartialViewModel
            {
                UserFriends = friends,
                UserGames = games
            };

            ChessboardPartialViewModel board = new ChessboardPartialViewModel {
                GameState = gamestate,
                SelectedTile = null,
                CanMoveToAndTakeTiles = emptyStringList,
                CanMoveToTiles = emptyStringList,
                GameId = gameId,
                PlayerColor = _GameRepo.GetPlayerColor(gameId, userId)
            };

            return View(new GameViewModel { FriendsAndGames = friendsAndGames, Board = board, ChatMessages = null});
        }

        public IActionResult ClickedTile(string clickedTile, string gameId, string selectedTile)
        {
            List<string> canMove = new List<string>();
            List<string> canTake = new List<string>();

            string userId = _signInManager.UserManager.GetUserId(User);
            RefreshUser(User);
            GameStateDTO gamestate = _GameRepo.GetGamestate(gameId);
            char userColor = _GameRepo.GetPlayerColor(gameId, userId);

            if (_GameRepo.IsActivePlayer(gameId, userId))
            {
                // clickedPieceType contains color and type, ex "wk" (white king)
                string clickedPieceType = gamestate.Board[7 - (clickedTile[1] - 49), (clickedTile[0] - 97)];

                if (selectedTile == clickedTile)
                {
                    //Clicked the selected tile
                    //it is unselected
                    selectedTile = null;
                }
                else if (userColor == clickedPieceType[0])
                {
                    //Clicked one of their own pieces
                    //it is selected.
                    selectedTile = clickedTile;
                }
                else if(selectedTile != null)
                {
                    //a tile is already selected, and clicked an enemy or empty tile
                    //Can the selected piece move there?

                    MovePlanDTO movePlan = new MovePlanDTO
                    {
                        Board = gamestate.Board,
                        From = selectedTile,
                        To = clickedTile,
                        PlayerColor = userColor
                    };

                    if (_chessLogicManager.IsValidMove(movePlan))
                    {
                        //yes, the selected piece can move there
                        //Apply the game logic, save the new board state, and log the move.
                        string[,] newBoard = _chessLogicManager.DoMove(movePlan);
                        NewMoveDTO newMove = new NewMoveDTO
                        {
                            NewBoard = newBoard,
                            From = selectedTile,
                            To = clickedTile,
                            GameID = gameId
                        };
                        _GameRepo.AddNewMove(newMove);
                        //unselect the old tile
                        selectedTile = null;
                    }
                    else
                    {
                        //clicked a tile that can't be moved to
                        //the selected tile is unselected.
                        selectedTile = null;
                    }
                }

                if (selectedTile != null)
                {
                    //Get the positions we can move to
                    SelectedPieceDTO selectedPiece = new SelectedPieceDTO
                    {
                        Board = gamestate.Board,
                        PlayerColor = userColor,
                        Selected = selectedTile
                    };
                    PossibleMovesDTO possibleMoves = _chessLogicManager.GetPossibleMoves(selectedPiece);
                    canMove = possibleMoves.PositionsPieceCanMoveTo;
                    canTake = possibleMoves.PositionsPieceCanKillAt;
                }
            }

            gamestate = _GameRepo.GetGamestate(gameId);
            //var chat = _GameRepo.GetMessageLog(gameId);
            var friends = _GameRepo.GetFriendList(userId);
            var games = _GameRepo.GetUserGameList(userId);

            FriendsPartialViewModel friendsAndGames = new FriendsPartialViewModel
            {
                UserFriends = friends,
                UserGames = games
            };

            ChessboardPartialViewModel board = new ChessboardPartialViewModel
            {
                GameState = gamestate,
                SelectedTile = selectedTile,
                CanMoveToAndTakeTiles = canTake,
                CanMoveToTiles = canMove,
                GameId = gameId,
                PlayerColor = userColor
            };

            return View("Index", new GameViewModel { FriendsAndGames = friendsAndGames, Board = board, ChatMessages = null } );

        }

        private void RefreshUser(System.Security.Claims.ClaimsPrincipal user){
            var uid = _signInManager.UserManager.GetUserId(User);
            UserActivity.RefreshUser(uid);
        }

        public IActionResult NewGame(string userID)
        {
            var newGameID = _GameRepo.AddNewGame(_signInManager.UserManager.GetUserId(User),userID);

            return RedirectToAction("Index", new { gameId = newGameID });
        }
    }
}