using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.Command;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Data;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Business.Tests.Command
{
    public class AddEmployeeCommandTest
    {
        [Fact]
        public async Task ExecuteAsync_ShouldAddEmployeeToDatabase()
        {
            // Arrange
            using var context = Common.CreateInMemoryContext(nameof(ExecuteAsync_ShouldAddEmployeeToDatabase));

            var input = new CreateEmployeeDto
            {
                Birthdate = new DateTime(1990, 01, 01),
                FullName = "John Doe",
                Tin = "123456789",
                TypeId = 1,
            };

            var command = new AddEmployeeCommand(context);

            // Act
            var employeeId = await command.ExecuteAsync(input, CancellationToken.None);

            // Assert
            var employee = await context.Employee.FindAsync(employeeId);
            Assert.NotNull(employee);
            Assert.Equal(employeeId, employee.Id);
            Assert.Equal(input.Birthdate, employee.Birthdate);
            Assert.Equal(input.FullName, employee.FullName);
            Assert.Equal(input.Tin, employee.TIN);
            Assert.Equal(input.TypeId, employee.EmployeeTypeId);
            Assert.False(employee.IsDeleted);
        }
    }
}
