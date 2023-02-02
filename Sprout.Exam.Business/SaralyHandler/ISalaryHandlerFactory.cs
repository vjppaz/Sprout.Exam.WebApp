using Sprout.Exam.Business.SaralyHandler.Salaries;
using Sprout.Exam.Common.Models;

namespace Sprout.Exam.Business.SaralyHandler
{
    public interface ISalaryHandlerFactory
    {
        void RegisterCalculator(ISalaryCalculator calculator);
        ISalaryCalculator GetCalculator(Employee employee);
    }
}
