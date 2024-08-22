using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyCollection.Data;
using MyCollection.Models;
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult ManageUsers()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    public IActionResult BlockUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user != null)
        {
            user.IsBlocked = !user.IsBlocked;
            _context.SaveChanges();
        }
        return RedirectToAction("ManageUsers");
    }

    public IActionResult ManagePosts()
    {
        var posts = _context.Posts.ToList();
        return View(posts);
    }

    public IActionResult DeletePost(int id)
    {
        var post = _context.Posts.Find(id);
        if (post != null)
        {
            _context.Posts.Remove(post);
            _context.SaveChanges();
        }
        return RedirectToAction("ManagePosts");
    }
}
