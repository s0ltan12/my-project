using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class UserController : Controller
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        // Register (GET)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Register (POST)
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(user);
        }

        // Login (GET)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login (POST)
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                return RedirectToAction("Welcome", new { name = user.FirstName });
            }

            ViewBag.Error = "Invalid email or password";
            return View();
        }

        public IActionResult Welcome(string name)
        {
            ViewBag.Name = name;
            return View();
        }
    }
}
