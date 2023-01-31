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
        private List<BaseCalculator> calculators;

        public SalaryHandlerFactory()
        {
            calculators = new List<BaseCalculator>();
        }

        public BaseCalculator GetCalculator(Employee employee)
        {
            var calculator = calculators.FirstOrDefault(m => m.TargetType == (EmployeeTypes)employee.EmployeeTypeId);
            if (calculator == null)
            {
                throw new NotImplementedException($"No registered calculator for {employee.EmployeeType}");
            }

            return calculator;
        }

        public void RegisterCalculator(BaseCalculator calculator)
        {
            if (calculators.Any(m => m.TargetType == calculator.TargetType))
            {
                throw new InvalidOperationException($"There is already a calculator for {calculator.TargetType}");
            }

            calculators.Add(calculator);
        }
    }
}
