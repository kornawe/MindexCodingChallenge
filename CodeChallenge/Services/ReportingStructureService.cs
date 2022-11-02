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
        private readonly IReportingStructureRepository _reportingStructureRepository;
        private readonly ILogger<ReportingStructureService> _logger;

        public ReportingStructureService(ILogger<ReportingStructureService> logger, IReportingStructureRepository reportingStructureRepository)
        {
            _reportingStructureRepository = reportingStructureRepository;
            _logger = logger;
        }

        /// <inheritdoc/>
        public ReportingStructure GetByEmployeeId(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return null;
            }
            var reportingStructure = _reportingStructureRepository.GetByEmployeeId(id);
            if (reportingStructure == null)
            {
                return null;
            }
            reportingStructure.NumberOfReports = AccumulateReportees(reportingStructure.Employee);
            return reportingStructure;
        }

        /// <summary>
        ///     Recursively calculates the number of reports under a
        ///     given employee.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        private int AccumulateReportees(Employee employee, HashSet<Employee> visitedNodes = null)
        {
            visitedNodes ??= new HashSet<Employee>();
            if (employee == null || employee.DirectReports == null)
            {
                return 0;
            }
            // In the off chance that the reporting structure is circular, don't double count.
            if (visitedNodes.Contains(employee))
            {
                return 0;
            }
            visitedNodes.Add(employee);
            int n = 0;
            foreach (Employee directReport in employee.DirectReports)
            {
                
                n += AccumulateReportees(directReport, visitedNodes);
                n++;
            }
            return n;
        }
    }
}
