using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Business.SaralyHandler.Salaries;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.SaralyHandler
{
    public class SalaryHandlerFactory : ISalaryHandlerFactory
    {
        private List<ISalaryCalculator> calculators;

        public SalaryHandlerFactory()
        {
            calculators = new List<ISalaryCalculator>();
        }

        public ISalaryCalculator GetCalculator(Employee employee)
        {
            var employeeType = (EmployeeTypes)employee.EmployeeTypeId;
            var calculator = calculators.FirstOrDefault(m => m.TargetType == employeeType);
            if (calculator == null)
            {
                throw new NotImplementedException($"No registered calculator for {employeeType}");
            }

            return calculator;
        }

        public void RegisterCalculator(ISalaryCalculator calculator)
        {
            if (calculators.Any(m => m.TargetType == calculator.TargetType))
            {
                throw new InvalidOperationException($"There is already a calculator for {calculator.TargetType}");
            }

            calculators.Add(calculator);
        }
    }
}
