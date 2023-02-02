using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Models;

namespace Sprout.Exam.Business.SaralyHandler.Salaries
{
    public interface ISalaryCalculator
    {
        EmployeeTypes TargetType { get; }
        decimal Calculate(Employee employee, SalaryCalculatorArgument argument);
    }
}
