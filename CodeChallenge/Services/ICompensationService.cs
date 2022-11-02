using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        /// <summary>
        ///     Attempts to get a <see cref="Compensation"/> from a given
        ///     <paramref name="employeeId"/>
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns>
        ///     The found object, or null.
        /// </returns>
        Compensation GetByEmployeeId(String employeeId);

        /// <summary>
        ///     Creates record for a <see cref="Compensation"/>.
        /// </summary>
        /// <param name="compensation"></param>
        /// <returns>
        ///     The created compensation, or null if 
        ///     <paramref name="compensation"/> was invalid.
        /// </returns>
        Compensation Create(Compensation compensation);
    }
}
