using Microsoft.AspNetCore.Identity;

namespace PictureCollection.Models
{
    public class User : IdentityUser
    {
        public string Role { get; set; }
        public bool IsBlocked { get; set; }
    }
}
