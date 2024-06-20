using EmployeeApi.Models;

namespace EmployeeApi.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly EmpDbContext _dbContext;

        public AuthRepository(EmpDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool RegisterUser(User user)
        {

            var result = false;
            if (user != null) { 
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public bool UserExists(string loginId, string email)
        {
           if(_dbContext.Users.Where(m =>  m.LoginId == loginId || m.Email == email).Any())
            {
                return true;
            }
           return false;
        }

        public User? ValidateUser(string username)
        {
            User? user = _dbContext.Users.Where(m => m.LoginId == username || m.Email == username).FirstOrDefault();
            return user;
        }
    }
}
