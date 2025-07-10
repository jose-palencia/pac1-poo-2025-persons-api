using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Security.Auth;
using Persons.API.Services.Interfaces;

namespace Persons.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(
            IAuthService authService)
        {
            this._authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseDto<LoginResponseDto>>>
            Login(LoginDto dto)
        { 
            var response = await _authService.LoginAsync(dto);

            return StatusCode(response.StatusCode, new ResponseDto<LoginResponseDto> 
            {
                Status = response.Status,
                Message = response.Message,
                Data = response.Data
            });
        }

    }
}
