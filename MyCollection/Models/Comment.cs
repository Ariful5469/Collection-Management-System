using System;

namespace MyCollection.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } // This should work if User class is defined
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
