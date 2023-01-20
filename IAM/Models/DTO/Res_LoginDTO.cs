using System;
namespace IAM.Models.DTO
{
	public class Res_LoginDTO
	{
        public Guid? Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? StatusMessage { get; set; }
    }
}

