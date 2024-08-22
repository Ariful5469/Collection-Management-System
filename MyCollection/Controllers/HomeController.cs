using Microsoft.AspNetCore.Mvc;
using MyCollection.Data;
using MyCollection.Models; // Ensure this is included for the Post model
using System.Linq;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var posts = _context.Posts.OrderByDescending(p => p.CreatedAt).ToList();
        return View(posts);
    }


    public IActionResult Search(string query)
    {
        var results = _context.Posts.Where(p => p.Title.Contains(query) || p.Content.Contains(query)).ToList();
        return View("Index", results);
    }
}
