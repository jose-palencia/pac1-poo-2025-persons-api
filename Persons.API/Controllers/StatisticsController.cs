using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persons.API.Constants;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Statistics;
using Persons.API.Services.Interfaces;

namespace Persons.API.Controllers
{
    [Route("api/statistics")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("counts")]
        [Authorize(Roles = $"{RolesConstant.SYS_ADMIN_EDITADO}, {RolesConstant.NORMAL_USER}")]
        public async Task<ActionResult<ResponseDto<StatisticsDto>>> GetCounts() 
        {
            var response = await _statisticsService.GetCounts();

            return StatusCode(response.StatusCode, new ResponseDto<StatisticsDto> 
            {
                Status = response.Status,
                Message = response.Message,
                Data = response.Data
            });
        }

    }
}
