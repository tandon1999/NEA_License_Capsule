using API.RequestModel.Auth;
using API.Services.Interface.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController(ITokenService _tokenService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("GetToken")]
        public async Task<IActionResult> CreateToken(LoginRequestModel loginRequest)
        {
            return Ok(await _tokenService.CreateToken(loginRequest));
        }
    }
}
