using CqrsTodo.Command.Concrete;
using CqrsTodo.Command.Handler.Abstract;
using CqrsTodo.EF;
using System.Threading.Tasks;

namespace CqrsTodo.Command.Handler.Concrete
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
