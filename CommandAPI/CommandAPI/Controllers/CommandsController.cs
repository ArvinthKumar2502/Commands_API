using CommandAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace CommandAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly CommandContext _context;

        public CommandsController(CommandContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommandItems()
        {
            return _context.CommandItems;
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandItem(int id)
        {
            var CommandItem = _context.CommandItems.Find(id);

            if (CommandItem == null)
                return NotFound();
            return CommandItem;
        }

        [HttpPost]
        public ActionResult<Command> PostCommandItem(Command command)
        {
            _context.CommandItems.Add(command);
            _context.SaveChanges();

            return CreatedAtAction("GetCommandItem", new Command { Id = command.Id }, command);
        }


        [HttpPut("{id}")]
        public ActionResult<Command> Put(int id, Command command)
        {
            if (id != command.Id)
                return BadRequest();

            _context.Entry(command).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            var commandItem = _context.CommandItems.Find(id);
            return commandItem;
        }


        [HttpDelete("{id}")]
        public ActionResult<Command> Delete(int id)
        {
            var CommandItem = _context.CommandItems.Find(id);

            if (CommandItem == null)
                return NotFound();

            _context.CommandItems.Remove(CommandItem);
            _context.SaveChanges();
            return CommandItem;
        }
    }
}
