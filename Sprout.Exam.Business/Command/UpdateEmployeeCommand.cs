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

        /// <summary>
        /// The execute async method.
        /// </summary>
        /// <param name="input">The data of the employee to be edited.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The updated employee details.</returns>
        /// <exception cref="KeyNotFoundException">Throws when input.Id does not exist in employee table.</exception>
        public async Task<EmployeeDto> ExecuteAsync(EditEmployeeDto input, CancellationToken cancellationToken)
        {
            input = input ?? throw new ArgumentNullException();

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
