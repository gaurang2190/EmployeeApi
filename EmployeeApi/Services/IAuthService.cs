using EmployeeApi.Dtos;

namespace EmployeeApi.Services
{
    public interface IAuthService
    {
        ServiceResponse<string> RegisterUserService(RegisterDto register);

        ServiceResponse<string> LoginUserService(LoginDto login);
    }
}
