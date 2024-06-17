using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entites
{
    public class UserTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        [ForeignKey("Users")]
        public string User_id { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
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
