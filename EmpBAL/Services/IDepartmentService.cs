using EmpBAL.Dtos;
using EmpDAL.Models;

namespace EmpBAL.Services
{
    public interface IDepartmentService
    {
        ServiceResponse<IEnumerable<DepartmentDto>> GetDepartments();

        ServiceResponse<DepartmentDto> GetDepartmentById(int departmentId);

        ServiceResponse<string> AddDepartment(Department department);

        public ServiceResponse<string> UpdateDepartment(Department department);

        ServiceResponse<string> DeleteDepartment(int id);

    }
}
