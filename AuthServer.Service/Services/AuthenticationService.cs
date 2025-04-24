using AuthServer.Core.Configurations;
using AuthServer.Core.DTOs.Login;
using AuthServer.Core.DTOs.Token;
using AuthServer.Core.Entities;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitofWork;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly List<Client> _clients;
        private readonly ITokenService _tokenService;
        private readonly UserManager<UserApp> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<UserRefreshToken> _userRefreshTokenService;

        public AuthenticationService(IOptions<List<Client>> optionsClient, ITokenService tokenService, 
            UserManager<UserApp> userManager, IUnitOfWork unitOfWork,
            IGenericRepository<UserRefreshToken> userRefreshTokenService)
        {
            _clients = optionsClient.Value;
            _tokenService = tokenService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _userRefreshTokenService = userRefreshTokenService;
        }

        public async Task<Response<TokenDto>> CreateAccessToken(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));
            var user = await  _userManager.FindByEmailAsync(loginDto.Email);

            if (user==null)
            {
                return Response<TokenDto>.Fail("email or password is wrong", 400, true);
            }
             if(!await _userManager.CheckPasswordAsync(user, loginDto.password))
            {
                return Response<TokenDto>.Fail("email or password is wrong", 400, true);
            }


            var token = _tokenService.CreateToken(user);
            var userRefreshToken = await _userRefreshTokenService.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();
            if (userRefreshToken == null)
            {
                // databasede bele bir refrsh token yoxdusa kayd etmek
                await _userRefreshTokenService.AddAsync(new UserRefreshToken
                {
                    Expiration = token.RefreshTokenExpiration,
                    Code = token.RefreshToken,
                    UserId = user.Id
                });
            }
            else
            {
                userRefreshToken.Code = token.RefreshToken;
                userRefreshToken.Expiration = token.RefreshTokenExpiration;
            }
            await _unitOfWork.CommitAsync();
            return Response<TokenDto>.Success(token,200);
        }

        public Response<ClientTokenDto> CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);
            if (client == null)
            {
                return Response<ClientTokenDto>.Fail("clientid or clientsecret not found", 404, true);
            }
            var token = _tokenService.CreateTokenByClient(client);
            return Response<ClientTokenDto>.Success(token, 200);
        }

        public async Task<Response<TokenDto>> CreateTokenByRefreshToken(string RefreshToken)
        {
            var existRefreshToken = await _userRefreshTokenService.Where(x => x.Code == RefreshToken).SingleOrDefaultAsync();
            if (existRefreshToken ==null)
            {
                return Response<TokenDto>.Fail("user id not found", 404, true);
            }
            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            var tokenDto = _tokenService.CreateToken(user);
            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;
            await _unitOfWork.CommitAsync();
            return Response<TokenDto>.Success(tokenDto, 200);

        }

        public async Task <Response<NoDataDto>> RevokeRefreshToken(string RefreshToken)
        {
            var existRefreshtoken = await _userRefreshTokenService.Where(x => x.Code == RefreshToken).SingleOrDefaultAsync();
            if (existRefreshtoken == null)
            {
                return Response<NoDataDto>.Fail("refreshtoken not found", 404, true);
            }
            _userRefreshTokenService.Remove(existRefreshtoken);
            await _unitOfWork.CommitAsync();
            return Response<NoDataDto>.Success(200);
        }
    }
}
