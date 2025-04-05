﻿using Persons.API.Database.Entities;
using Persons.API.Dtos.Common;
using Persons.API.Dtos.Persons;

namespace Persons.API.Services.Interfaces
{
    public interface IPersonsService
    {
        Task<ResponseDto<PersonActionResponseDto>> CreateAsync(PersonCreateDto person);
        Task<ResponseDto<PersonActionResponseDto>> DeleteAsync(string id);
        Task<ResponseDto<PersonActionResponseDto>> EditAsync(PersonEditDto dto, string id);
        Task<ResponseDto<PaginationDto<List<PersonDto>>>> GetListAsync(
            string searchTerm = "", int page = 1, int pageSize = 0 
        );
        Task<ResponseDto<PersonDto>> GetOneByIdAsync(string id);
    }
}
