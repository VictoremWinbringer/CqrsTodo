using CqrsTodo.Models;
using CqrsTodo.Query.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CqrsTodo.Query.Concrete
{
    public class GetAllTodo : IQuery<Task<IEnumerable<Todo>>>
    {
    }
}
