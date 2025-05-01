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
        // apiye gondereceyimiz ucun geriye dto donuruk
        Task<Response<TokenDto>> CreateAccessToken(LoginDto loginDto);  // eger login datalar dogrudusa geriye token donuruk
        Task<Response<TokenDto>> CreateTokenByRefreshToken(string RefreshToken);  // refreshtokenle yeni token yaratma

        Task<Response<NoDataDto>> RevokeRefreshToken(string RefreshToken);   // user logout edende refreshtokenini null etmek

        Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto); // clientlere token olusturma=> gelen clientlogindto icerisindeki datalar
                                                                                        // heqiqeten bizim appsettingjson da varsa geriye clientTokendto donme
    }
}
