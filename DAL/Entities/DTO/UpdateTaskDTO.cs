using System.ComponentModel.DataAnnotations;

namespace DAL.Entities.DTO
{
    public class UpdateTaskDTO
    {
        [Required]
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
