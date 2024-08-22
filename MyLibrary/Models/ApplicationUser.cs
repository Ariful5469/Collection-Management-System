using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
namespace MyLibrary.Models
{
    public class ApplicationUser:IdentityUser
    {
        
        public string Firstname { get; set; }
        public string Lastname { get; set; }
       

    }
}
