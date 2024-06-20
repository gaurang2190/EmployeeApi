using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeClient.ViewModels
{
    public class EmployeesViewModel
    {
        public int EmployeeId { get; set; }

        [DisplayName("Employee Name")]
        public string EmployeeName { get; set; }

        public string Gender { get; set; }

        [DisplayName("Department")]
        public int DepartmentId { get; set; }

        public EmployeeDepartmentViewModel Department { get; set; }

        public int Salary { get; set; }

        [DisplayName("Is Permenant")]
        public bool IsPermenant { get; set; }

        public byte[] ImageData { get; set; }
    }
}
