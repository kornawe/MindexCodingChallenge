using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        public Employee GetEmplyeeAndReportsById(String id)
        {
            var employee = _employeeContext.Employees.Where(e => e.EmployeeId == id)
                                                    .Include(e => e.DirectReports)
                                                    .SingleOrDefault();
            if (employee.DirectReports != null)
            {
                // TODO: This is terribly ineffcient, however, I could not find a 
                // better way to load the data recursively without just loading the
                // whole table. I would love to know the 'proper' way to do what I am trying.
                for (int i = 0; i < employee.DirectReports.Count; i++)
                {
                    employee.DirectReports[i] = GetEmplyeeAndReportsById(employee.DirectReports[i].EmployeeId);
                }
            }
            return employee;
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
