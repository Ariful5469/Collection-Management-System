using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PictureCollection.Data;
using PictureCollection.Models;
using System.Linq;
using System.Threading.Tasks;

public class ProfileController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public ProfileController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User); // Get the current user
        if (user == null)
        {
            return RedirectToAction("Login", "Account"); // Redirect if user is not logged in
        }

        var posts = _context.Posts.Where(p => p.UserId == user.Id).ToList();
        return View(posts);
    }
}
