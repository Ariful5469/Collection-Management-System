using Microsoft.AspNetCore.Mvc;
using MyCollection.Data;
using MyCollection.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyCollection.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Password != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View(model);
                }

                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password, // Ensure proper hashing in real scenarios
                    Role = "User",
                    IsBlocked = false
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    // Assuming user is authenticated successfully
                    // Set authentication cookie or session here (if using cookie-based auth)

                    // For simplicity, we'll use TempData to pass the success message
                    TempData["SuccessMessage"] = "Login successful!";

                    // Redirect to Create Post page if user is authenticated
                    if (user.Role == "User") // Check for User role, adjust if needed
                    {
                        return RedirectToAction("Create", "Post");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Admin"); // Redirect admin to admin page
                    }
                }
                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if username or email already exists
                if (_context.Users.Any(u => u.Username == model.Username || u.Email == model.Email))
                {
                    ModelState.AddModelError("", "Username or Email already exists.");
                    return View(model);
                }

                var user = new User
                {
                    Username = model.Username,
                    Email = model.Email,
                    Password = model.Password,
                    Role = "User", // Default role for registration
                    IsBlocked = false
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Registration successful! You can now log in.";
                return RedirectToAction("Login");
            }

            return View(model);
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            // Implement logout logic here
            TempData["SuccessMessage"] = "You have been logged out.";
            return RedirectToAction("Login");
        }
    }
}
