using CqrsTodo.EF;
using CqrsTodo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CqrsTodo.Command.Concrete;
using CqrsTodo.Command.Dispatcher.Abstract;
using CqrsTodo.Filters;
using CqrsTodo.Query.Concrete;
using CqrsTodo.Query.Dispatcher.Abstract;
using CqrsTodo.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace CqrsTodo.Controllers
{
    [ValidateModel]
    [Route("api/v1/[controller]")]
    public class ToDoController : Controller
    {
        private readonly TodoContext _context;
        private readonly IHubContext<Notifier> _notifier;
        private readonly ICommandDispatcher _command;
        private readonly IQueryDispatcher _query;

        public ToDoController(TodoContext context, IHubContext<Notifier> notifier
            , ICommandDispatcher command, IQueryDispatcher query)
        {
            _context = context;
            _notifier = notifier;
            _command = command;
            _query = query;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _query.Execute<GetAllTodo, Task<IEnumerable<Todo>>>(new GetAllTodo()));
        }

        [HttpGet("{id}")]
        [ValidateTodoExists]
        public IActionResult Get(Guid id)
        {
            return Ok(_context.Todos.Find(id));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Todo value)
        {
            var command = new CreateTodo(Guid.NewGuid(), value.Description);

            value.Id = command.Id;

            await _command.Execute(command);

            return Ok(value);
        }

        [HttpPost("{id}/[action]")]
        [ValidateTodoExists]
        public IActionResult MakeComplete(Guid id)
        {
            var todo = _context.Todos.Find(id);

            todo.IsComplete = true;

            _context.SaveChanges();

            _notifier.Clients.All.InvokeAsync("Notify", todo.Description + " is complete");

            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult Count()
        {
            return Ok(_context.Todos.Count());
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ValidateTodoExists]
        public IActionResult Put(Guid id, [FromBody, /*CustomizeValidator(RuleSet = "Update")*/]Todo value)
        {
            var todo = _context.Todos.Find(id);

            todo.Description = value.Description;

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var todo = _context.Todos.Find(id);

            if (todo != null)
            {
                _context.Todos.Remove(todo);

                _context.SaveChanges();
            }

            return Ok();
        }
    }
}
