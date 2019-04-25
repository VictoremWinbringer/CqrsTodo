using System.Threading.Tasks;
using CqrsTodo.EF;
using CqrsTodo.Models;
using CqrsTodo.Query.Concrete;

namespace CqrsTodo.Handler.QueryHandlers
{
    internal sealed class GetTodoByIdHandler : IQueryHandler<GetTodoById, Task<Todo>>
    {
        private readonly TodoContext _context;

        public GetTodoByIdHandler(TodoContext context)
        {
            _context = context;
        }
        public async Task<Todo> Execute(GetTodoById query)
        {
            return await _context.Todos.FindAsync(query.Id);
        }
    }
}
