using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.DTO
{
    public class TaskDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Content { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
