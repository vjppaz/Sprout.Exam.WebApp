using Sprout.Exam.Business.SaralyHandler.Salaries;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.SaralyHandler.Computations
{
    public class RegularSalaryCalculator : BaseCalculator
    {
        private const decimal basicMonthlySalary = 20000;
        private const decimal tax = 0.12m;

        public RegularSalaryCalculator() : base(EmployeeTypes.Regular)
        {
        }

        public override decimal Calculate(Employee employee, SalaryCalculatorArgument argument)
        {
            var dailyRate = basicMonthlySalary / 22;
            var absentCost = argument.AbsentDays * dailyRate;
            var taxCost = basicMonthlySalary * tax;

            return basicMonthlySalary - absentCost - taxCost;
        }
    }
}
