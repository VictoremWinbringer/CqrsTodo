using CqrsTodo.EF;
using CqrsTodo.Models;
using CqrsTodo.Query.Concrete;
using CqrsTodo.Query.Handler.Abstract;
using System.Threading.Tasks;

namespace CqrsTodo.Query.Handler.Concrete
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
