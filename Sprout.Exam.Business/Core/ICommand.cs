using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Core
{
    public interface ICommand<TOut, TIn>
    {
        Task<TOut> ExecuteAsync(TIn input, CancellationToken cancellationToken);
    }

    public interface ICommand<TIn>
    {
        Task ExecuteAsync(TIn input, CancellationToken cancellationToken);
    }
}
