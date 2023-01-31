using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Core
{
    public interface IQuery<TEntity, TFilter>
    {
        Task<IEnumerable<TEntity>> ExecuteAsync(TFilter parameter, CancellationToken cancellationToken);
    }

    public interface ISingleQuery<TEntity, TFilter>
    {
        Task<TEntity> ExecuteAsync(TFilter parameter, CancellationToken cancellationToken);
    }
}
