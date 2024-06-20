using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeClient.ViewModels
{
    public class EmployeeViewModel
    {
        [Required(ErrorMessage = "Employee name is required.")]
        [StringLength(50)]
        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }

        public string Gender { get; set; }

        [DisplayName("Department")]
        
        [Required(ErrorMessage = "Department is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid option.")]
        public int DepartmentId { get; set; }

        public List<DepartmentViewModel>? Departments { get; set; }

        [Required(ErrorMessage = "Salary is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "The value must be a non-negative number.")]
        public int Salary { get; set; }

        [DisplayName("Is Permenant")]
        public bool IsPermenant { get; set; }


        [Required(ErrorMessage = "Please select a file.")]
        public IFormFile File { get; set; }

        //public byte[] ImageData { get; set; }

        public string FileName { get; set; } = string.Empty;
    }
}
