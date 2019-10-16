using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Norris.UI.Models;
using Microsoft.AspNetCore.Identity;
using Norris.Data.Data.Entities;
using Norris.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Norris.UI.Controllers
{
    public class HomeController : Controller
    {
        private SignInManager<User> _signInManager;
        private readonly NContext _nContext;
        public HomeController(SignInManager<User> sim, NContext nContext)
        {
            _signInManager = sim;
            _nContext = nContext;
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

            var friends = _nContext.Users.ToList();

            return View(friends);
        }


        public IActionResult Game()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");

            ViewData["Message"] = "Game view.";

            var friends = _nContext.Users.ToList();

            return View(friends);
        }

        public IActionResult FindFriends()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");


            var friends = _nContext.Users.ToList();

            return View("FindFriends",friends);
        }

        [HttpGet]
        public PartialViewResult Search(string searchString)
        {
            List<User> tempUsers = _nContext.Users.ToList();
            List<User> foundUsers = new List<User>();
            searchString = searchString.ToLower();
            int j = 0;
            foreach (var user in tempUsers)
            {
                if (j == 50)
                    break;

                if (user.UserName.ToLower().Contains(searchString))
                {
                    foundUsers.Add(user);
                    j++;
                }
            }
            return PartialView("SearchResultsView", foundUsers);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
