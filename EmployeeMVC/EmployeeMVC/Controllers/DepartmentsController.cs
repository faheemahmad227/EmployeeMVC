using EmployeeMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMVC.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IRepository<Department> departmentRepository;

        public DepartmentsController(IRepository<Department> departmentRepo)
        {
            this.departmentRepository = departmentRepo;
        }

        public async Task<IActionResult> Index()
        {
            var data = await departmentRepository.GetAsync();
            return View(data);
        }
    }
}
