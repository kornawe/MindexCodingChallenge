using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _compensationRepository = compensationRepository;
            _logger = logger;
        }

        public Compensation Create(Compensation compensation)
        {
            if (compensation == null)
            {
                _logger.Log(LogLevel.Warning, $"Attempted creation of null {nameof(compensation)}");
                return null;
            }

            // Check that the employee exists.
            var linkedEmployee = _employeeRepository.GetById(compensation.EmployeeId);
            if (linkedEmployee == null)
            {
                _logger.Log(LogLevel.Warning, $"Cannot create {nameof(Compensation)}," +
                    $" linked {nameof(Employee)} with id '{compensation.EmployeeId}' could not be found.");
                return null;
            }

            // If an employee was specified, verify that the employee id is valid
            if (compensation.Employee != null && compensation.Employee.EmployeeId != compensation.EmployeeId)
            {
                _logger.Log(LogLevel.Warning, 
                    $"Cannot create {nameof(Compensation)}, linked {nameof(Employee)} with ID '{compensation.Employee.EmployeeId}'" + 
                    $" does not match {nameof(Compensation.EmployeeId)} '{compensation.EmployeeId}'.");
                return null;
            }

            if (compensation != null)
            {
                _compensationRepository.Add(compensation);
                _compensationRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        public Compensation GetByEmployeeId(string emplyeeId)
        {
            if(!String.IsNullOrEmpty(emplyeeId))
            {
                return _compensationRepository.GetByEmployeeId(emplyeeId);
            }

            return null;
        }
    }
}
