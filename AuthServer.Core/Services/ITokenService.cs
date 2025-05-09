﻿using AuthServer.Core.Configurations;
using AuthServer.Core.DTOs.Token;
using AuthServer.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp userApp); // user -e ozgun token uretme
        ClientTokenDto CreateTokenByClient(Client client);  /* videoda deyirki clienttokendto donen method */


    }
}
