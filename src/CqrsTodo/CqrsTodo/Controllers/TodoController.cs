using CqrsTodo.EF;
using CqrsTodo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using CqrsTodo.Filters;
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

        public ToDoController(TodoContext context, IHubContext<Notifier> notifier)
        {
            _context = context;
            _notifier = notifier;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Todos.ToList());
        }

        [HttpGet("{id}")]
        [ValidateTodoExists]
        public IActionResult Get(Guid id)
        {
            return Ok(_context.Todos.Find(id));
        }


        [HttpPost]
        public IActionResult Post([FromBody]Todo value)
        {
            value.Id = Guid.NewGuid();

            value.IsComplete = false;

            _context.Todos.Add(value);

            _context.SaveChanges();

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
