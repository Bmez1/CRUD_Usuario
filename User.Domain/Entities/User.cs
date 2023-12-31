﻿using User.Domain.Enums;

namespace User.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public Role Role { get; set; }
        public string PassWordHash { get; set; }
    }
}
