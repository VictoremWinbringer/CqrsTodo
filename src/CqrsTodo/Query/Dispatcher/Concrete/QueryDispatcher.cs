using CqrsTodo.Query.Abstract;
using CqrsTodo.Query.Dispatcher.Abstract;
using CqrsTodo.Query.Handler.Abstract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using CqrsTodo.Exceptions;

namespace CqrsTodo.Query.Dispatcher.Concrete
{
    internal sealed class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TResult Execute<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult> where  TResult:Task
        {
            if (query == null) throw new ArgumentNullException("query");

            var handler = _serviceProvider.GetService<IQueryHandler<TQuery, TResult>>();

            if (handler == null) throw new QueryHandlerNotFoundException(typeof(TQuery));

            return handler.Execute(query);
        }
    }
}
