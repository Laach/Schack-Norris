using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Norris.UI.Models;
using Microsoft.AspNetCore.Identity;
using Norris.Data.Data.Entities;

namespace Norris.UI.Controllers
{
    public class HomeController : Controller
    {
        private SignInManager<User> _signInManager;
        public HomeController(SignInManager<User> sim)
        {
            _signInManager = sim;
        }

        public IActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account");

            return View();
        }

        public IActionResult Lobby()
        {
            ViewData["Message"] = "Lobby page.";

            return View();
        }

        public IActionResult Game()
        {
            ViewData["Message"] = "Game view.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
