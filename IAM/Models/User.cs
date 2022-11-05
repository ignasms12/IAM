using System;
namespace IAM.Models
{
	public class User
	{
		public Guid Id { get; set; }
		public string? Username { get; set; }
		public string? Email { get; set; }
		public DateTime Createdts { get; set; }
		public string? CreatedBy { get; set; }
		public DateTime ModifiedTs { get; set; }
		public string? ModifiedBy { get; set; }
    }
}

