using CqrsTodo.Command.Concrete;
using CqrsTodo.Filters;
using CqrsTodo.Models;
using CqrsTodo.Query.Concrete;
using CqrsTodo.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CqrsTodo.Dispatcher;

namespace CqrsTodo.Controllers
{
    [ValidateModel]
    [Route("api/v1/[controller]")]
    public class ToDoController : Controller
    {
        private readonly IHubContext<Notifier> _notifier;
        private readonly ICommandDispatcher _command;
        private readonly IQueryDispatcher _query;

        public ToDoController(IHubContext<Notifier> notifier
            , ICommandDispatcher command
            , IQueryDispatcher query)
        {
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
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _query.Execute<GetTodoById, Task<Todo>>(new GetTodoById(id)));
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Todo value)
        {
            value.Id = Guid.NewGuid();

            var command = new CreateTodo(value.Id, value.Description);

            await _command.Execute(command);

            return Ok(await _query.Execute<GetTodoById, Task<Todo>>(new GetTodoById(value.Id)));
        }

        [HttpPost("{id}/[action]")]
        [ValidateTodoExists]
        public async Task<IActionResult> MakeComplete(Guid id)
        {
            await _command.Execute(new MakeComplete(id));

            var todo = await _query.Execute<GetTodoById, Task<Todo>>(new GetTodoById(id));

            await _notifier.Clients.All.InvokeAsync("Notify", todo.Description + " is complete");

            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Count()
        {
            return Ok(await _query.Execute<GetTodoCount, Task<int>>(new GetTodoCount()));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ValidateTodoExists]
        public async Task<IActionResult> Put(Guid id, [FromBody]Todo value)
        {
            await _command.Execute(new UpdateTodo(id, value.Description));

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _command.Execute(new DeleteTodo(id));

            return Ok();
        }
    }
}
