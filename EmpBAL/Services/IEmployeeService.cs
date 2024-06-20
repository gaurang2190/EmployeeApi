using EmpBAL.Dtos;
using EmpDAL.Models;

namespace EmpBAL.Services
{
    public interface IEmployeeService
    {
        ServiceResponse<string> AddEmployee(Employee employee);
        ServiceResponse<string> UpdateEmployee(Employee employee);
        ServiceResponse<string> DeleteEmployee(int id);
        ServiceResponse<IEnumerable<EmployeeDto>> GetAllEmployees();
        ServiceResponse<EmployeeDto> GetEmployeeById(int id);
        ServiceResponse<int> TotalEmployee();
        ServiceResponse<IEnumerable<EmployeeDto>> GetPaginatedEmployee(int page, int pageSize);
    }
}
