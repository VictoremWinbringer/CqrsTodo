using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CqrsTodo.Models;

namespace CqrsTodo.Query.Concrete
{
    public class GetTodoById:IQuery<Task<Todo>>
    {
        public Guid Id { get; }

        public GetTodoById(Guid id)
        {
            Id = id;
        }
    }
}
