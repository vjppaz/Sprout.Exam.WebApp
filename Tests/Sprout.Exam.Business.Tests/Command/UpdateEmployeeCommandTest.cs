using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.Command;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common;
using Sprout.Exam.Common.Models;
using Sprout.Exam.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Business.Tests.Command
{
    public class UpdateEmployeeCommandTest
    {
        [Fact]
        public async Task UpdateEmployeeCommand_SuccessfulUpdate_ReturnsExpectedResult()
        {
            // Arrange
            var context = Common.CreateInMemoryContext(nameof(UpdateEmployeeCommand_SuccessfulUpdate_ReturnsExpectedResult));
            var command = new UpdateEmployeeCommand(context);

            var employee = new Employee
            {
                Id = 1,
                FullName = "John Doe",
                TIN = "123456789",
                Birthdate = new DateTime(1980, 01, 01),
                EmployeeTypeId = 1
            };
            await context.Employee.AddAsync(employee);
            await context.SaveChangesAsync();

            var input = new EditEmployeeDto
            {
                Id = 1,
                FullName = "Jane Doe",
                Tin = "987654321",
                Birthdate = new DateTime(1981, 01, 01),
                TypeId = 2
            };
            var cancellationToken = CancellationToken.None;

            // Act
            var result = await command.ExecuteAsync(input, cancellationToken);

            // Assert
            Assert.Equal(input.Id, result.Id);
            Assert.Equal(input.FullName, result.FullName);
            Assert.Equal(input.Tin, result.Tin);
            Assert.Equal(input.Birthdate.ToString(SystemConstants.DefaultDateFormat), result.Birthdate);
            Assert.Equal(input.TypeId, result.TypeId);
        }

        [Fact]
        public async Task UpdateEmployeeCommand_EmployeeNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var context = Common.CreateInMemoryContext(nameof(UpdateEmployeeCommand_EmployeeNotFound_ThrowsKeyNotFoundException));
            var command = new UpdateEmployeeCommand(context);

            var input = new EditEmployeeDto
            {
                Id = 1,
                FullName = "Jane Doe",
                Tin = "987654321",
                Birthdate = new DateTime(1981, 01, 01),
                TypeId = 2
            };
            var cancellationToken = new CancellationToken();

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => command.ExecuteAsync(input, cancellationToken));
        }

        [Fact]
        public async Task UpdateEmployeeCommand_NullInput_ThrowsArgumentNullException()
        {
            // Arrange
            var context = Common.CreateInMemoryContext(nameof(UpdateEmployeeCommand_NullInput_ThrowsArgumentNullException));
            var command = new UpdateEmployeeCommand(context);

            EditEmployeeDto input = null;
            var cancellationToken = new CancellationToken();

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => command.ExecuteAsync(input, cancellationToken));
        }
    }
}
