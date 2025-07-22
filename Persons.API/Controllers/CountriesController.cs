using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persons.API.Constants;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Countries;
using Persons.API.Services.Interfaces;

namespace Persons.API.Controllers
{
    [Route("api/countries")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpGet]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN_EDITADO}, {RolesConstant.NORMAL_USER}")]
        public async Task<ActionResult<ResponseDto<List<CountryDto>>>> GetList(
            string searchTerm = "", int page = 1, int pageSize = 0
            )
        {
            var response = await _countriesService.GetListAsync(
                searchTerm, page, pageSize);

            return StatusCode(response.StatusCode, new 
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN_EDITADO}, {RolesConstant.NORMAL_USER}")]
        public async Task<ActionResult<ResponseDto<List<CountryDto>>>> GetOne(string id)
        {
            var response = await _countriesService.GetOneByIdAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPost]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN_EDITADO}")]
        public async Task<ActionResult<ResponseDto<List<CountryActionResponseDto>>>> Create(
            [FromBody] CountryCreateDto dto)
        {
            var response = await _countriesService.CreateAsync(dto);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN_EDITADO}")]
        public async Task<ActionResult<ResponseDto<List<CountryActionResponseDto>>>> Edit(
            [FromBody] CountryEditDto dto, string id)
        {
            var response = await _countriesService.EditAsync(dto, id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN_EDITADO}")]
        public async Task<ActionResult<ResponseDto<List<CountryActionResponseDto>>>> Create(
           string id)
        {
            var response = await _countriesService.DeleteAsync(id);

            return StatusCode(response.StatusCode, new
            {
                response.Status,
                response.Message,
                response.Data
            });
        }
    }
}
