using CqrsTodo.EF;
using CqrsTodo.Query.Concrete;
using CqrsTodo.Query.Handler.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CqrsTodo.Query.Handler.Concrete
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
