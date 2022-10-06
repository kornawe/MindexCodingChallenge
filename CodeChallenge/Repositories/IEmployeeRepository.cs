using CodeChallenge.Models;
using System;
using System.Threading.Tasks;

namespace CodeChallenge.Repositories
{
    public interface IEmployeeRepository
    {
        /// <summary>
        ///     Gets an employee from a given <paramref name="id"/>,
        ///     but does not load the <see cref="Employee.DirectReports"/>.
        /// </summary>
        /// <param name="id">
        ///     Employee id
        /// </param>
        /// <returns>
        ///     Found employee, or null.
        /// </returns>
        Employee GetById(String id);
        /// <summary>
        ///     Gets an employee from a given <paramref name="id"/>,
        ///     and loads the <see cref="Employee.DirectReports"/>.
        /// </summary>
        /// <param name="id">
        ///     Employee id
        /// </param>
        /// <returns>
        ///     Found employee, or null.
        /// </returns>
        /// <remarks>
        ///     Must slower than <see cref="GetById(string)"/>. Use <see cref="GetById(string)"/>
        ///     if access to <see cref="Employee.DirectReports"/> is not critical.
        /// </remarks>
        Employee GetEmplyeeAndReportsById(String id);
        Employee Add(Employee employee);
        Employee Remove(Employee employee);
        Task SaveAsync();
    }
}