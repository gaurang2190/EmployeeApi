using EmpBAL.Dtos;

namespace EmpBAL.Services
{
    public interface IAuthService
    {
        ServiceResponse<string> RegisterUserService(RegisterDto register);

        ServiceResponse<string> LoginUserService(LoginDto login);
    }
}
