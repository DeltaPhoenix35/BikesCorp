using BikesTest.Exceptions;
using BikesTest.Services;
using BikesTest.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Google.Cloud.Firestore;
using BikesTest.Service;

namespace BikesTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly LoginServices _lService;
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger, LoginServices lService,
                              Context db)
        {
            _logger = logger;
            _lService = lService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult login(string returnUrl = "/Home")
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> login(User user, string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
                throw new InvalidOperationException("login user invalid");

            try
            {
                await HttpContext.SignInAsync(_lService.Login(user));
                return Redirect(returnUrl);
            }
            catch(InvalidUsernameException e)
            {
                ViewData["LoginError"] = e.Message;
                return View();
            }
            catch (InvalidPasswordException e)
            {
                ViewData["LoginError"] = e.Message;
                return View();
            }
            catch (Exception e)
            {
                ViewData["LoginError"] = e.Message;
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            int currentUserId = Int32.Parse(User.Identities.First().FindFirst("Id").Value);

            var currentUser = _lService.Logout(currentUserId);
                
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
