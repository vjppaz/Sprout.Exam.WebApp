using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Query;
using Sprout.Exam.Common.Models;
using Sprout.Exam.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.Business.Tests.Query
{
    public class GetEmployeesQueryTest
    {
        [Fact]
        public async Task GetEmployeesQuery_ExecuteAsync_ReturnsListOfEmployees()
        {
            // Arrange
            using var context = Common.CreateInMemoryContext(nameof(GetEmployeesQuery_ExecuteAsync_ReturnsListOfEmployees));

            var employee1 = new Employee
            {
                Id = 1,
                Birthdate = DateTime.Now,
                FullName = "John Doe",
                TIN = "123456789",
                EmployeeTypeId = 1
            };

            var employee2 = new Employee
            {
                Id = 2,
                Birthdate = DateTime.Now,
                FullName = "Jane Doe",
                TIN = "987654321",
                EmployeeTypeId = 2
            };

            context.Employee.Add(employee1);
            context.Employee.Add(employee2);
            await context.SaveChangesAsync();

            // Act
            var query = new GetEmployeesQuery(context);
            var result = await query.ExecuteAsync("", CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<EmployeeDto>>(result);
            Assert.Equal(2, result.Count());
        }
    }
}
