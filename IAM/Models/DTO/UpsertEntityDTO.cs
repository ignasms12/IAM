using System;
namespace IAM.Models.DTO
{
    public class UpsertEntityDTO
    {
        public string? EntityType { get; set; }
        public Guid? UserId { get; set; }
        public string? Content { get; set; }
    }
}

