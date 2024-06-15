using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.DTO
{
    public class UpdateTaskDTO
    {
        [Required]
        public int Id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
