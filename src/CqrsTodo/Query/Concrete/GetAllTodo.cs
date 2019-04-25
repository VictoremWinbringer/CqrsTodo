using CqrsTodo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CqrsTodo.Query.Concrete
{
    public class GetAllTodo : IQuery<Task<IEnumerable<Todo>>>
    {
    }
}
