using System;
namespace IAM.Entities
{
    public class UpsertRequestDTO
    {
        public string? EntityType { get; set; }
        public Guid? UserId { get; set; }
        public string? Content { get; set; }
    }
}

