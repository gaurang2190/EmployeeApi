using EmpDAL.Models;
namespace EmpDAL.Repository;

public interface IAuthRepository
{
    bool RegisterUser(User user);

    User? ValidateUser(string username);

    bool UserExists(string loginId, string email);
}
