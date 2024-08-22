
namespace PictureCollection.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } // Make sure this matches the property name in your model
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; } // This should match the UserId in User model
        public User User { get; set; } // Navigation property
    }
}
