using Persons.API.Dtos.Common;
using Persons.API.Dtos.Statistics;

namespace Persons.API.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<ResponseDto<StatisticsDto>> GetCounts();
    }
}
