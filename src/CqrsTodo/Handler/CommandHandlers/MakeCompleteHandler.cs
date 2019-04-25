using System.Threading.Tasks;
using CqrsTodo.Command.Concrete;
using CqrsTodo.EF;

namespace CqrsTodo.Handler.CommandHandlers
{
    internal sealed class MakeCompleteHandler : ICommandHandler<MakeComplete>
    {
        private readonly TodoContext _context;

        public MakeCompleteHandler(TodoContext context)
        {
            _context = context;
        }

        public async Task Execute(MakeComplete command)
        {
            var todo = await _context.Todos.FindAsync(command.Id);

            todo.IsComplete = true;

            await _context.SaveChangesAsync();
        }
    }
}

