using Persons.API.Dtos.Common;
using Persons.API.Dtos.Security.Users;

namespace Persons.API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ResponseDto<UserActionResponseDto>> CreateAsync(UserCreateDto dto);
    }
}
