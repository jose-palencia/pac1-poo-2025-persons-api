using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Security.Users;
using Persons.API.Services.Interfaces;

namespace Persons.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(
            IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<UserActionResponseDto>>> 
            Create([FromBody] UserCreateDto dto) 
        {
            var response = await _usersService.CreateAsync(dto);

            return StatusCode(response.StatusCode, 
                new ResponseDto<UserActionResponseDto> 
            {
                StatusCode = response.StatusCode,
                Status = response.Status,
                Message = response.Message,
                Data = response.Data
            });
        }

    }
}
