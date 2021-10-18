using IdentityExample.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExample.Controllers
{
    [ApiController]
    [Route("weatherforecast")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            var result = new Microsoft.AspNetCore.Identity.SignInResult();

            if (user is not null)
            {
                result = await _signInManager.PasswordSignInAsync(user, password, false, false);
            }

            if (result.Succeeded)
            {
                return Ok(new
                {
                    UserCreated = user,
                });
            }
            else
                return NotFound($"Either username or password are wrong.");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = $"{username.Substring(0, 4)}{new Random().Next(username.Length)}@gmail.com",
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    Result = result,
                    UserCreated = user,
                });
            }
            else
                return BadRequest(new
                {
                    Result = result,
                    UserCreated = user,
                });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("You were signed out.");
        }
    }
}
