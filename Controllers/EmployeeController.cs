using EmployeeManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize] // Uncomment to enable JWT authentication
    public class EmployeeController : ControllerBase
    {
        // Static in-memory storage for employees
        private static List<Employee> _employees = new List<Employee>
        {
            new Employee { Id = 1, Name = "Vaishnavi Deshpande", Position = "Developer", Salary = 50000, IsDeleted = false },
            new Employee { Id = 2, Name = "Mayur Jambhulakar", Position = "Manager", Salary = 70000, IsDeleted = false }
        };

        // Get all employees
        [HttpGet]
        public IActionResult GetEmployees()
        {
            try
            {
                // Filter out deleted employees 
                var employees = _employees.Where(e => !e.IsDeleted).ToList();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get employee by ID
        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            try
            {
                var employee = _employees.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
                if (employee == null)
                    return NotFound($"Employee with ID {id} not found.");

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Add a new employee
        [HttpPost]
        public IActionResult AddEmployee([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                // Generate a new ID
                employee.Id = _employees.Any() ? _employees.Max(e => e.Id) + 1 : 1;
                employee.IsDeleted = false;

                // Add the employee to the in-memory list
                _employees.Add(employee);

                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Update an existing employee
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var existingEmployee = _employees.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
                if (existingEmployee == null)
                    return NotFound($"Employee with ID {id} not found.");

                // Update fields
                existingEmployee.Name = employee.Name;
                existingEmployee.Position = employee.Position;
                existingEmployee.Salary = employee.Salary;

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Soft delete an employee
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                var employee = _employees.FirstOrDefault(e => e.Id == id && !e.IsDeleted);
                if (employee == null)
                    return NotFound($"Employee with ID {id} not found.");

                // Perform soft delete
                employee.IsDeleted = true;

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
