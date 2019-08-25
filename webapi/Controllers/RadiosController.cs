using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Controllers
{
    [Route("api/radios")]
    [ApiController]
    public class RadioController : ControllerBase
    {
        private readonly WebApiContext _context;

        public RadioController(WebApiContext context)
        {
            _context = context;

            if (_context.Radios.Count() == 0)
            {
                // Create a new Radio if collection is empty,
                // in this way we ensure not deleting all Radios.
                _context.Radios.Add(new Radio { alias = "Radio1" });
                _context.SaveChanges();
            }
        }

        // GET: api/radio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Radio>>> GetRadios()
        {
            return await _context.Radios.ToListAsync();
        }

        // GET: api/radio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Radio>> GetRadio(int id)
        {
            var radio = await _context.Radios.FindAsync(id);

            if (radio == null)
            {
                return NotFound();
            }

            return radio;
        }

        // POST: api/radio
        [HttpPost]
        public async Task<ActionResult<Radio>> PostRadio(Radio radio)
        {

            _context.Radios.Add(radio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRadio), new { id = radio.id }, radio);
        }

        // PUT: api/radio/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRadio(int id, Radio radio)
        {
            if (id != radio.id)
            {
                return BadRequest();
            }

            _context.Entry(radio).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
