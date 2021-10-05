using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MateAPI.Models
{
    public class Shop
    {
        public int ID { get; set; }
        public string Username { get; set; }

        public string Password { get; set; }
        public string Token { get; set; }
        public int PhoneNumber { get; set; }
        public string Shopname { get; set; }

        public string Email { get; set; }
        public double lat{ get; set; }
        public double longi { get; set; }

        public string bio { get; set; }

        public int rating { get; set; }
        public string categoryIDs { get; set; }
        public byte[] TradeMark { get; set; }
        public byte[] logo { get; set; }
   
    }
   
}
