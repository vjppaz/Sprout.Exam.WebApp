using Sprout.Exam.Business.Query;
using Sprout.Exam.Common;
using Sprout.Exam.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Business.Tests.Query
{
    public class GetEmployeeByIdQueryTest
    {
        [Fact]
        public async Task GetEmployeeByIdQuery_WhenCalled_ReturnsCorrectEmployee()
        {
            // Arrange
            var context = Common.CreateInMemoryContext(nameof(GetEmployeeByIdQuery_WhenCalled_ReturnsCorrectEmployee));
            int expectedId = 1;
            var expectedTin = "TIN1234567";
            var expectedFullName = "John Doe";
            var expectedBirthdate = new DateTime(2000, 1, 1).ToString(SystemConstants.DefaultDateFormat);
            int expectedTypeId = 1;

            context.Add(new Employee
            {
                Id = expectedId,
                TIN = expectedTin,
                FullName = expectedFullName,
                Birthdate = new DateTime(2000, 1, 1),
                EmployeeTypeId = expectedTypeId,
            });
            await context.SaveChangesAsync(CancellationToken.None);

            // Act
            var getEmployeeByIdQuery = new GetEmployeeByIdQuery(context);
            var employee = await getEmployeeByIdQuery.ExecuteAsync(expectedId, CancellationToken.None);

            // Assert
            Assert.Equal(expectedId, employee.Id);
            Assert.Equal(expectedTin, employee.Tin);
            Assert.Equal(expectedFullName, employee.FullName);
            Assert.Equal(expectedBirthdate, employee.Birthdate);
            Assert.Equal(expectedTypeId, employee.TypeId);
        }

        [Fact]
        public async Task GetEmployeeByIdQuery_WhenCalledWithInvalidId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var context = Common.CreateInMemoryContext(nameof(GetEmployeeByIdQuery_WhenCalledWithInvalidId_ThrowsKeyNotFoundException));
            var getEmployeeByIdQuery = new GetEmployeeByIdQuery(context);
            int invalidId = 999;

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => getEmployeeByIdQuery.ExecuteAsync(invalidId, CancellationToken.None));
        }
    }
}
