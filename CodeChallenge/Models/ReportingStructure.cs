namespace CodeChallenge.Models
{
    public class ReportingStructure
    {

        public string EmployeeId { get; set; }

        /// <summary>
        /// The recursive total of all emplyees reporting to the <see cref="Employee"/>
        /// with id = <see cref="EmployeeId"/>. 
        /// 
        /// 0 if <see cref="EmployeeId"/> is not valid.
        /// </summary>
        public int NumberOfReports  { get; set; }
    }
}
