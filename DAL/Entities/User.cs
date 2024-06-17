using System.ComponentModel.DataAnnotations;

namespace DAL.Entites
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
