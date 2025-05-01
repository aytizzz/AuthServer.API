using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Entities
{
    public class UserRefreshToken
    {
        public string UserId { get; set; }// userid elaqelenmeli?
        public string Code { get; set; }// Refreshtoken kodu 
        public DateTime Expiration { get; set; }
    }
}
