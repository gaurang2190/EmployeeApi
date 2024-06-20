using EmployeeApi.Dtos;
using EmployeeApi.Models;
using EmployeeApi.Repository;

namespace EmployeeApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public ServiceResponse<string> AddEmployee(Employee employee)
        {
            var response = new ServiceResponse<string>();

            if (employee == null)
            {
                response.Success = false;
                response.Message = "Something went wrong. Please try after sometime.";
                return response;
            }
            
            var result = _repository.InsertEmployee(employee);
            response.Success = result;
            response.Message = result ? "Employee saved successfully." : "Something went wrong. Please try after sometime.";

            return response;
        }

        public ServiceResponse<string> UpdateEmployee(Employee employee)
        {
            var response = new ServiceResponse<string>();
            if (employee == null)
            {
                response.Success = false;
                response.Message = "Something went wrong. Please try after sometime.";
                return response;
            }
            
            var updatedEmployee = _repository.GetEmployeeById(employee.EmployeeId);
            if (updatedEmployee != null)
            {
                updatedEmployee.EmployeeName = employee.EmployeeName;
                updatedEmployee.Gender = employee.Gender;
                updatedEmployee.IsPermenant = employee.IsPermenant;
                updatedEmployee.DepartmentId = employee.DepartmentId;
                updatedEmployee.Salary = employee.Salary;
                updatedEmployee.ImageData = employee.ImageData;
                updatedEmployee.FileName = employee.FileName;
                
                var result = _repository.UpdateEmployee(updatedEmployee);

                response.Success = result;
                response.Message = result ? "Employee updated successfully." : "Something went wrong. Please try after sometime.";
            }
            else
            {
                response.Success = false;
                response.Message = "Something went wrong. Please try after sometime.";
                return response;
            }

            return response;
        }

        public ServiceResponse<string> DeleteEmployee(int id)
        {
            var response = new ServiceResponse<string>();

            if (id < 0)
            {
                response.Success = false;
                response.Message = "No record to delete.";

            }

            var result = _repository.DeleteEmployee(id);
            response.Success = result;
            response.Message = result ? "Employee deleted successfully." : "Something went wrong, please try after sometime.";

            return response;
        }

        public ServiceResponse<IEnumerable<EmployeeDto>> GetAllEmployees()
        {
            var response = new ServiceResponse<IEnumerable<EmployeeDto>>();
            var employees = _repository.GetAllEmployees();

            if (employees == null)
            {
                response.Success = false;
                response.Data = new List<EmployeeDto>();
                response.Message = "No record found.";
                return response;
            }

            List<EmployeeDto> employeeDtos = new List<EmployeeDto>();
            foreach (var employee in employees.ToList())
            {
                employeeDtos.Add(
                    new EmployeeDto()
                    {
                        EmployeeId = employee.EmployeeId,
                        EmployeeName = employee.EmployeeName,
                        Salary = employee.Salary,
                        Gender = employee.Gender,
                        DepartmentId = employee.DepartmentId,
                        IsPermenant = employee.IsPermenant,
                        ImageData = employee.ImageData,
                        Department = new Department()
                        {
                            DepartmentId = employee.Department.DepartmentId,
                            Name = employee.Department.Name,
                        },
                    });

            }

            response.Data = employeeDtos;
            return response;
        }

        public ServiceResponse<EmployeeDto> GetEmployeeById(int id)
        {
            var response = new ServiceResponse<EmployeeDto>();

            var employee = _repository.GetEmployeeById(id);

            var employeeDto = new EmployeeDto()
            {
                EmployeeId = employee.EmployeeId,
                EmployeeName = employee.EmployeeName,
                Salary = employee.Salary,
                Gender = employee.Gender,
                DepartmentId = employee.DepartmentId,
                IsPermenant = employee.IsPermenant,
                ImageData= employee.ImageData,
                Department = new Department()
                {
                    DepartmentId = employee.Department.DepartmentId,
                    Name = employee.Department.Name,
                },
            };

            response.Data = employeeDto;
            return response;
        }

        public ServiceResponse<int> TotalEmployee() {
            var response = new ServiceResponse<int>();
            
            var totalEmployee = _repository.TotalEmployee();
            response.Data = totalEmployee;
            response.Success = true;
            return response;

        }

        public ServiceResponse<IEnumerable<EmployeeDto>> GetPaginatedEmployee(int page, int pageSize)
        {
            var response = new ServiceResponse<IEnumerable<EmployeeDto>>();
            var emp = _repository.GetPagintaedEmployee(page, pageSize);
            List<EmployeeDto> employeeDtos = new List<EmployeeDto>();
            foreach (var employee in emp.ToList())
            {
                employeeDtos.Add(
                    new EmployeeDto()
                    {
                        EmployeeId = employee.EmployeeId,
                        EmployeeName = employee.EmployeeName,
                        Salary = employee.Salary,
                        Gender = employee.Gender,
                        DepartmentId = employee.DepartmentId,
                        IsPermenant = employee.IsPermenant,
                        Department = new Department()
                        {
                            DepartmentId = employee.Department.DepartmentId,
                            Name = employee.Department.Name,
                        },
                    });

            }

            response.Data = employeeDtos;
            return response;
        }
    }
}
