using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces.Command;
using Sprout.Exam.Common;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Models;
using Sprout.Exam.WebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Command
{
    public class UpdateEmployeeCommand : IUpdateEmployeeCommand
    {
        private readonly ApplicationDbContext context;

        public UpdateEmployeeCommand(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<EmployeeDto> ExecuteAsync(EditEmployeeDto input, CancellationToken cancellationToken)
        {
            var set = context.Set<Employee>();
            var employee = await set.ActiveOnly().FirstOrDefaultAsync(m => m.Id == input.Id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee {input.Id} does not exist.");
            }

            employee.TIN = input.Tin;
            employee.Birthdate = input.Birthdate;
            employee.FullName = input.FullName;
            employee.EmployeeTypeId = input.TypeId;

            await this.context.SaveChangesAsync(cancellationToken);

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
