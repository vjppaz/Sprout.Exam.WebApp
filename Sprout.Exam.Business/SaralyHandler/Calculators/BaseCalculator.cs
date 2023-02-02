using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.SaralyHandler.Salaries
{
    public record SalaryCalculatorArgument(decimal AbsentDays, decimal WorkedDays);

    public abstract class BaseCalculator : ISalaryCalculator
    {
        public BaseCalculator(EmployeeTypes targetType)
        {
            TargetType = targetType;
        }

        public EmployeeTypes TargetType { get; private set; }
        public abstract decimal Calculate(Employee employee, SalaryCalculatorArgument argument);
    }
}
