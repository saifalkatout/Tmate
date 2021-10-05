using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MateAPI.Models
{
    public class AuthUser
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}