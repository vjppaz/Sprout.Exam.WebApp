using Sprout.Exam.Business.Core;
using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Interfaces.Query
{
    public interface IGetEmployeeByIdQuery : ISingleQuery<EmployeeDto, int>
    {
    }
}
