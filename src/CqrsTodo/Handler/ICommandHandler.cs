using System.Threading.Tasks;
using CqrsTodo.Command;

namespace CqrsTodo.Handler
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Execute(TCommand command);
    }
}
