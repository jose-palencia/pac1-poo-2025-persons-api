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

        public PersonsService(PersonsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<PersonDto>>> GetListAsync() 
        {
            var personsEntity = await _context.Persons.ToListAsync();

            var personsDto = _mapper.Map<List<PersonDto>>(personsEntity);

            return new ResponseDto<List<PersonDto>> 
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = personsEntity.Count() > 0 
                    ? "Registros encontrados" 
                    : "No se encontraron registros",
                Data = personsDto

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
