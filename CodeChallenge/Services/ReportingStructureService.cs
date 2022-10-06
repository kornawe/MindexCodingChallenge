using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public ReportingStructure GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                var employee = _employeeRepository.GetEmplyeeAndReportsById(id);
                if (employee == null)
                {
                    return null;
                }
                return new ReportingStructure() {
                    EmployeeId = employee.EmployeeId,
                    NumberOfReports = AccumulateReportees(employee)
                };
            }

            return null;
        }

        /// <summary>
        ///     Recursively calculates the number of reportees under a
        ///     given emplyee.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// <remarks>
        ///     This is very likely not the best place for this logic,
        ///     but I am unsure where the creation logic should go.
        /// </remarks>
        private int AccumulateReportees(Employee employee)
        {
            if (employee == null || employee.DirectReports == null)
            {
                return 0;
            }
            int n = 0;
            foreach (Employee directReport in employee.DirectReports)
            {
                n += AccumulateReportees(directReport);
                n++;
            }
            return n;
        }
    }
}
