using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Registration_and_authentication.Models;
using System.Threading.Tasks;
using System.Linq;

namespace Registration_and_authentication.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Users()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Block(string[] ids)
        {
            foreach (var id in ids)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.Status = "Blocked";
                    await _userManager.UpdateAsync(user);
                }
            }
            return RedirectToAction("Users");
        }

        [HttpPost]
        public async Task<IActionResult> Unblock(string[] ids)
        {
            foreach (var id in ids)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    user.Status = "Active";
                    await _userManager.UpdateAsync(user);
                }
            }
            return RedirectToAction("Users");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string[] ids)
        {
            foreach (var id in ids)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user != null)
                {
                    await _userManager.DeleteAsync(user);
                }
            }
            return RedirectToAction("Users");
        }
    }
}
