using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
    public interface IReportingStructureService
    {
        /// <summary>
        ///     Gets a <see cref="ReportingStructure"/> from a given
        ///     <paramref name="employeeId"/>.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>
        ///     Found <see cref="ReportingStructure"/> or null.
        /// </returns>
        ReportingStructure GetByEmployeeId(String employeeId);
    }
}
