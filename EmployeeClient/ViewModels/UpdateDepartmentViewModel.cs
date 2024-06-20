using System.ComponentModel.DataAnnotations;

namespace EmployeeClient.ViewModels
{
    public class UpdateDepartmentViewModel
    {
        [Required(ErrorMessage = "Department id is required.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }

        [Display(Name = "Department Description")]
        [Required(ErrorMessage = "Department description is required.")]
        public string DepartmentDescription { get; set; }
    }
}
