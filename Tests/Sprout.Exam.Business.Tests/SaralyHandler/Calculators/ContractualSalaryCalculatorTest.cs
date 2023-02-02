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
    public class ContractualSalaryCalculatorTest
    {
        [Fact]
        public void Calculate_ShouldReturnCorrectSalary()
        {
            // Arrange
            var calculator = new ContractualSalaryCalculator();
            var employee = new Employee { EmployeeTypeId = (int)EmployeeTypes.Contractual };
            var argument = new SalaryCalculatorArgument(0, 22);

            // Act
            var result = calculator.Calculate(employee, argument);

            // Assert
            Assert.Equal(11000, result);
        }

        [Fact]
        public void Calculate_ShouldReturnRoundedSalary()
        {
            // Arrange
            var calculator = new ContractualSalaryCalculator();
            var employee = new Employee { EmployeeTypeId = (int)EmployeeTypes.Contractual };
            var argument = new SalaryCalculatorArgument(0, 22.5m);

            // Act
            var result = calculator.Calculate(employee, argument);

            // Assert
            Assert.Equal(11250, result);
        }
    }
}
