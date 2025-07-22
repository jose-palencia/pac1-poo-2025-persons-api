using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Persons.API.Constants;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Persons;
using Persons.API.Services.Interfaces;

namespace Persons.API.Controllers
{
    [ApiController]
    [Route("api/persons")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonsService _personsService;

        public PersonsController(IPersonsService personsService)
        {
            _personsService = personsService;
        }

        [HttpGet]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN_EDITADO}, {RolesConstant.NORMAL_USER}")]
        public async Task<ActionResult<ResponseDto<List<PersonDto>>>> GetList(
            string searchTerm = "", int page = 1, int pageSize = 0
        ) 
        {
            var response = await _personsService.GetListAsync(searchTerm, page, pageSize);

            return StatusCode(response.StatusCode, new 
            {
                response.Status,
                response.Message,
                response.Data
            });
        }
 
        [HttpGet("{id}")]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN_EDITADO}, {RolesConstant.NORMAL_USER}")]
        public async Task<ActionResult<ResponseDto<PersonDto>>> GetOne(string id) 
        {
            var response = await _personsService.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, response);
        }
        
        
        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN_EDITADO}, {RolesConstant.NORMAL_USER}")]
        public async Task<ActionResult<ResponseDto<PersonActionResponseDto>>> Post([FromBody] PersonCreateDto dto) 
        {
            var response = await _personsService.CreateAsync(dto);
            
            return StatusCode(response.StatusCode, new 
            {
                response.Status,
                response.Message,
                response.Data,
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN_EDITADO}, {RolesConstant.NORMAL_USER}")]
        public async Task<ActionResult<ResponseDto<PersonActionResponseDto>>> Edit([FromBody] PersonEditDto dto, string Id)
        {
            var response =  await _personsService.EditAsync(dto, Id);

            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN_EDITADO}, {RolesConstant.NORMAL_USER}")]
        public async Task<ActionResult<ResponseDto<PersonActionResponseDto>>> Delete(
            string id)
        {
            var response = await _personsService.DeleteAsync(id);

            return StatusCode(response.StatusCode, response);
        }
    }
}
