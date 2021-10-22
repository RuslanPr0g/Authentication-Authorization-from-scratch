using AA.Presentation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AA.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthorizationService _authorizationService;

        public HomeController(ILogger<HomeController> logger, IAuthorizationService authorizationService)
        {
            _logger = logger;
            _authorizationService = authorizationService;
        }

        public async Task<IActionResult> DoStuff()
        {
            // doing smth

            var builder = new AuthorizationPolicyBuilder("Schema");
            var customPolicy = builder.RequireClaim("Hello").Build();

            var authresult = await _authorizationService.AuthorizeAsync(User, "Claim.DoB");

            if (authresult.Succeeded)
            {
                // do smth more
            }

            return Index();
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var testClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "test1"),
                new Claim(ClaimTypes.Email, "testemail@gmail.com"),
                new Claim("Test.Says", "Very cool test boi here")
            };

            var licenceClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Test 1 Value"),
                new Claim("DrivingLicence", "B+")
            };

            var testIdentity = new ClaimsIdentity(testClaims, "Test Identity");
            var licenceIdentity = new ClaimsIdentity(licenceClaims, "Licence Identity");

            var userPrincipal = new ClaimsPrincipal(new[] { testIdentity, licenceIdentity });

            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
