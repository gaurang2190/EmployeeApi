using EmpDAL.Models;

namespace EmpDAL.Repository;

public interface IEmployeeRepository
{
    bool InsertEmployee(Employee employee);
    IEnumerable<Employee> GetAllEmployees();
    Employee GetEmployeeById(int id);
    bool UpdateEmployee(Employee employee);
    bool DeleteEmployee(int id);
    int TotalEmployee();
    IEnumerable<Employee> GetPagintaedEmployee(int page, int pagesize);
}
