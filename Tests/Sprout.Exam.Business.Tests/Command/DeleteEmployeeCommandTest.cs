using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.Command;
using Sprout.Exam.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Business.Tests.Command
{
    public class DeleteEmployeeCommandTest
    {
        [Fact]
        public async Task DeleteEmployeeCommand_DeletesEmployee_WhenEmployeeExists()
        {
            // Arrange
            using var context = Common.CreateInMemoryContext(nameof(DeleteEmployeeCommand_DeletesEmployee_WhenEmployeeExists));
            var employee = new Employee
            {
                Id = 1,
                IsDeleted = false,
            };
            context.Employee.Add(employee);
            await context.SaveChangesAsync();
            var command = new DeleteEmployeeCommand(context);

            // Act
            await command.ExecuteAsync(employee.Id, CancellationToken.None);

            // Assert
            var deletedEmployee = await context.Employee.FirstOrDefaultAsync(m => m.Id == employee.Id);
            Assert.True(deletedEmployee.IsDeleted);
        }

        [Fact]
        public async Task DeleteEmployeeCommand_ThrowsKeyNotFoundException_WhenEmployeeDoesNotExist()
        {
            // Arrange
            using var context = Common.CreateInMemoryContext(nameof(DeleteEmployeeCommand_ThrowsKeyNotFoundException_WhenEmployeeDoesNotExist));
            var command = new DeleteEmployeeCommand(context);

            // Act and Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => command.ExecuteAsync(1, CancellationToken.None));
        }
    }
}
