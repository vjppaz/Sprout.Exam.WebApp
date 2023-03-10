using Sprout.Exam.Business.SaralyHandler.Salaries;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Models;
using System;

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

            var salary = basicMonthlySalary - absentCost - taxCost;
            return Math.Round(salary, 2);
        }
    }
}
