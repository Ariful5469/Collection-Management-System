using System.ComponentModel.DataAnnotations;

namespace crud_first.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Qty { get; set; }
    }
}
