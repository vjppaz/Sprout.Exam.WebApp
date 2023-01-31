using Sprout.Exam.Business.Core;
using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Interfaces.Command
{
    public interface IUpdateEmployeeCommand : ICommand<EmployeeDto, EditEmployeeDto>
    {

    }
}
