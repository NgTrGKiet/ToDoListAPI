using System.ComponentModel.DataAnnotations;

namespace BLL.DTO
{
    public class TaskCreateDTO
    {
        [Required]
        public string Title { get; set; }
        public string? Content { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
