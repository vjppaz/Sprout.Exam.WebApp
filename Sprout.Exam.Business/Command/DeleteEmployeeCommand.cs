using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.Interfaces.Command;
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
    public class DeleteEmployeeCommand : IDeleteEmployeeCommand
    {
        private readonly ApplicationDbContext context;

        public DeleteEmployeeCommand(ApplicationDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task ExecuteAsync(int input, CancellationToken cancellationToken)
        {
            var set = this.context.Set<Employee>();
            var employee = await set.ActiveOnly().FirstOrDefaultAsync(m => m.Id == input);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee {input} does not exist.");
            }

            employee.IsDeleted = true;
            await this.context.SaveChangesAsync(cancellationToken);
        }
    }
}
