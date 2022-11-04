using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.DL.Interfaces;
using CarDealership.Models.Users;

namespace CarDealership.DL.Repositories.MsSQLRepos
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public EmployeeRepository()
        {

        }

        public Task<Employee> CreateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
        public Task<Employee> UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> DeleteEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Employee>> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetEmployeeById(int employeeId)
        {
            throw new NotImplementedException();
        }
    }
}
