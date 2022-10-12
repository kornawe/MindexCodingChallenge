namespace CodeChallenge.Models
{
    public class ReportingStructure
    {
        /// <summary>
        ///     Related <see cref="Employee"/> of the <see cref="ReportingStructure"/>
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// The recursive total of all employees reporting to the <see cref="Employee"/>
        /// with id = <see cref="EmployeeId"/>. 
        /// 
        /// 0 if <see cref="EmployeeId"/> is not valid.
        /// </summary>
        /// <remarks>
        ///     Recalculated on each call.
        /// </remarks>
        public int NumberOfReports  {
            get
            {
                return AccumulateReportees(Employee);
            }
        }

        /// <summary>
        ///     Recursively calculates the number of reports under a
        ///     given employee.
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
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
