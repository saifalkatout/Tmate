using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MateAPI.Models
{
    public class user
    {
        public int ID { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public string Token { get; set; }
        public int PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Country { get; set; }
        public byte[] profilepicture { get; set; }
    }
}
