using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.Interfaces.Command;
using Sprout.Exam.Business.SaralyHandler;
using Sprout.Exam.Business.SaralyHandler.Salaries;
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
    public class CalculateSalaryCommand : ICalculateSalaryCommand
    {
        private readonly ISalaryHandlerFactory salaryHandlerFactory;
        private readonly ApplicationDbContext context;

        public CalculateSalaryCommand(ISalaryHandlerFactory salaryHandlerFactory, ApplicationDbContext context)
        {
            this.salaryHandlerFactory = salaryHandlerFactory;
            this.context = context;
        }

        public async Task<decimal> ExecuteAsync(SalaryCalculatorParameter parameter, CancellationToken cancellationToken)
        {
            var set = context.Set<Employee>();

            var employee = await set.ActiveOnly().FirstOrDefaultAsync(m => m.Id == parameter.EmployeeId);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee {parameter.EmployeeId} does not exist");
            }

            var calculator = salaryHandlerFactory.GetCalculator(employee);

            var args = new SalaryCalculatorArgument(parameter.AbsentDays, parameter.WorkedDays);
            var salary = calculator.Calculate(employee, args);

            return salary;
        }
    }
}
