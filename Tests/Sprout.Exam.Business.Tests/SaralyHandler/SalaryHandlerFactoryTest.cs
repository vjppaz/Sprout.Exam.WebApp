using Sprout.Exam.Business.SaralyHandler.Salaries;
using Sprout.Exam.Business.SaralyHandler;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Sprout.Exam.Business.SaralyHandler.Computations;

namespace Sprout.Exam.Business.Tests.SaralyHandler
{
    public class SalaryHandlerFactoryTest
    {
        private SalaryHandlerFactory factory;
        private ISalaryCalculator calculator1;
        private ISalaryCalculator calculator2;

        [Fact]
        public void GetCalculator_ReturnsCalculator_WhenEmployeeTypeMatches()
        {
            // Arrange
            calculator1 = new ContractualSalaryCalculator();
            calculator2 = new RegularSalaryCalculator();
            factory = new SalaryHandlerFactory();
            factory.RegisterCalculator(calculator1);
            factory.RegisterCalculator(calculator2);

            var employee = new Employee { EmployeeTypeId = (int)EmployeeTypes.Contractual };

            // Act
            var result = factory.GetCalculator(employee);

            // Assert
            Assert.Equal(calculator1, result);
        }

        [Fact]
        public void GetCalculator_ThrowsNotImplementedException_WhenEmployeeTypeDoesNotMatch()
        {
            // Arrange
            calculator1 = new ContractualSalaryCalculator();
            factory = new SalaryHandlerFactory();
            factory.RegisterCalculator(calculator1);

            var employee = new Employee { EmployeeTypeId = (int)EmployeeTypes.Regular };

            // Act and Assert
            var ex = Assert.Throws<NotImplementedException>(() => factory.GetCalculator(employee));
            Assert.Equal("No registered calculator for Regular", ex.Message);
        }

        [Fact]
        public void RegisterCalculator_ThrowsInvalidOperationException_WhenCalculatorForEmployeeTypeAlreadyExists()
        {
            // Arrange
            calculator1 = new ContractualSalaryCalculator();
            factory = new SalaryHandlerFactory();
            factory.RegisterCalculator(calculator1);

            // Act and Assert
            var ex = Assert.Throws<InvalidOperationException>(() => factory.RegisterCalculator(calculator1));
            Assert.Equal("There is already a calculator for Contractual", ex.Message);
        }
    }
}
