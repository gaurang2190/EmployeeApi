
using EmployeeApi.Models;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetAllEmployees")]
        public IActionResult GetAllEmployees()
        {
            var response = _employeeService.GetAllEmployees();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("AddEmployee")]
        public IActionResult AddEmployee([FromForm] string EmployeeName, [FromForm] string Gender, [FromForm] string DepartmentId, [FromForm] string Salary, [FromForm] string IsPermenant, [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using var memorystream = new MemoryStream();
            file.CopyTo(memorystream);

            var employee = new Employee
            {
                DepartmentId = Convert.ToInt32(DepartmentId),
                EmployeeName = EmployeeName,
                Salary = Convert.ToInt32(Salary),
                IsPermenant = Convert.ToBoolean(IsPermenant),
                Gender = Gender,
                ImageData = memorystream.ToArray(),
                FileName = file.FileName

            };

            var result = _employeeService.AddEmployee(employee);
            return !result.Success ? BadRequest(result) : Ok(result);
        }



        //[HttpPost("AddEmployee")]
        //public IActionResult AddEmployee(AddEmployeeDto addEmployee)
        //{
        //    var employee = new Employee
        //    {
        //        DepartmentId = addEmployee.DepartmentId,
        //        EmployeeName = addEmployee.EmployeeName,
        //        Salary = addEmployee.Salary,
        //        IsPermenant = addEmployee.IsPermenant,
        //        Gender = addEmployee.Gender,
        //        FileName = addEmployee.FileName,
        //    };

        //    var result = _employeeService.AddEmployee(employee);
        //    return !result.Success ? BadRequest(result) : Ok(result);
        //}

        [HttpGet("GetEmployeeById/{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            var response = _employeeService.GetEmployeeById(id);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPut("UpdateEmployee")]
        public IActionResult UpdateEmployee([FromForm] string EmployeeId, [FromForm] string EmployeeName, [FromForm] string Gender, [FromForm] string DepartmentId, [FromForm] string Salary, [FromForm] string IsPermenant, [FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            using var memorystream = new MemoryStream();
            file.CopyTo(memorystream);


            var employee = new Employee
            {
                DepartmentId = Convert.ToInt32(DepartmentId),
                EmployeeName = EmployeeName,
                Salary = Convert.ToInt32(Salary),
                IsPermenant = Convert.ToBoolean(IsPermenant),
                Gender = Gender,
                EmployeeId = Convert.ToInt32(EmployeeId),
                ImageData = memorystream.ToArray(),
                FileName = file.FileName
            };

            var response = _employeeService.UpdateEmployee(employee);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }

        //[HttpPut("UpdateEmployee")]
        //public IActionResult UpdateEmployee(UpdateEmployeeDto employeeDto)
        //{

        //    var employee = new Employee
        //    {
        //        DepartmentId = employeeDto.DepartmentId,
        //        EmployeeName = employeeDto.EmployeeName,
        //        Salary = employeeDto.Salary,
        //        IsPermenant = employeeDto.IsPermenant,
        //        Gender = employeeDto.Gender,
        //        EmployeeId = employeeDto.EmployeeId,
        //    };

        //    var response = _employeeService.UpdateEmployee(employee);
        //    if (!response.Success)
        //    {
        //        return BadRequest(response);
        //    }
        //    else
        //    {
        //        return Ok(response);
        //    }
        //}

        [HttpDelete("DeleteEmployee/{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            if (id > 0)
            {
                var response = _employeeService.DeleteEmployee(id);

                if (!response.Success)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            else
            {
                return BadRequest("Please enter proper data.");
            }
        }

        [HttpGet("TotalEmployee")]
        public IActionResult TotalEmployee() { 
         var response = _employeeService.TotalEmployee();
            return Ok(response);
        
        }
        [HttpGet("GetPaginatedEmployee/{page}/{pageSize}")]
        public IActionResult GetPaginatedEmployee(int page, int pageSize)
        {
            var response = _employeeService.GetPaginatedEmployee(page, pageSize);
            return Ok(response);

        }
    }
}
