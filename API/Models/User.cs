using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    [Table("users")]
    public class User
    {
        //[Key]
        public int userId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
