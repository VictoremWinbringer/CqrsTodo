using System.Threading.Tasks;
using CqrsTodo.Query;

namespace CqrsTodo.Handler
{
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult> where TResult : Task
    {
        TResult Execute(TQuery query);
    }
}
