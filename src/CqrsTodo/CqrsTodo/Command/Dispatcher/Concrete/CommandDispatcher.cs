using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CqrsTodo.Command.Abstract;
using CqrsTodo.Command.Dispatcher.Abstract;
using CqrsTodo.Command.Handler.Abstract;
using CqrsTodo.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace CqrsTodo.Command.Dispatcher.Concrete
{
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
