using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MateAPI.Models
{
    public class TokenRequest
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string Token { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string RefreshToken { get; set; }
    }
}
