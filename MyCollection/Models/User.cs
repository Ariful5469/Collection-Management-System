﻿namespace MyCollection.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Ensure this property is here if needed
        public string Role { get; set; }
        public bool IsBlocked { get; set; }
    }
}
