using System.Threading.Tasks;
using CqrsTodo.Command.Concrete;
using CqrsTodo.EF;
using CqrsTodo.Models;

namespace CqrsTodo.Handler.CommandHandlers
{
    internal sealed class CreateTodoHandler : ICommandHandler<CreateTodo>
    {
        private readonly TodoContext _context;

        public CreateTodoHandler(TodoContext context)
        {
            _context = context;
        }
        public async Task Execute(CreateTodo command)
        {
            _context.Todos.Add(new Todo
            {
                Id = command.Id,
                Description = command.Description,
                IsComplete = false
            });

            await _context.SaveChangesAsync();
        }
    }
}
