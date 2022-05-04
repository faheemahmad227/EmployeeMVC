using EmployeeMVC.Models;
using EmployeeMVC.Models.EmployeesViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeMVC.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IRepository<Employee> employeeRepository;
        private readonly EmployeesDbContext context;
        public EmployeesController(IRepository<Employee> employeeRepositoryt, EmployeesDbContext context)
        {
            this.employeeRepository = employeeRepositoryt;
            this.context = context;
        }

        public IActionResult Index()
        {
            var employees = context.Employees.Include(m => m.Department).ToList();
            return View(employees);
        }

        public IActionResult Create()
        {
            var createEmployee = new EmployeesViewModel()
            {
                Departments = context.Departments.ToList()
            };

            return View("Create", createEmployee);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            employeeRepository.Add(employee);
            await employeeRepository.SaveAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);

            if (employee == null) return NotFound();

            return View(employee);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);

            if (employee == null) return NotFound();

            var editEmployee = new EmployeesViewModel()
            {
                Employee = employee,
                Departments = context.Departments.ToList()
            };

            return View(editEmployee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            employeeRepository.Update(employee);
            await employeeRepository.SaveAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);

            if (employee == null) return NotFound();

            return View(employee);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await employeeRepository.GetByIdAsync(id);
            employeeRepository.Delete(employee);
            await employeeRepository.SaveAsync();
            return RedirectToAction("Index");
        }
    }
}
