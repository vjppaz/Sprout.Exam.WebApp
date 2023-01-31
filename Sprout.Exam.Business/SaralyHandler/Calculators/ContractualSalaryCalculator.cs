using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.SaralyHandler.Salaries
{
    public class ContractualSalaryCalculator : BaseCalculator
    {
        private const decimal dailyRate = 500;

        public ContractualSalaryCalculator() : base(EmployeeTypes.Contractual)
        {
        }

        public override decimal Calculate(Employee employee, SalaryCalculatorArgument argument)
        {
            var salary = dailyRate * argument.WorkedDays;
            return Math.Round(salary, 2);
        }
    }
}
