
using System.ComponentModel.DataAnnotations;

namespace EmpBAL.Dtos
{
    public class AddEmployeeDto
    {
        
        [Required(ErrorMessage = "Employee name is required.")]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        public string Gender { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        public int Salary { get; set; }

        public bool IsPermenant { get; set; }

        public string FileName { get; set; } = string.Empty;

        //[Required(ErrorMessage = "File is required.")]
        //public IFormFile File { get; set; }
    }
}
