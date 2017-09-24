using CqrsTodo.Command.Abstract;
using System;

namespace CqrsTodo.Command.Concrete
{
    public class UpdateTodo:ICommand
    {
        public Guid Id { get; }
        public string Description { get; }

        public UpdateTodo(Guid id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
