using Sprout.Exam.Business.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Interfaces.Command
{
    public record SalaryCalculatorParameter(int EmployeeId, decimal AbsentDays, decimal WorkedDays);

    public interface ICalculateSalaryCommand : ISingleQuery<decimal, SalaryCalculatorParameter>
    {
    }
}
