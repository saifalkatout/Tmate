using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MateAPI.Configuration
{
    public class AuthResult
    {
        public AuthResult(string T, string R, bool S)
        {
            this.Token = T;
            this.RefreshToken = R;
            this.Success = S;
        }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool Success { get; set; }
    }
}
