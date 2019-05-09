using System;
using System.Threading.Tasks;
using CqrsTodo.Command;
using CqrsTodo.Exceptions;
using CqrsTodo.Handler;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsTodo.Dispatcher
{
    public interface ICommandDispatcher
    {
        Task Execute<TCommand>(TCommand command) where TCommand : ICommand;
    }
    
    internal sealed class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public Task Execute<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var handler = _serviceProvider.GetService<ICommandHandler<TCommand>>();

            if (handler == null) throw new CommandHandlerNotFoundException(typeof(TCommand));

           return handler.Execute(command);
        }
    }
}
