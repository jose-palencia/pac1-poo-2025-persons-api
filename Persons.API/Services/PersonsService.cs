using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persons.API.Constants;
using Persons.API.Database;
using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Persons;
using Persons.API.Services.Interfaces;

namespace Persons.API.Services
{
    public class PersonsService : IPersonsService
    {
        private readonly PersonsDbContext _context;
        private readonly IMapper _mapper;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public PersonsService(
            PersonsDbContext context, 
            IMapper mapper,
            IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

        public async Task<ResponseDto<PaginationDto<List<PersonDto>>>> GetListAsync(
            string searchTerm = "", int page = 1, int pageSize = 0
        ) 
        {
            pageSize = pageSize == 0 ? PAGE_SIZE : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<PersonEntity> personQuery = _context.Persons;

            if (!string.IsNullOrEmpty(searchTerm)) 
            {
                personQuery = personQuery
                    .Where(x => (x.DNI + " " + x.FirstName + " " + x.LastName)
                    .Contains(searchTerm));
            }

            int totalRows = await personQuery.CountAsync();

            var personsEntity = await personQuery
                .OrderBy(x => x.FirstName)
                .Skip(startIndex)
                .Take(pageSize)
                .ToListAsync();

            var personsDto = _mapper.Map<List<PersonDto>>(personsEntity);

            return new ResponseDto<PaginationDto<List<PersonDto>>> 
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = personsEntity.Count() > 0 
                    ? "Registros encontrados" 
                    : "No se encontraron registros",
                Data = new PaginationDto<List<PersonDto>> 
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = personsDto,
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && page < (int)Math
                        .Ceiling((double)totalRows / pageSize),
                    HasPreviousPage = page > 1
                }

            };
            
        }

        public async Task<ResponseDto<PersonDto>> GetOneByIdAsync(Guid id) 
        {
            var personEntity = await _context.Persons
                .FirstOrDefaultAsync(person => person.Id == id);

            if (personEntity is null) 
            {
                return new ResponseDto<PersonDto> 
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado"
                
                };
            }

            return new ResponseDto<PersonDto> 
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro encontrado",
                Data = _mapper.Map<PersonDto> (personEntity)
            };

        }

        public async Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonCreateDto dto)
        {
            var personEntity = _mapper.Map<PersonEntity>(dto);

            var countryEntity = await _context.Countries
                .FirstOrDefaultAsync(c => c.Id == dto.CountryId);

            if (countryEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto> 
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El pais no existe"
                };
            }

            _context.Persons.Add(personEntity);

            await _context.SaveChangesAsync();            

            return new ResponseDto<PersonActionResponseDto> 
            {
                StatusCode = HttpStatusCode.CREATED,
                Status = true,
                Message = "Registro creado correctamente",
                Data = _mapper.Map<PersonActionResponseDto>(personEntity)
            };
        }

        public async Task<ResponseDto<PersonActionResponseDto>> EditAsync(
            PersonEditDto dto, Guid id) 
        {
            var personEntity =  await _context.Persons
                .FirstOrDefaultAsync(x => x.Id == id);

            if (personEntity is null) 
            {
                return new ResponseDto<PersonActionResponseDto> 
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            var countryEntity = await _context.Countries
                .FirstOrDefaultAsync(c => c.Id == dto.CountryId);

            if (countryEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto>
                {
                    StatusCode = HttpStatusCode.BAD_REQUEST,
                    Status = false,
                    Message = "El pais no existe"
                };
            }

            _mapper.Map<PersonEditDto, PersonEntity>(dto, personEntity);

            _context.Persons.Update(personEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PersonActionResponseDto> 
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro editado corretamente",
                Data = _mapper.Map<PersonActionResponseDto>(personEntity)
            };

        }


        public async Task<ResponseDto<PersonActionResponseDto>> DeleteAsync(
            Guid id) 
        {
            var personEntity =  await _context.Persons
                .FirstOrDefaultAsync(x => x.Id == id);

            if (personEntity is null)
            {
                return new ResponseDto<PersonActionResponseDto> 
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = "Registro no encontrado",
                };
            }

            _context.Persons.Remove(personEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PersonActionResponseDto> 
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Registro eliminado correctamente",
                Data = _mapper.Map<PersonActionResponseDto>(personEntity)
            };

        }
    
    
    }
}
