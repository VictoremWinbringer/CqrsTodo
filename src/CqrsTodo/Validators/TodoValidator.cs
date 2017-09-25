using System.Linq;
using CqrsTodo.EF;
using CqrsTodo.Models;
using FluentValidation;

namespace CqrsTodo.Validators
{
    public class TodoValidator : AbstractValidator<Todo>
    {
        private const int MIN_DESCRIPTION_LENGTH = 3;
        private const int MAX_DESCRIPTION_LENGTH = 255;

        public TodoValidator(TodoContext context)
        {
            RuleFor(t => t).NotNull()
                .WithMessage(string.Format(Properties.Resource.ResourceManager.GetString("IsNullOrEmpty"), nameof(Todo)));

            RuleFor(t => t.Description).NotEmpty()
                .WithMessage(string.Format(Properties.Resource.ResourceManager.GetString("IsNullOrEmpty"), nameof(Todo.Description)));

            RuleFor(t => t.Description).MinimumLength(MIN_DESCRIPTION_LENGTH)
                .WithMessage(string.Format(Properties.Resource.ResourceManager.GetString("MinLength"),
                    nameof(Todo.Description), MIN_DESCRIPTION_LENGTH));

            RuleFor(t => t.Description).MaximumLength(MAX_DESCRIPTION_LENGTH)
                .WithMessage(string.Format(Properties.Resource.ResourceManager.GetString("MaxLength"), nameof(Todo.Description), MAX_DESCRIPTION_LENGTH));

            RuleFor(t => t.Description).Must(t => context.Todos.All(todo => todo.Description != t))
                .WithMessage(string.Format(Properties.Resource.ResourceManager.GetString("NotUnique"), nameof(Todo.Description)));
        }
    }
}
