using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace MyLibrary.Models
{
    public class BookViewModel
    {
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
        public IFormFile Photo { get; set; }
    }
}
