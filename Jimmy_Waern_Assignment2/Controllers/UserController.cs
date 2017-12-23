using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Jimmy_Waern_Assignment2.Models;
using System.IO;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Jimmy_Waern_Assignment2.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly UserContext _context;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<UserRole> roleManager, UserContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;

            if (_context.Users.Count() == 0)
            {
                _context.Users.Add(new User { UserName = "- Tom användarlista -" });
                _context.SaveChanges();
            }
        }

        private async Task<IActionResult> AddRoles()
        {
            string[] roles = { "Administrator", "Publisher", "Subscriber" };

            foreach (var role in roles)
            {
                var result = await _roleManager.RoleExistsAsync(role);

                if (!result)
                {
                    var newRole = new UserRole
                    {
                        Name = role
                    };
                    await _roleManager.CreateAsync(newRole);
                }
            }
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(_userManager.Users.OrderBy(c => c.Name).ToList());
        }

        [HttpGet, Route("seed")]
        public async Task<IActionResult> Seed()
        {
            await AddRoles();

            _context.RemoveRange(_userManager.Users);

            var file = Path.Combine(Environment.CurrentDirectory, "data", "Users.csv");
            using (var streamReader = System.IO.File.OpenText(file))
            {
                while (!streamReader.EndOfStream)
                {
                    var data = streamReader.ReadLine().Trim().Split(",");
                    var user = new User { Name = data[1], UserName = data[2], Role = data[3] };
                    if (!String.IsNullOrWhiteSpace(data[4]))
                    {
                        user.Age = int.Parse(data[4]);
                    }
                    var result = await _userManager.CreateAsync(user);

                    if (result.Succeeded)
                    {
                        if (user.Role != null)
                            await _userManager.AddToRoleAsync(user, user.Role);
                    }

                    if (user.Role == "Subscriber")
                        await _userManager.AddClaimAsync(user, new Claim("MinimumAge", user.Age.ToString()));
                }
            }
            return Ok("Database Seeded");
        }

        [HttpGet, Route("claims")]
        public IActionResult GetUsersWithClaims()
        {
            return Ok(_userManager.Users.Where(c => c.Role == "Subscriber").OrderBy(c => c.Name).ToList());
        }

        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user != null)
            {
                await _signInManager.SignInAsync(user, false);
                return Ok(String.Format("User: {0}", user.UserName));
            }
            return NotFound();
        }
    }
}
