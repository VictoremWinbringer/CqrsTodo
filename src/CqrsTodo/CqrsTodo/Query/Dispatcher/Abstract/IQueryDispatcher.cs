using CqrsTodo.Query.Abstract;
using System.Threading.Tasks;

namespace CqrsTodo.Query.Dispatcher.Abstract
{
    public interface IQueryDispatcher
    {
        TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult> where TResult : Task;
    }
}
