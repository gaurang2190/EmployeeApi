﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.Dtos
{
    public class AddDepartmentDto
    {
        [Required(ErrorMessage = "Department name is required.")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Department description is required.")]
        public string DepartmentDescription { get; set; }
    }
}
