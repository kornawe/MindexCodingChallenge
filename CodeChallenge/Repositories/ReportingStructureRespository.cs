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
    /// <summary>
    /// Non-persistent repository for <see cref="ReportingStructure"/>.
    /// </summary>
    public class ReportingStructureRepository : IReportingStructureRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public ReportingStructureRepository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        /// <inheritdoc/>
        public ReportingStructure GetByEmployeeId(String employeeId)
        {
            var employee = GetEmployeeById(employeeId);
            if (employee == null)
            {
                return null;
            }

            return new ReportingStructure()
            {
                Employee = employee,
            };
        }

        /// <summary>
        ///     Gets a employee with a fully populated <see cref="Employee.DirectReports"/> tree.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>
        ///     Found employee, or null.
        /// </returns>
        private Employee GetEmployeeById(String employeeId)
        {
            var employee = _employeeContext.Employees.Where(e => e.EmployeeId == employeeId)
                                                    .Include(e => e.DirectReports)
                                                    .SingleOrDefault();
            if (employee == null)
            {
                return null;
            }

            if (employee.DirectReports != null)
            {
                for (int i = 0; i < employee.DirectReports.Count; i++)
                {
                    employee.DirectReports[i] = GetEmployeeById(employee.DirectReports[i].EmployeeId);
                }
            }
            return employee;
        }
    }
}
