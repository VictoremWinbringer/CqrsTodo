using System;
using System.Threading.Tasks;
using CqrsTodo.Exceptions;
using CqrsTodo.Handler;
using CqrsTodo.Query;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsTodo.Dispatcher
{
    public interface IQueryDispatcher
    {
        TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult> where TResult : Task;
    }

    internal sealed class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult> where TResult : Task
        {
            if (query == null) throw new ArgumentNullException("query");

            var handler = _serviceProvider.GetService<IQueryHandler<TQuery, TResult>>();

            if (handler == null) throw new QueryHandlerNotFoundException(typeof(TQuery));

            return handler.Execute(query);
        }
    }
}