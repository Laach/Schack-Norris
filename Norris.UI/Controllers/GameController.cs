using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
            var games = _GameRepo.GetUserGameList(userId);
            var gamestate = _GameRepo.GetGamestate(gameId);
            var emptyStringList = new List<string>();
            List<string> changedTiles = new List<string>();

            foreach(char f in Enumerable.Range('a', 8))
            {
                foreach(char r in Enumerable.Range('1', 8))
                {
                    changedTiles.Add(f.ToString() + r.ToString());
                }
            }

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
                PlayerColor = _GameRepo.GetPlayerColor(gameId, userId),
                ChangedTiles = changedTiles
            };

            return View(new GameViewModel { FriendsAndGames = friendsAndGames, Board = board});
        }

        public class TileClick{
          public string ClickedTile    {get; set;}
          public string GameID         {get; set;}
          public string SelectedTile   {get; set;}
          public List<string> CanMove  {get; set;}
          public List<string> CanTake  {get; set;}
        }

        // public IActionResult ClickedTile(string clickedTile, string gameId, string selectedTile, List<string> canMove, List<string> canTake)
        public IActionResult ClickedTile([FromBody] TileClick data)
        {
            var gameId = data.GameID;
            var clickedTile = data.ClickedTile;
            var selectedTile = data.SelectedTile;
            var canMove = data.CanMove;
            var canTake = data.CanTake;

            List<string> changedTiles = new List<string>();
            changedTiles.AddRange(canMove);
            changedTiles.AddRange(canTake);

            string userId = _signInManager.UserManager.GetUserId(User);
            RefreshUser(User);
            GameStateDTO gamestate = _GameRepo.GetGamestate(gameId);
            char userColor = _GameRepo.GetPlayerColor(gameId, userId);

            if (!_GameRepo.IsActivePlayer(gameId, userId)){
              // Not players turn
              return Json("");
            }

            else if(selectedTile == clickedTile){
              // Clicked already selected tile. Deselect it.
              changedTiles.Add(selectedTile);
              selectedTile = null;
              canMove = new List<string>();
              canTake = new List<string>();
            }

            else if(_chessLogicManager.PositionIsColor(gamestate.Board, clickedTile, userColor)){
              // Clicked own piece. Get possible moves.
              SelectedPieceDTO selectedPiece = new SelectedPieceDTO {
                  Board = gamestate.Board,
                  PlayerColor = userColor,
                  Selected = clickedTile
                };
              var moves = _chessLogicManager.GetPossibleMoves(selectedPiece);
              canMove = moves.PositionsPieceCanMoveTo;
              canTake = moves.PositionsPieceCanKillAt;
              changedTiles.AddRange(canMove);
              changedTiles.AddRange(canTake);
              if(selectedTile != null){
                changedTiles.Add(selectedTile);
              }
              changedTiles.Add(clickedTile);
              selectedTile = clickedTile;
            }

            else if(selectedTile != null){
              //a tile is already selected, and clicked an enemy or empty tile
              //Can the selected piece move there?
              MovePlanDTO movePlan = new MovePlanDTO
              {
                  Board = gamestate.Board,
                  From = selectedTile,
                  To = clickedTile,
                  PlayerColor = userColor
              };

              changedTiles.Add(clickedTile);
              changedTiles.Add(selectedTile);
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
                  _GameRepo.SetChangedTiles(gameId, changedTiles);
              }
              // Either a move was made, or the tile was deselected by clicking 
              // a tile where a move wasn't possible. Remove highlights.
              canMove = new List<string>();
              canTake = new List<string>();
              selectedTile = null;
            }

            gamestate = _GameRepo.GetGamestate(gameId);

            ChessboardPartialViewModel board = new ChessboardPartialViewModel
            {
                GameState = gamestate,
                SelectedTile = selectedTile,
                CanMoveToAndTakeTiles = canTake,
                CanMoveToTiles = canMove,
                GameId = gameId,
                PlayerColor = userColor,
                ChangedTiles = changedTiles
            };
                
            return Json(board);
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



        public class GameRefreshData{
          public string GameID  {get; set;}
          public int chatLength {get; set;}
        }

        public class GameViewData{
          public ChessboardPartialViewModel Game {get; set;}
        }

        public IActionResult GameRefresh([FromBody] GameRefreshData data){

            string userID = _signInManager.UserManager.GetUserId(User);

            bool IsMyTurn = _GameRepo.IsActivePlayer(data.GameID, userID);

            ChessboardPartialViewModel game = null;

            if(IsMyTurn){
              var changedTiles = _GameRepo.GetChangedTiles(data.GameID);
              char userColor = _GameRepo.GetPlayerColor(data.GameID, userID);
              var gamestate = _GameRepo.GetGamestate(data.GameID);
              
              
              game = new ChessboardPartialViewModel
              {
                  GameState = gamestate,
                  SelectedTile = null,
                  CanMoveToAndTakeTiles = new List<string>(),
                  CanMoveToTiles = new List<string>(),
                  GameId = data.GameID,
                  PlayerColor = userColor,
                  ChangedTiles = changedTiles.ToList()
              };

            }



          return Json(new GameViewData{Game=game});
          
        }
    }
}