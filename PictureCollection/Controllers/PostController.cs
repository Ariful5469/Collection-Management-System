using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PictureCollection.Data;
using PictureCollection.Models;
using System;
using System.IO;
using System.Linq;
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
    public async Task<IActionResult> Create(Post post, IFormFile image)
    {
        if (ModelState.IsValid)
        {
            if (image != null && image.Length > 0)
            {
                var fileName = Path.GetFileName(image.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                post.ImageUrl = $"/images/{fileName}";
            }

            post.CreatedAt = DateTime.Now;
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        return View(post);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null) return NotFound();
        return View(post);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Post post, IFormFile image)
    {
        if (ModelState.IsValid)
        {
            var existingPost = await _context.Posts.FindAsync(post.Id);
            if (existingPost != null)
            {
                existingPost.Title = post.Title;
                existingPost.Description = post.Description;

                if (image != null && image.Length > 0)
                {
                    var fileName = Path.GetFileName(image.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    existingPost.ImageUrl = $"/images/{fileName}";
                }

                _context.Posts.Update(existingPost);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
        }

        return View(post);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null) return NotFound();
        return View(post);
    }
}
