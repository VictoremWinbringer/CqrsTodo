using CqrsTodo.Query.Abstract;
using System.Threading.Tasks;

namespace CqrsTodo.Query.Concrete
{
    public class GetTodoCount:IQuery<Task<int>>
    {
    }
}
