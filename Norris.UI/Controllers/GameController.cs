using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Norris.Data;

namespace Norris.UI.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameRepository _GameRepo;
        public GameController( IGameRepository GameRepo)
        {
            _GameRepo = GameRepo;
        }

        public IActionResult Index()
        {
            

            ViewData["Message"] = "Game view.";

            var friends = _GameRepo.GetFriendList(2);

            return View(friends);
        }
    }
}