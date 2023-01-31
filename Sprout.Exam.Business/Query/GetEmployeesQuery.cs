using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces.Query;
using Sprout.Exam.Common;
using Sprout.Exam.Common.Models;
using Sprout.Exam.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Query
{
    public class GetEmployeesQuery : IGetEmployeesQuery
    {
        private readonly ApplicationDbContext context;

        public GetEmployeesQuery(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<EmployeeDto>> ExecuteAsync(string parameter, CancellationToken cancellationToken)
        {
            var employee = await context.Set<Employee>()
                .ActiveOnly()
                .Select(m => new EmployeeDto
                {
                    Birthdate = m.Birthdate.ToString(SystemConstants.DefaultDateFormat),
                    FullName = m.FullName,
                    Tin = m.TIN,
                    TypeId = m.EmployeeTypeId,
                })
                .ToListAsync(cancellationToken);

            return employee;
        }
    }
}
