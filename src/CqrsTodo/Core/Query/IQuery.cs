using System.Threading.Tasks;

namespace CqrsTodo.Query
{
    public interface IQuery<out TResult> where TResult : Task
    {
    }
}
