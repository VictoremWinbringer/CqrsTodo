using CqrsTodo.Command.Abstract;
using System;

namespace CqrsTodo.Command.Concrete
{
    public class MakeComplete:ICommand
    {
        public Guid Id { get; }

        public MakeComplete(Guid id)
        {
            Id = id;
        }
    }
}
