using AuthServer.Core.DTOs.Login;
using AuthServer.Core.DTOs.RefreshToken;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]/[action]")] // api/auth/creattoken
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
            var result = await _authenticationService.CreateAccessToken(loginDto);
            return ActionResultInstance(result); /* if (result.statuscode==200) {return ok(result) else if(result.statuscode==404 return notfound )}*/
        }

        [HttpPost]
        public IActionResult  CreateTokenByClient(ClientLoginDto clientLoginDto)
        {
            var result =  _authenticationService.CreateTokenByClient(clientLoginDto);
            return ActionResultInstance(result);
        }
        [HttpPost]
        public async Task< IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result =  await _authenticationService.RevokeRefreshToken(refreshTokenDto.RefreshToken);
            return ActionResultInstance(result);

        }
        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.CreateTokenByRefreshToken(refreshTokenDto.RefreshToken);
            return ActionResultInstance(result);
        }

    }
}
