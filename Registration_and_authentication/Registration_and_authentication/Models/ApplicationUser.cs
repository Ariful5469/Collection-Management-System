using Microsoft.AspNetCore.Identity;
using System;

namespace Registration_and_authentication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? LastLoginTime { get; set; }
        public DateTime RegistrationTime { get; set; }
        public string Status { get; set; } // Active, Blocked, etc.
    }
}
