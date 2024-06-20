
using EmpDAL.Models;
using System.ComponentModel.DataAnnotations;

namespace EmpBAL.Dtos
{
    public class EmployeeDto
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        public string Gender { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

        [Required]
        public int Salary { get; set; }

        public bool IsPermenant { get; set; }

        public byte[] ImageData { get; set; }
    }
}
