using EmployeeApi.Dtos;
using EmployeeApi.Models;
using EmployeeApi.Repository;

namespace EmployeeApi.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repository;

        public DepartmentService(IDepartmentRepository repository)
        {
            _repository = repository;
        }

        public ServiceResponse<IEnumerable<DepartmentDto>> GetDepartments()
        {
            var response = new ServiceResponse<IEnumerable<DepartmentDto>>();
            var departments = _repository.GetAll();
            if (departments != null && departments.Any())
            {
                List<DepartmentDto> departmentDtos = new List<DepartmentDto>();
                foreach (var department in departments)
                {
                    departmentDtos.Add(
                        new DepartmentDto() { DepartmentId = department.DepartmentId, DepartmentName = department.Name, DepartmentDescription = department.Description }
                        );
                }

                response.Data = departmentDtos;
            }
            else
            {
                response.Success = false;
                response.Message = "No record found!";
            }

            return response;
        }

        public ServiceResponse<DepartmentDto> GetDepartmentById(int departmentId)
        {
            var response = new ServiceResponse<DepartmentDto>();
            var existingDepartment = _repository.GetDepartmentById(departmentId);
            if (existingDepartment != null)
            {
                var department = new DepartmentDto()
                {
                    DepartmentId = departmentId,
                    DepartmentName = existingDepartment.Name,
                    DepartmentDescription = existingDepartment.Description
                };

                response.Data = department;
            }
            else
            {
                response.Success = false;
                response.Message = "No record found!";
            }

            return response;
        }

        public ServiceResponse<string> AddDepartment(Department department)
        {
            var response = new ServiceResponse<string>();
            

            var result = _repository.InsertDepartment(department);
            if (result)
            {
                response.Message = "Department saved successfully.";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong, please try after sometime.";
            }

            return response;
        }

        public ServiceResponse<string> UpdateDepartment(Department department)
        {
            var response = new ServiceResponse<string>();

            var existingDepartment = _repository.GetDepartmentById(department.DepartmentId);
            var result = false;
            if (existingDepartment != null)
            {
                existingDepartment.Name = department.Name;
                existingDepartment.Description = department.Description;
                result = _repository.UpdateDepartment(existingDepartment);
            }

            if (result)
            {
                response.Message = "Department updated successfully.";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong, please try after sometime.";
            }

            return response;
        }

        public ServiceResponse<string> DeleteDepartment(int id)
        {
            var response = new ServiceResponse<string>();
            var result = _repository.DeleteDepartment(id);
            if (result)
            {
                response.Message = "Department deleted successfully.";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong, please try after sometimes.";
            }

            return response;
        }
    }
}
