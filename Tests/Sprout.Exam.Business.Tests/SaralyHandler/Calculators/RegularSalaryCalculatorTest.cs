using Sprout.Exam.Business.SaralyHandler.Computations;
using Sprout.Exam.Business.SaralyHandler.Salaries;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Business.Tests.SaralyHandler.Calculators
{
    public class RegularSalaryCalculatorTest
    {
        [Fact]
        public void RegularSalaryCalculator_CalculatesSalary_WithCorrectValue()
        {
            // Arrange
            var calculator = new RegularSalaryCalculator();
            var employee = new Employee { Id = 1, FullName = "John Doe", EmployeeTypeId = (int)EmployeeTypes.Regular };
            var argument = new SalaryCalculatorArgument(0, 0);

            // Act
            var salary = calculator.Calculate(employee, argument);

            // Assert
            Assert.Equal(17600, salary);
        }

        [Fact]
        public void RegularSalaryCalculator_CalculatesSalary_WithAbsentDays()
        {
            // Arrange
            var calculator = new RegularSalaryCalculator();
            var employee = new Employee { Id = 1, FullName = "John Doe", EmployeeTypeId = (int)EmployeeTypes.Regular };
            var argument = new SalaryCalculatorArgument(2, 0);

            // Act
            var salary = calculator.Calculate(employee, argument);

            // Assert
            Assert.Equal(15781.82m, salary);
        }
    }
}
