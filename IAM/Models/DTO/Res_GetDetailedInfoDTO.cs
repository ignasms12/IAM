using System;
namespace IAM.Models.DTO
{
	public class Res_GetDetailedInfoDTO
	{
		public IEnumerable<User>? users { get; set; }
		public IEnumerable<Role>? roles { get; set; }
		public IEnumerable<Function>? functions { get; set; }
	}
}

