using System;
namespace IAM.Models
{
    public class Function
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedTs { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime ModifiedTs { get; set; }
        public string? ModifiedBy { get; set; }
    }
}

