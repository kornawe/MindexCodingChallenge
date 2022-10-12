using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        
        public string CompensationId { get; set; }

        public virtual Employee Employee { get; set; }

        [Required]
        public string EmployeeId { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [DataType(DataType.Date)]
        public DateTime EffectiveDate { get; set; }
    }
}
