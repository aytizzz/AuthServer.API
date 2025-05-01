using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.DTOs.Token
{
    public class TokenDto
    {
        public string AccessToken { get; set; }  // string ifadedi 
        public DateTime AccessTokenExpiration { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

    }
}

// cliente doneceyimiz token modeli