using Microsoft.AspNetCore.Mvc;
using MyCollection.Data;
using MyCollection.Models;
using System;
using System.Threading.Tasks;

public class PostController : Controller
{
    private readonly ApplicationDbContext _context;

    public PostController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Post post)
    {
        if (ModelState.IsValid)
        {
            post.CreatedAt = DateTime.Now; // Set creation date
            // Optionally set the UserId if needed, e.g., from the logged-in user
            // post.UserId = User.Identity.Name; // Adjust this according to your authentication system

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index"); // Redirect to a page showing all posts or a confirmation page
        }
        return View(post); // Return the same view with validation errors
    }
}
