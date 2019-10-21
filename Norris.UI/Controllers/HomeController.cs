using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Norris.UI.Models;
using Microsoft.AspNetCore.Identity;
using Norris.Data.Data.Entities;
using Norris.Data;
using Norris.Data.Models;
using Norris.Data.Models.DTO;
using Norris.UI.Models.ManageViewModels;
using Microsoft.EntityFrameworkCore;

namespace Norris.UI.Controllers
{
    public class HomeController : Controller
    {
        private SignInManager<User> _signInManager;
        private readonly IGameRepository _GameRepo;
        public HomeController(SignInManager<User> sim, IGameRepository GameRepo)
        {
            _signInManager = sim;
            _GameRepo = GameRepo;
        }

        public IActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");

            return Redirect("/Home/Lobby");
        }

        public IActionResult Lobby()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");

            ViewData["Message"] = "Lobby page.";

            var friends = _GameRepo.GetFriendList(2);
            var lobbyUsers = _GameRepo.GetPlayerLobby();
            var lobbyAndFriends = new LobbyAndFriendsViewModel{ CurrentLobbyUsers = lobbyUsers.Users, Friends = friends.Users };
            return View(lobbyAndFriends);
        }

        public IActionResult Game()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");
            else
                return RedirectToAction("Index", "Game", new { GameID = 13});
            
        }

        public IActionResult FindFriends()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");


            var friends = _GameRepo.GetFriendList(2);

            return View("FindFriends",friends);
        }

        [HttpGet]
        public PartialViewResult Search(string searchString)
        {
            UserListDTO users = new UserListDTO();
            users = _GameRepo.GetUserSearchResult(searchString);
           
            //List<User> foundUsers = new List<User>();
            //searchString = searchString.ToLower();
            //int j = 0;
            //foreach (var user in tempUsers)
            //{
            //    if (j == 50)
            //        break;

            //    if (user.UserName.ToLower().Contains(searchString))
            //    {
            //        foundUsers.Add(user);
            //        j++;
            //    }
            //}
            return PartialView("SearchResultsView", users.Users);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
