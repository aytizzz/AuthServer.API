using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.DTOs.Token
{
    public class ClientTokenDto
    {
        public string AccessToken { get; set; } // // Frontend API-lərə göndərəcək
        public DateTime AccessTokenExpiration { get; set; } // Token nə vaxt bitəcək
    }
}
