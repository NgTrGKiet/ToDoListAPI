using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entites
{
    public class Token
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Users")]
        public string user_id { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public Boolean isValid { get; set; }
    }
}
