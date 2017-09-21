using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CqrsTodo.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CqrsTodo.Filters
{
    public sealed class ValidateTodoExistsAttribute : TypeFilterAttribute
    {
        public ValidateTodoExistsAttribute() : base(typeof
            (ValidateTodoExistsFilterImpl))
        {
        }

        private class ValidateTodoExistsFilterImpl : ActionFilterAttribute
        {
            private readonly TodoContext _context;

            public ValidateTodoExistsFilterImpl(TodoContext context)
            {
                _context = context;
            }

            public override void OnActionExecuting(ActionExecutingContext context)
            {
                if (context.ActionArguments.ContainsKey("id"))
                {
                    var id = context.ActionArguments["id"] as Guid?;
                    if (id.HasValue)
                    {
                        if (_context.Todos.Find(id) == null)
                        {
                            context.Result = new NotFoundObjectResult(id.Value);
                        }
                    }
                }
            }
        }
    }
}
