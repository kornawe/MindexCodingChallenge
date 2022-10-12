using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface ICompensationRepository
    {
        /// <summary>
        ///     Attempts to retrieve a <see cref="Compensation"/>
        ///     for a given <paramref name="employeeId"/>.
        /// </summary>
        /// <param name="employeeId">
        /// </param>
        /// <returns>
        ///     Compensation for the employee, if found or
        ///     null if the employee could not be found.
        /// </returns>
        Compensation GetByEmployeeId(String employeeId);

        /// <summary>
        ///     Adds new compensation data, or 
        /// </summary>
        /// <param name="compensation"></param>
        /// <returns></returns>
        Compensation Add(Compensation compensation);
        Compensation Remove(Compensation compensation);
        Task SaveAsync();
    }
}