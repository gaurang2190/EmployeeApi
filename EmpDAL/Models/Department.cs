using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmpDAL.Models
{
    public class Department
    {
        [Key]
        [DisplayName("Department Id")]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Employee> Employees { get; set; }

    }
}
