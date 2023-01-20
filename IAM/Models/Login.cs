using System;
namespace IAM.Models
{
	public class Login
	{
        public Guid? Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}

