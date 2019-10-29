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
            // _GameRepo.AddFriend("643cc4da-31aa-41c7-addd-30f614429469", "5ef685b4-dd4f-45c8-9885-a93e92754efc");
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
            RefreshUser(User);

            ViewData["Message"] = "Lobby page.";

            var uid = _signInManager.UserManager.GetUserId(User);
            var friends = _GameRepo.GetFriendList(uid);
            var lobbyUsers = _GameRepo.GetPlayerLobby();
            var games = _GameRepo.GetUserGameList(uid);

            FriendsPartialViewModel friendsAndGames = new FriendsPartialViewModel
            {
                UserFriends = friends,
                UserGames = games
            };

            var lobbyAndFriends = new LobbyAndFriendsViewModel{ CurrentLobbyUsers = lobbyUsers.Users, Friends = friendsAndGames };
            return View(lobbyAndFriends);
        }

        public IActionResult Game()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");
            else
            {
                var gid = _GameRepo.GetUserGameList(_signInManager.UserManager.GetUserId(User)).FirstOrDefault().GameID;
                return RedirectToAction("Index", "Game", new { gameId = gid });            

            }
        }

        public IActionResult FindFriends()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");
            RefreshUser(User);


         

            return View("FindFriends");
        }

        public class userToAdd
        {
            public string userID { get; set; }
        }

        public void AddFriend([FromBody] userToAdd add)
        {
            _GameRepo.AddFriend(_signInManager.UserManager.GetUserId(User), add.userID);
        }

        [HttpGet]
        public PartialViewResult Search(string searchString)
        {
            // var users = new UserListDTO();
            var userID = _signInManager.UserManager.GetUserId(User);
            // users.Users = _signInManager.UserManager.Users.ToList();
            var dto = _GameRepo.GetUserSearchResult(userID, searchString);

            return PartialView("SearchResultsView", dto.Users.Take(50).ToList());
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void RefreshUser(System.Security.Claims.ClaimsPrincipal user){
            var uid = _signInManager.UserManager.GetUserId(User);
            UserActivity.RefreshUser(uid);
        }


        public IActionResult ArchivedGames(){
          if (!_signInManager.IsSignedIn(User))
              return RedirectToAction("Login", "Account");
          RefreshUser(User);

          var uid = _signInManager.UserManager.GetUserId(User);

          var games = _GameRepo.GetArchivedGameList(uid);

          return View(games);
        }
    }
}
