using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CqrsTodo.Command.Concrete;
using CqrsTodo.Command.Handler.Abstract;
using CqrsTodo.EF;
using CqrsTodo.Models;

namespace CqrsTodo.Command.Handler.Concrete
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
