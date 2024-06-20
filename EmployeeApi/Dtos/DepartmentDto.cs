﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeApi.Dtos
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string DepartmentDescription { get; set; }
    }
}
