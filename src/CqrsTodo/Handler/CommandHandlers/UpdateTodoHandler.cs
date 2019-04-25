using System.Threading.Tasks;
using CqrsTodo.Command.Concrete;
using CqrsTodo.EF;

namespace CqrsTodo.Handler.CommandHandlers
{
    internal sealed class UpdateTodoHandler : ICommandHandler<UpdateTodo>
    {
        private readonly TodoContext _context;

        public UpdateTodoHandler(TodoContext context)
        {
            _context = context;
        }
        public async Task Execute(UpdateTodo command)
        {
            var todo = await _context.Todos.FindAsync(command.Id);

            todo.Description = command.Description;

            await _context.SaveChangesAsync();
        }
    }
}
