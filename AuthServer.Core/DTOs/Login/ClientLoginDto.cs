﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.DTOs.Login
{
    

    public class ClientLoginDto
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; } // password gibi dusuncez
    }
}
