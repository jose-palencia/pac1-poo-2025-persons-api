using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Security.Roles;
using Persons.API.Services.Interfaces;

namespace Persons.API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<PaginationDto
            <List<RoleDto>>>>> GetListPagination(
            string searchTerm = "", int page = 1, int pageSize = 10
            )
        {
            var response = await _rolesService
                    .GetListAsync(searchTerm, page, pageSize);

            return StatusCode(response.StatusCode,
                new ResponseDto<PaginationDto<List<RoleDto>>>
                {
                    Status = response.Status,
                    Message = response.Message,
                    Data = response.Data
                });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<RoleDto>>> GetOneById(string id) 
        {
            var response = await _rolesService.GetOneById(id);
            return StatusCode(response.StatusCode,
                new ResponseDto<RoleDto>
                {
                    Status = response.Status,
                    Message = response.Message,
                    Data = response.Data
                });
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDto<RoleActionResponseDto>>> 
            CreateAsync(
            RoleCreateDto dto
        )
        {
            var response = await _rolesService.CreateAsync(dto);
            return StatusCode(response.StatusCode,
                new ResponseDto<RoleActionResponseDto>
                {
                    Status = response.Status,
                    Message = response.Message,
                    Data = response.Data
                });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<RoleActionResponseDto>>> Edit(
            [FromBody] RoleEditDto dto, string id
            ) 
        {
            var response = await _rolesService.EditAsync(dto, id);
            return StatusCode(response.StatusCode,
                new ResponseDto<RoleActionResponseDto>
                {
                    Status = response.Status,
                    Message = response.Message,
                    Data = response.Data
                });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<RoleActionResponseDto>>> Delete(string id) 
        {
            var response = await _rolesService.DeleteAsync(id);
            return StatusCode(response.StatusCode,
                new ResponseDto<RoleActionResponseDto>
                {
                    Status = response.Status,
                    Message = response.Message,
                    Data = response.Data
                });
        }
    }
}
