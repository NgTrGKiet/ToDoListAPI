using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entites
{
    public class UserTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string title { get; set; }
        public string content { get; set; }
        [ForeignKey("Users")]
        public string user_id { get; set; }
        public string status { get; set; }
        public string priority { get; set; }
        private DateTime _start;
        public DateTime Start
        {
            get => _start;
            set => _start = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        private DateTime _end;
        public DateTime End
        {
            get => _end;
            set => _end = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }
}
