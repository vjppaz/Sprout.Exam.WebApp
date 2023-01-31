using Sprout.Exam.Common.Core;
using Sprout.Exam.Common.Enums;
using System;

namespace Sprout.Exam.Common.Models
{
    public class Employee : SproutEntity
    {
        public string FullName { get; set; }
        public DateTime Birthdate { get; set; }
        public string TIN { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public int EmployeeTypeId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
