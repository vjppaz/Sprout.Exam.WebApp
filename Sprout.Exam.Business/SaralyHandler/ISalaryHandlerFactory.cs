using Sprout.Exam.Business.SaralyHandler.Salaries;
using Sprout.Exam.Common.Models;

namespace Sprout.Exam.Business.SaralyHandler
{
    public interface ISalaryHandlerFactory
    {
        void RegisterCalculator(BaseCalculator calculator);
        BaseCalculator GetCalculator(Employee employee);
    }
}
