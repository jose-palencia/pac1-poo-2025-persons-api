using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Countries;
using Persons.API.Services.Interfaces;

namespace Persons.API.Controllers
{
    [Route("api/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<CountryDto>>>> GetList()
        {
            var response = await _countriesService.GetListAsync();

            return StatusCode(response.StatusCode, new 
            {
                response.Status,
                response.Message,
                response.Data
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<List<CountryDto>>>> GetOne(Guid id)
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
        public async Task<ActionResult<ResponseDto<List<CountryActionResponseDto>>>> Create(
            [FromBody] CountryEditDto dto, Guid id)
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
        public async Task<ActionResult<ResponseDto<List<CountryActionResponseDto>>>> Create(
           Guid id)
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
