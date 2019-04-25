using System;

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
