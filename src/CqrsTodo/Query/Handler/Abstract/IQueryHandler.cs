using CqrsTodo.Query.Abstract;
using System.Threading.Tasks;

namespace CqrsTodo.Query.Handler.Abstract
{
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult> where TResult : Task
    {
        TResult Execute(TQuery query);
    }
}
