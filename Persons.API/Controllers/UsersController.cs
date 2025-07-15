using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Security.Users;
using Persons.API.Services.Interfaces;

namespace Persons.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(
            IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [Authorize(Roles = "NORMAL_USER, SYS_ADMIN_EDITADO")]
        public async Task<ActionResult<ResponseDto<PaginationDto<List<UserDto>>>>> 
            GetPaginationList(string searchTerm = "", int page = 1, int pageSize = 0) 
        {
            var response = await _usersService
                .GetListAsync(searchTerm, page, pageSize);

            return StatusCode(response.StatusCode, new ResponseDto<PaginationDto
                <List<UserDto>>> 
            {
                Status = response.Status,
                Message = response.Message,
                Data = response.Data
            });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SYS_ADMIN_EDITADO")]
        public async Task<ActionResult<ResponseDto<UserDto>>> GetOneById(
            string id) 
        {
            var response = await _usersService.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, new ResponseDto<UserDto>
            {
                Status = response.Status,
                Message= response.Message,
                Data = response.Data
            });
        }

        [HttpPost]
        [Authorize(Roles = "SYS_ADMIN_EDITADO")]
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

        [HttpPut("{id}")]
        [Authorize(Roles = "SYS_ADMIN_EDITADO")]
        public async Task<ActionResult<ResponseDto<UserActionResponseDto>>> Edit(
            [FromBody] UserEditDto dto, string id) 
        {
            var response = await _usersService.EditAsync(dto, id);

            return StatusCode(response.StatusCode,
                new ResponseDto<UserActionResponseDto>
                {
                    StatusCode = response.StatusCode,
                    Status = response.Status,
                    Message = response.Message,
                    Data = response.Data
                });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SYS_ADMIN_EDITADO")]
        public async Task<ActionResult<ResponseDto<UserActionResponseDto>>> 
            Delete(string id) 
        {
            var response = await _usersService.DeleteAsync(id);

            return StatusCode(response.StatusCode, new ResponseDto<UserActionResponseDto> 
            {
                StatusCode = response.StatusCode,
                Status = response.Status,
                Message = response.Message,
                Data = response.Data
            });
        }

    }
}
