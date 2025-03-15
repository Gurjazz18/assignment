using assignment.Data.Data;
using assignment.Models.Entity;
using assignment.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService service;
        public EmployeesController(IEmployeeService service)
        {
            this.service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await service.GetAllEmployees());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {

            var employee=await service.GetEmployeeById(id);
            if (employee == null) { 

              return NotFound("Not found");
            }
            else
            {
                return Ok(employee);
            }
        }

        [HttpPost]

        public async Task<IActionResult> AddEmployee([FromBody] Employee employee)
        {
              var newEmp=await service.AddEmployee(employee);
               return Ok(newEmp);
              
             
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, Employee employee)
        {
            var UpdatedEmployee=await service.UpdateEmployee(id, employee);
            if (UpdatedEmployee == null) { 
              return NotFound();
            }
            else
            {
                return Ok(UpdatedEmployee);
            }
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DelateEmp(int id)
        {
            var DelateEmp=await service.DeleteEmployee(id);

            if (DelateEmp == null) {
                return NotFound("Employee Not Found");
             }
            else
            {
                return Ok(DelateEmp);
            }

        }
    }
}