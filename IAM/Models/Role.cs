using System;
namespace IAM.Models
{
	public class Role
	{
		public Guid? Id { get; set; }
		public string? Name { get; set; }
		public DateTime? CreatedTs { get; set; }
		public string? CreatedBy { get; set; }
		public DateTime? ModifiedTs { get; set; }
		public string? ModifiedBy { get; set; }
	}
}

