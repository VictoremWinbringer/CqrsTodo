using System.Threading.Tasks;
using CqrsTodo.Command.Abstract;

namespace CqrsTodo.Command.Dispatcher.Abstract
{
    public interface ICommandDispatcher
    {
        Task Execute<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
