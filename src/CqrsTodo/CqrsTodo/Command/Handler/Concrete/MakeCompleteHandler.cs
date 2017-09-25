using CqrsTodo.Command.Concrete;
using CqrsTodo.Command.Handler.Abstract;
using CqrsTodo.EF;
using System.Threading.Tasks;

namespace CqrsTodo.Command.Handler.Concrete
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

