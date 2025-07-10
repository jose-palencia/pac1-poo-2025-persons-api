using Persons.API.Dtos.Common;
using Persons.API.Dtos.Security.Auth;

namespace Persons.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<LoginResponseDto>> LoginAsync(LoginDto dto);
    }
}
