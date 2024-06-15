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
        public string title { get; set; }
        public string content { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
    }
}
