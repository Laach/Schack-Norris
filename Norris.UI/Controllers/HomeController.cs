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

        public IActionResult JoinGame(string opponentId)
        {
            //Add the game
            var user = _signInManager.UserManager.Users.Where(u => u.Id == opponentId).FirstOrDefault();
            if(!user.IsInLobby){
              return Redirect("/Home/Index");
            }
            string newGameId = _GameRepo.AddNewGame(_signInManager.UserManager.GetUserId(User), opponentId);

            //Remove the user looking for a game from the lobby
            _GameRepo.LeaveLobby(opponentId);

            //Send the joining user to the game
            return RedirectToAction("Index", "Game", new { gameId = newGameId });
        }

        public IActionResult Lobby()
        {

            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");
            RefreshUser(User);

            var uid = _signInManager.UserManager.GetUserId(User);

            var friends = _GameRepo.GetFriendList(uid);
            var lobbyUsers = _GameRepo.GetPlayerLobby(uid);
            var games = _GameRepo.GetUserGameList(uid);

            FriendsPartialViewModel friendsAndGames = new FriendsPartialViewModel
            {
                UserFriends = friends,
                UserGames = games,
                ActiveGame = ""
            };

            var lobbyAndFriends = new LobbyAndFriendsViewModel{
                CurrentLobbyUsers = lobbyUsers.Users,
                Friends = friendsAndGames,
                IsInLobby = _GameRepo.IsInLobby(uid) ? true : false
            };

            return View(lobbyAndFriends);
        }

        public IActionResult GetLobbyPartial(){
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");
            RefreshUser(User);

            var uid = _signInManager.UserManager.GetUserId(User);

            var lobbyUsers = _GameRepo.GetPlayerLobby(uid);

            var lobby = new LobbyAndFriendsViewModel{
                CurrentLobbyUsers = lobbyUsers.Users,
                Friends = null,
                IsInLobby = _GameRepo.IsInLobby(uid) ? true : false
            };
          return PartialView("_LobbyPartial", lobby);
        }

        public IActionResult Sidebar([FromBody] GameController.GameRefreshData data){
            var uid = _signInManager.UserManager.GetUserId(User);
            var friends = _GameRepo.GetFriendList(uid);
            var games = _GameRepo.GetUserGameList(uid);
            FriendsPartialViewModel friendsAndGames = new FriendsPartialViewModel
            {
                UserFriends = friends,
                UserGames = games,
                ActiveGame = data.GameID
            };
            var sidebar = new SidebarModel();
            var t1 = this.RenderViewAsync<FriendsPartialViewModel>( "_ActiveGamesPartial", friendsAndGames, true);
            var t2 = this.RenderViewAsync<IEnumerable<User>>("_OnlineFriendsPartial", friends.OnlineFriends, true);
            var t3 = this.RenderViewAsync<IEnumerable<User>>("_OfflineFriendsPartial", friends.OfflineFriends, true);
            
            Task.WaitAll(new Task<string>[]{t1, t2, t3});
            sidebar.ActiveGames    = t1.Result;
            sidebar.OnlineFriends  = t2.Result;
            sidebar.OfflineFriends = t3.Result;
            return Json(sidebar);
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

            var uid = _signInManager.UserManager.GetUserId(User);
            var friends = _GameRepo.GetFriendList(uid);
            var games = _GameRepo.GetUserGameList(uid);

            FriendsPartialViewModel friendsAndGames = new FriendsPartialViewModel
            {
                UserFriends = friends,
                UserGames = games,
                ActiveGame = ""
            };
            return View(friendsAndGames);
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

        public IActionResult JoinLobby()
        {
            var uid = _signInManager.UserManager.GetUserId(User);
            _GameRepo.EnterLobby(uid);
            return RedirectToAction("Lobby");
        }

        public IActionResult LeaveLobby()
        {
            var uid = _signInManager.UserManager.GetUserId(User);
            _GameRepo.LeaveLobby(uid);
            return RedirectToAction("Lobby");
        }
    }
}
