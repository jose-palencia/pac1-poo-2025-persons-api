using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persons.API.Constants;
using Persons.API.Database;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Countries;
using Persons.API.Dtos.Persons;
using Persons.API.Services.Interfaces;

namespace Persons.API.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly PersonsDbContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public CountriesService(
            PersonsDbContext personsDbContext,
            IMapper mapper,
            IConfiguration configuration)
        {
            _context = personsDbContext;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        public async Task<ResponseDto<PaginationDto<List<CountryDto>>>> GetListAsync(
            string searchTerm = "", int page = 1, int pageSize = 0
         ) 
        {
            pageSize = pageSize == 0 ? PAGE_SIZE : pageSize;
            int startIndex = (page - 1) * pageSize;

            IQueryable<CountryEntity> countriesQuery = _context.Countries;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                countriesQuery = countriesQuery
                    .Where(x => (x.Name + " " + x.AlphaCode3)
                    .Contains(searchTerm));
            }

            int totalRows = await countriesQuery.CountAsync();

            var countriesEntity = await countriesQuery
                .OrderBy(x => x.Name)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            var countriesDtos = _mapper.Map<List<CountryDto>>(countriesEntity);

            return new ResponseDto<PaginationDto<List<CountryDto>>> 
            {
                StatusCode = HttpStatusCode.OK,
                Status =  true,
                Message = "Registros obtenidos correctamente",
                Data = new PaginationDto<List<CountryDto>>
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = countriesDtos,
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && page < (int)Math
                        .Ceiling((double)totalRows / pageSize),
                    HasPreviousPage = page > 1
                }

            };
        }

        public async Task<ResponseDto<CountryDto>> GetOneByIdAsync(string id) 
        {
            var country = await _context.Countries
                .FirstOrDefaultAsync(x => x.Id == id);

            if (country is null) 
            {
                return new ResponseDto<CountryDto> 
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "El registro no fue encontrado."
                };
            }

            return new ResponseDto<CountryDto> 
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro econtrado",
                Data = _mapper.Map<CountryDto>(country)
            };
        }

        public async Task<ResponseDto<CountryActionResponseDto>> CreateAsync(
            CountryCreateDto dto
            ) 
        {
            var countryEntity = _mapper.Map<CountryEntity>(dto);

            countryEntity.Id = Guid.NewGuid().ToString();

            _context.Countries.Add(countryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CountryActionResponseDto> 
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado correctamente",
                Data = _mapper.Map<CountryActionResponseDto>(countryEntity)
            };
        }

        public async Task<ResponseDto<CountryActionResponseDto>> EditAsync(
            CountryEditDto dto, string id
            )
        {
            var countryEntity = await _context.Countries.FindAsync(id);

            if (countryEntity is null) 
            {
                return new ResponseDto<CountryActionResponseDto> 
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            _mapper.Map<CountryEditDto, CountryEntity>(dto, countryEntity);

            _context.Countries.Update(countryEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<CountryActionResponseDto> 
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro editado correctamente",
                Data = _mapper.Map<CountryActionResponseDto>(countryEntity)
            };
        }

        public async Task<ResponseDto<CountryActionResponseDto>> DeleteAsync(
            string id) 
        {
            var countryEntity = await _context.Countries.FindAsync(id);

            if (countryEntity is null)
            {
                return new ResponseDto<CountryActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                };
            }

            var personsInCountry = await _context.Persons
                .CountAsync(p => p.CountryId == id);

            if (personsInCountry > 0)
            {
                return new ResponseDto<CountryActionResponseDto> 
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El pais tiene datos relacionados."
                };
            }

            _context.Countries.Remove(countryEntity);

            await _context.SaveChangesAsync();

            return new ResponseDto<CountryActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro eliminado correctamente",
                Data = _mapper.Map<CountryActionResponseDto>(countryEntity)

            };
        }
    }
}
