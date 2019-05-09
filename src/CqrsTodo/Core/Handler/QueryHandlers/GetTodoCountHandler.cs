using System.Threading.Tasks;
using CqrsTodo.EF;
using CqrsTodo.Query.Concrete;
using Microsoft.EntityFrameworkCore;

namespace CqrsTodo.Handler.QueryHandlers
{
    internal sealed class GetTodoCountHandler : IQueryHandler<GetTodoCount, Task<int>>
    {
        private readonly TodoContext _context;

        public GetTodoCountHandler(TodoContext context)
        {
            _context = context;
        }
        public async Task<int> Execute(GetTodoCount query)
        {
            return await _context.Todos.CountAsync();
        }
    }
}
