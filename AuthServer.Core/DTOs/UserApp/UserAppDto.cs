using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.DTOs.UserApp
{
    public class UserAppDto 
    {
        public int Id { get; set; } // string olmaliydi?    
        public string UserName { get; set; }
        public string Email { get; set;}
        public string City { get; set; }
    }
}
