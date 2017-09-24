using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CqrsTodo.Command.Abstract;

namespace CqrsTodo.Command.Handler.Abstract
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Execute(TCommand command);
    }
}
