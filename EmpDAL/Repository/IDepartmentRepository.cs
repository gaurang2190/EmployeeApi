using EmpDAL.Models;

namespace EmpDAL.Repository;

public interface IDepartmentRepository
{
    IEnumerable<Department> GetAll();

    bool InsertDepartment(Department department);

    bool UpdateDepartment(Department department);

    bool DeleteDepartment(int id);

    Department? GetDepartmentById(int id);
}
