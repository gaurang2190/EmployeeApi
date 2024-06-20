using System.ComponentModel.DataAnnotations;

namespace EmpBAL.Dtos
{
    public class UpdateDepartmentDto
    {
        [Required(ErrorMessage = "Department id is required.")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Department description is required.")]
        public string DepartmentDescription { get; set; }
    }
}
