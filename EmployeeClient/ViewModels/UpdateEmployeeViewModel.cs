using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EmployeeClient.ViewModels
{
    public class UpdateEmployeeViewModel
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

        public byte[]? ImageData { get; set; }

        [Required(ErrorMessage = "Please select a file.")]
        public IFormFile File { get; set; }

        public List<DepartmentViewModel>? Departments { get; set; }
    }
}
