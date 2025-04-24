using AuthServer.Core.DTOs.Login;
using AuthServer.Core.DTOs.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using SharedLibrary.Dtos;

using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Services
{
    public interface IAuthenticationService
    {
        Task<Response<TokenDto>> CreateAccessToken(LoginDto loginDto);
        Task<Response<TokenDto>> CreateTokenByRefreshToken(string RefreshToken);

        // user logout edende refreshtokenini null etmek
        Task<Response<NoDataDto>> RevokeRefreshToken(string RefreshToken);
        Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto);
    }
}
