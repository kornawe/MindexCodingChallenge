using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface IReportingStructureRepository
    {
        /// <summary>
        ///     Gets a <see cref="ReportingStructure"/> for a specific <paramref name="employeeId"/>
        /// </summary>
        /// <param name="employeeId">
        ///     Employee id
        /// </param>
        /// <returns>
        ///     Found reporting structure, or null.
        /// </returns>
        ReportingStructure GetByEmployeeId(String employeeId);
    }
}