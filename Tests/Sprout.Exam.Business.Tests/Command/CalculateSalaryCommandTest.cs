using Microsoft.EntityFrameworkCore;
using Moq;
using Sprout.Exam.Business.Command;
using Sprout.Exam.Business.Interfaces.Command;
using Sprout.Exam.Business.SaralyHandler;
using Sprout.Exam.Business.SaralyHandler.Salaries;
using Sprout.Exam.Common.Models;
using Sprout.Exam.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Business.Tests.Command
{
    public class CalculateSalaryCommandTest
    {
        private CalculateSalaryCommand command;
        private Mock<ISalaryHandlerFactory> salaryHandlerFactoryMock;
        private ApplicationDbContext context;
        private Mock<ISalaryCalculator> calculatorMock;

        private void Initialize(string databaseName)
        {
            salaryHandlerFactoryMock = new Mock<ISalaryHandlerFactory>();
            context = Common.CreateInMemoryContext(databaseName);
            calculatorMock = new Mock<ISalaryCalculator>();

            command = new CalculateSalaryCommand(salaryHandlerFactoryMock.Object, context);
        }

        [Fact]
        public async Task ExecuteAsync_GivenEmployeeIdNotExist_ShouldThrowKeyNotFoundException()
        {
            // Arrange
            Initialize(nameof(ExecuteAsync_GivenEmployeeIdNotExist_ShouldThrowKeyNotFoundException));
            var parameter = new SalaryCalculatorParameter(1, 1, 30);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => command.ExecuteAsync(parameter, CancellationToken.None));
            Assert.Equal("Employee 1 does not exist", ex.Message);
        }

        [Fact]
        public async Task ExecuteAsync_GivenEmployeeIdExist_ShouldReturnExpectedSalary()
        {
            // Arrange
            Initialize(nameof(ExecuteAsync_GivenEmployeeIdExist_ShouldReturnExpectedSalary));
            var employee = new Employee { Id = 1, EmployeeTypeId = 1, FullName = "John Doe" };
            context.Add(employee);
            context.SaveChanges();

            var parameter = new SalaryCalculatorParameter(1, 1, 30);
            var args = new SalaryCalculatorArgument(parameter.AbsentDays, parameter.WorkedDays);

            salaryHandlerFactoryMock.Setup(x => x.GetCalculator(It.IsAny<Employee>()))
                .Returns(calculatorMock.Object);

            calculatorMock.Setup(x => x.Calculate(It.IsAny<Employee>(), It.IsAny<SalaryCalculatorArgument>()))
                .Returns(1000);

            // Act
            var result = await command.ExecuteAsync(parameter, CancellationToken.None);

            // Assert
            Assert.Equal(1000, result);
            calculatorMock.Verify(x => x.Calculate(employee, args), Times.Once);
        }
    }
}
