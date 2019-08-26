using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.DTO;
using webapi.Helpers;
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

        // GET: api/radios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Radio>>> GetRadios()
        {
            var helper = new RadioHelper(_context);
            await helper.GetLocations();
            return await _context.Radios.ToListAsync();
        }

        // GET: api/radios/5
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


        // GET: api/radios/id/location
        [HttpGet("{id}/location")]
        public async Task<ActionResult<RadioDTO>> GetRadioLocation(int id)
        {
            var radio = await _context.Radios.FindAsync(id);

            if (radio == null)
            {
                return NotFound();
            }

            var radioDTO = new RadioDTO(){ location = radio.location };
            return radioDTO;
        }

        // POST: api/radios
        [HttpPost]
        public async Task<ActionResult<Radio>> PostRadio(Radio radio)
        {
            try
            {

                foreach (var item in radio.allowed_locations)
            {
                _context.AllowedLocations.Add(item);
            }
            _context.Radios.Add(radio);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRadio), new { id = radio.id }, radio);
            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/radios/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRadio(int id, Radio radio)
        {
            var helper = new RadioHelper(_context);
            var isLocationAllowed = helper.IsLocationAllowed(radio.location);
            if (id != radio.id)
            {
                return BadRequest();
            }
            
            if(await isLocationAllowed)
            {
                _context.Entry(radio).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            return Forbid();
        }
    }
}
