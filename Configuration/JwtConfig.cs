using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MateAPI.Configuration
{
    public class JwtConfig
    {
        public string Secret { get; set; }
        public int AccessTokenExpirationMinutes { get; set; }
    }
}
