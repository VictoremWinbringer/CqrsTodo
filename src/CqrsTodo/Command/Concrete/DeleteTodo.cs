using System;
using CqrsTodo.Command.Abstract;

namespace CqrsTodo.Command.Concrete
{
    public class DeleteTodo:ICommand
    {
        public Guid Id { get; }

        public DeleteTodo(Guid id)
        {
            Id = id;
        }
    }
}
