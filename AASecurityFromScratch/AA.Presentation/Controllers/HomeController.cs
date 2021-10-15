using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AA.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
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

            var userPrincipal = new ClaimsPrincipal(new[] {testIdentity, licenceIdentity});

            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }
    }
}