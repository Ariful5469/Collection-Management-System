using Microsoft.AspNetCore.Mvc;

using PictureCollection.Data;
using PictureCollection.Models;
using System.Linq;
using System.Threading.Tasks;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult ManageUsers()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(ManageUsers));
    }

    [HttpPost]
    public async Task<IActionResult> BlockUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            user.IsBlocked = true;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(ManageUsers));
    }

    [HttpPost]
    public async Task<IActionResult> UnblockUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null)
        {
            user.IsBlocked = false;
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(ManageUsers));
    }

    [HttpPost]
    public async Task<IActionResult> PromoteToAdmin(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null && user.Role != "Admin")
        {
            user.Role = "Admin";
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(ManageUsers));
    }

    [HttpPost]
    public async Task<IActionResult> DemoteFromAdmin(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user != null && user.Role == "Admin")
        {
            user.Role = "User";
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(ManageUsers));
    }
}
