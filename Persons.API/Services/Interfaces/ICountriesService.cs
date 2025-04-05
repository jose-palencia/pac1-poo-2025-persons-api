using Persons.API.Dtos.Common;
using Persons.API.Dtos.Countries;

namespace Persons.API.Services.Interfaces
{
    public interface ICountriesService
    {
        Task<ResponseDto<CountryActionResponseDto>> CreateAsync(CountryCreateDto dto);
        Task<ResponseDto<CountryActionResponseDto>> DeleteAsync(string id);
        Task<ResponseDto<CountryActionResponseDto>> EditAsync(CountryEditDto dto, string id);
        Task<ResponseDto<PaginationDto<List<CountryDto>>>> GetListAsync(
            string searchTerm = "", int page = 1, int pageSize = 0
        );
        Task<ResponseDto<CountryDto>> GetOneByIdAsync(string id);
    }
}
