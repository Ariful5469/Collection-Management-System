using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using  Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MyLibrary.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Author { get; set; }

        public DateTime PublishedDate { get; set; }

        [StringLength(13)]
        public string ISBN { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public string ImagePath { get; set; } // Path to the image file

        [NotMapped]
        public IFormFile Photo { get; set; }

        public string Tags { get; set; }
    }
}
