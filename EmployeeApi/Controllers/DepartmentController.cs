using EmployeeApi.Dtos;
using EmployeeApi.Models;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
   [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("GetAllDepartments")]
        public IActionResult GetAllDepartments()
        {
            var response = _departmentService.GetDepartments();
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet("GetDepartmentById/{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            var response = _departmentService.GetDepartmentById(id);
            if (!response.Success)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpPost("Create")]
        public IActionResult AddDepartment(AddDepartmentDto departmentDto)
        {

            var department = new Department()
            {
                Name = departmentDto.DepartmentName,
                Description = departmentDto.DepartmentDescription,
            };

            var result = _departmentService.AddDepartment(department);
            return !result.Success ? BadRequest(result) : Ok(result);

        }

        [HttpPut("UpdateDepartment")]
        public IActionResult UpdateDepartment(UpdateDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                DepartmentId = departmentDto.DepartmentId,
                Name = departmentDto.DepartmentName,
                Description = departmentDto.DepartmentDescription,
            };

            var response = _departmentService.UpdateDepartment(department);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }

        [HttpDelete("DeleteDepartment/{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            if (id > 0)
            {
                var response = _departmentService.DeleteDepartment(id);

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
    }
}
