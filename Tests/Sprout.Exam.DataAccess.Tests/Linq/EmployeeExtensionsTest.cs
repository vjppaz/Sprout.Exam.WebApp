using Sprout.Exam.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sprout.Exam.DataAccess.Tests.Linq
{
    public class EmployeeExtensionsTest
    {
        [Fact]
        public void ActiveOnly_ReturnsOnlyActiveEmployees()
        {
            // Arrange
            var employees = new[]
            {
                new Employee { Id = 1, IsDeleted = false },
                new Employee { Id = 2, IsDeleted = true },
                new Employee { Id = 3, IsDeleted = false },
            }.AsQueryable();

            // Act
            var result = employees.ActiveOnly();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, e => e.Id == 1);
            Assert.Contains(result, e => e.Id == 3);
        }

        [Fact]
        public void ActiveOnly_ReturnsEmptySequence_WhenAllEmployeesAreDeleted()
        {
            // Arrange
            var employees = new[]
            {
                new Employee { Id = 1, IsDeleted = true },
                new Employee { Id = 2, IsDeleted = true },
                new Employee { Id = 3, IsDeleted = true },
            }.AsQueryable();

            // Act
            var result = employees.ActiveOnly();

            // Assert
            Assert.Empty(result);
        }
    }
}
