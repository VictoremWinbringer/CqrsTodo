using CqrsTodo.EF;
using CqrsTodo.Models;
using CqrsTodo.Query.Concrete;
using CqrsTodo.Query.Handler.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CqrsTodo.Query.Handler.Concrete
{
    internal sealed class GetAllTodoHandler : IQueryHandler<GetAllTodo, Task<IEnumerable<Todo>>>
    {
        private readonly TodoContext _context;

        public GetAllTodoHandler(TodoContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Todo>> Execute(GetAllTodo query)
        {
            return await _context.Todos.ToListAsync();
        }
    }
}
