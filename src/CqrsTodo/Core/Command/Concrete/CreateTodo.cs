using System;

namespace CqrsTodo.Command.Concrete
{
    internal sealed class CreateTodo : ICommand
    {
        public Guid Id { get; }
        public string Description { get; }

        public CreateTodo(Guid id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}
