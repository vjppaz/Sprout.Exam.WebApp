using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.Interfaces.Command;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Models;
using Sprout.Exam.WebApp.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Command
{
    public class AddEmployeeCommand : IAddEmployeeCommand
    {
        private readonly ApplicationDbContext context;

        public AddEmployeeCommand(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<int> ExecuteAsync(CreateEmployeeDto input, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Birthdate = input.Birthdate,
                EmployeeTypeId = input.TypeId,
                FullName = input.FullName,
                IsDeleted = false,
                TIN = input.Tin,
            };

            this.context.Add(employee);
            await this.context.SaveChangesAsync(cancellationToken);

            return employee.Id;
        }
    }
}
