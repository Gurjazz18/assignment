using assignment.Data.Data;
using assignment.Models.Entity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace assignment.Services
{
    public class EmployeeService:IEmployeeService
    {
         private readonly AppDbContext dbcontext;
        public EmployeeService(AppDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return await dbcontext.Employees.ToListAsync();
        }

        public async Task<Employee?> GetEmployeeById(int id)
        {
            return await dbcontext.Employees.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
             dbcontext.Employees.Add(employee);
            await dbcontext.SaveChangesAsync();
            return employee;
        }

        public async Task<Employee?> UpdateEmployee(int id, Employee employee)
        {
            var existingEmployee = await dbcontext.Employees.FindAsync(id);
            if (existingEmployee == null) return null;

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.Email = employee.Email;
            existingEmployee.DateOfBirth = employee.DateOfBirth;
            existingEmployee.Position = employee.Position;
            existingEmployee.Salary = employee.Salary;
            await dbcontext.SaveChangesAsync();
            return existingEmployee;

        }

        public async Task<Employee?> DeleteEmployee(int id)
        {
            var ExistEmp = await dbcontext.Employees.FindAsync(id);
            if (ExistEmp == null) return null;

            dbcontext.Employees.Remove(ExistEmp);
            await dbcontext.SaveChangesAsync();
            return ExistEmp;

        }
    }
}
