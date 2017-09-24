using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CqrsTodo.Query.Abstract
{
    public interface IQuery<out TResult> where TResult : Task
    {
    }
}
