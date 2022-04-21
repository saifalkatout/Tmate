using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MateAPI.Models
{
    public class Tmate
    {
        public int ID { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string Language { get; set; }

        public string bio { get;set; }

        public Boolean pay { get; set; }
        public long followers { get; set; }
        public string categoryIDs { get; set; }
        public string salt {get;set;}
        public byte[] ProfilePic { get; set; }
    }
}
