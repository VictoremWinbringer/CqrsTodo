using System.Threading.Tasks;
using CqrsTodo.Command.Concrete;
using CqrsTodo.EF;

namespace CqrsTodo.Handler.CommandHandlers
{
    internal sealed class DeleteTodoHandler : ICommandHandler<DeleteTodo>
    {
        private readonly TodoContext _context;

        public DeleteTodoHandler(TodoContext context)
        {
            _context = context;
        }
        public async Task Execute(DeleteTodo command)
        {
            var todo = await _context.Todos.FindAsync(command.Id);

            if (todo != null)
            {
                _context.Todos.Remove(todo);

                await _context.SaveChangesAsync();
            }
        }
    }
}
