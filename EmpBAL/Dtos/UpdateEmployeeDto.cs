using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EmpBAL.Dtos
{
    public class UpdateEmployeeDto
    {
        [Required(ErrorMessage = "Employee id is required")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Employee name is required.")]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        public int Salary { get; set; }

        public bool IsPermenant { get; set; }

        public IFormFile File { get; set; }
    }
}
