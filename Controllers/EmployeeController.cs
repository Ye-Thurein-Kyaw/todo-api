using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public EmployeeController(IRepositoryWrapper RW)
        {
            _repositoryWrapper = RW;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeRequest>>> GetEmployees()
        {
          var employees =  await _repositoryWrapper.Employee.ListEmployee();
            return Ok(employees);
                
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeRequest>> GetEmployee(long id)
        {
             var employee = await _repositoryWrapper.Employee.FindByIDAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return EmployeeToDTO(employee);
            
        }

        // PUT: api/Employee/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(long id, EmployeeRequest employeeRequest)
        {
            if (id != employeeRequest.Id)
            {
                return BadRequest();
            }

            var employee = await _repositoryWrapper.Employee.FindByIDAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            employee.EmployeeName = employeeRequest.EmployeeName;
            employee.EmployeeAddress = employeeRequest.EmployeeAddress;
            employee.EmpDepartmentId = employeeRequest.EmpDepartmentId;

            try
            {
                await _repositoryWrapper.Employee.UpdateAsync(employee);
            }
            catch (DbUpdateConcurrencyException) when (!EmployeeExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Employee
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EmployeeRequest>> PostEmployee(EmployeeRequest employeeRequest)
        {
          var employee = new Employee
            {
                EmployeeName = employeeRequest.EmployeeName,
                EmployeeAddress = employeeRequest.EmployeeAddress,
                EmpDepartmentId = employeeRequest.EmpDepartmentId
            };

        
            await _repositoryWrapper.Employee.CreateAsync(employee, true);

            return CreatedAtAction(
                nameof(GetEmployee),
                new { id = employee.Id },
                EmployeeToDTO(employee));
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(long id)
        {
            if (_repositoryWrapper.Employee == null)
            {
                return NotFound();
            }
            var employee = await _repositoryWrapper.Employee.FindByIDAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _repositoryWrapper.Employee.DeleteAsync(employee, true);

            return NoContent();
        }

        [HttpPost("search/{term}")]
        public async Task<ActionResult<IEnumerable<EmployeeRequest>>>  SearchEmployee(string term)
        {
            var empList = await _repositoryWrapper.Employee.SearchEmployee(term);
            return Ok(empList);           
        }

        [HttpPost("searchemployee")]
        public async Task<ActionResult<IEnumerable<EmployeeRequest>>>  SearchEmployeeMultiple(EmployeeResult SearchObj)
        {
            var empList = await _repositoryWrapper.Employee.SearchEmployeeMultiple(SearchObj);
            return Ok(empList);           
        }

        private bool EmployeeExists(long id)
        {
            return _repositoryWrapper.Employee.IsExists(id);
        }

         private static EmployeeRequest EmployeeToDTO(Employee employee) =>
            new EmployeeRequest
            {
                Id = employee.Id,
                EmployeeName = employee.EmployeeName,
                EmployeeAddress = employee.EmployeeAddress,
                EmpDepartmentId = employee.EmpDepartmentId,
            };
    }
}
