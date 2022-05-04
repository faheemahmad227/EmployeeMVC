using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required, StringLength(30)]
        public string Name { get; set; }

        public Double Salary { get; set; }

        [Required, StringLength(100)]
        public string Address { get; set; }

        public String DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
