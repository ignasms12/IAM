using System;
namespace IAM.Models.DTO
{
	public class UpsertEntityRelationDTO
	{
		public string? DestEntity { get; set; }
		public string? RelationType { get; set; }
		public Guid? UserId { get; set; }
		public string? Content { get; set; }
	}
}

