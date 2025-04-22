using AuthServer.Core.Configurations;
using AuthServer.Core.DTOs.Token;
using AuthServer.Core.Entities;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOption _tokenOption;
       public TokenService(UserManager<UserApp> userManager,  IOptions< CustomTokenOption> options)
        {
            _userManager = userManager;
            _tokenOption = options.Value;
        }

        public TokenDto CreateToken(UserApp userApp)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOption.RefreshTokenExpiration);

            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(

                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore:DateTime.Now,
                claims:GetClaims(userApp,_tokenOption.Audience),
                signingCredentials:signingCredentials

                );
            var handler = new JwtSecurityTokenHandler();
            var accesstoken = handler.WriteToken(jwtSecurityToken); // token yaradir


            //tokeni dto donsuturme
            var tokenDto = new TokenDto
            {
                AccessToken = accesstoken,
                RefreshToken=CreateRefreshToken(),
                AccessTokenExpiration=accessTokenExpiration,
                RefreshTokenExpiration=refreshTokenExpiration


            };
            return tokenDto;
        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.UtcNow.AddMinutes(_tokenOption.RefreshTokenExpiration);

            var securityKey = SignService.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(

                issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: GetClaimsByClient(client),
                signingCredentials: signingCredentials

                );
            var handler = new JwtSecurityTokenHandler();
            var accesstoken = handler.WriteToken(jwtSecurityToken); // token yaradir


            //tokeni dto donsuturme
            var clientTokenDto = new ClientTokenDto
            {
                AccessToken = accesstoken,
                AccessTokenExpiration = accessTokenExpiration,
               


            };
            return clientTokenDto;

        }

        private string CreateRefreshToken()
        {
            var numberByte = new Byte[32];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }


        private IEnumerable<Claim>GetClaims(UserApp userApp, List<string> audiences)
        {
            var userlist = new List<Claim> ///payloadinda olmasini istedyimiz datalar
           {
               new Claim (ClaimTypes.NameIdentifier,userApp.Id),
               new Claim (JwtRegisteredClaimNames.Email,userApp.Email),
                new Claim (ClaimTypes.Name,userApp.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

           };
            userlist.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return userlist;
        }

        // uyelik sistemi olmayan apiler ucun
        private IEnumerable<Claim>GetClaimsByClient (Client client)
        {
            var claims = new List<Claim>();
           claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());
            new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString());
            return claims;
        
    }
    }

}
