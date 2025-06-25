namespace Persons.API.Dtos.Security.Users
{
    public class UserEditDto : UserCreateDto
    {

        public bool ChangePassword { get; set; }
    }
}
