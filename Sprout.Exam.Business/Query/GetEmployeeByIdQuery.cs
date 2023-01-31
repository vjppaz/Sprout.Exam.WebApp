using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces.Query;
using Sprout.Exam.Common;
using Sprout.Exam.Common.Models;
using Sprout.Exam.WebApp.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Query
{
    public class GetEmployeeByIdQuery : IGetEmployeeByIdQuery
    {
        private readonly ApplicationDbContext context;

        public GetEmployeeByIdQuery(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<EmployeeDto> ExecuteAsync(int parameter, CancellationToken cancellationToken)
        {
            var employee = await context.Set<Employee>().ActiveOnly().FirstOrDefaultAsync(m => m.Id == parameter);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee {parameter} does not exist.");
            }

            return new EmployeeDto
            {
                Birthdate = employee.Birthdate.ToString(SystemConstants.DefaultDateFormat),
                FullName = employee.FullName,
                Id = employee.Id,
                Tin = employee.TIN,
                TypeId = employee.EmployeeTypeId,
            };
        }
    }
}
