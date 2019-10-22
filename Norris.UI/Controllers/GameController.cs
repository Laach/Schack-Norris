using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Norris.Data;
using Norris.Data.Data.Entities;
using Norris.Data.Models.DTO;
using Norris.UI.Models;

namespace Norris.UI.Controllers
{
    public class GameController : Controller
    {
        private readonly UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private readonly IGameRepository _GameRepo;
        private string _selectedTile = null;
        private GameStateDTO _gameState = null;
        private string _gameId = null;

        public GameController(SignInManager<User> sim, IGameRepository GameRepo, UserManager<User> userManager)
        {
            _signInManager = sim;
            _GameRepo = GameRepo;
            _userManager = userManager;
        }
        public IActionResult Index(string gameId)
        {
            ViewData["Message"] = "Game view.";
            _gameId = gameId;
            var friends = _GameRepo.GetFriendList("2");
            var gamestate = _GameRepo.GetGamestate(gameId);
            _gameState = gamestate;
            var board = new BoardViewModel { GameState = gamestate, SelectedTile = _selectedTile, CanMoveToAndTakeTiles = null, CanMoveToTiles = null};
            return View(new GameViewModel { UserList = friends, Board = board });
        }

        public IActionResult ClickedTile(string rankFile)
        {
            char rank = rankFile[0];
            char file = rankFile[1];

            if(_selectedTile == null)
            {
                _selectedTile = rankFile;
            } else if (_selectedTile == rankFile)
            {
                _selectedTile = null;
            } else
            {
                //has a selected tile and has clicked any other tile

            }
            var gamestate = _GameRepo.GetGamestate(_gameId);
            var friends = _GameRepo.GetFriendList("2");
            _gameState = gamestate;
            var board = new BoardViewModel { GameState = gamestate, SelectedTile = _selectedTile, CanMoveToAndTakeTiles = null, CanMoveToTiles = null };
            return View("Index", new GameViewModel { UserList = friends, Board = board } );

        }
    }
}