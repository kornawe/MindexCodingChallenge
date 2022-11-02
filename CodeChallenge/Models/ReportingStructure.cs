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
        /// 0 if <see cref="Employee"/> is not valid.
        /// </summary>
        public int NumberOfReports { get; set; }
    }
}
