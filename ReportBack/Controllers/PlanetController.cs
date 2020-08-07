using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportBack.Data;
using ReportBack.Models;

namespace ReportBack.Controllers
{
    [Route("planet")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        private readonly DataContext _context;

        public PlanetController(DataContext dataContext)
        {
            this._context = dataContext;
        }

        // GET: planet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Planet>>> Get()
        {
            return await _context.Planets.ToListAsync();
        }

        // GET planet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Planet>> Get(Guid id)
        {
            var item = await _context.Planets.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST api/planet
        [HttpPost]
        public async Task<ActionResult<Planet>> Post(Planet planet)
        {

            _context.Planets.Add(planet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { id = planet.Id }
                );
        }

        // PUT api/planet/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Planet planetNew)
        {
            //if (id != planetDTO.Id)
            //{
            //    return BadRequest();
            //}

            var planet = await _context.Planets.FindAsync(id);
            if (planet == null)
            {
                return NotFound();
            }

            planet.Name = planetNew.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE api/<PlanetController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var planet = await _context.Planets.FindAsync(id);

            if (planet == null)
            {
                return NotFound();
            }

            _context.Planets.Remove(planet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(Guid id) =>
         _context.Planets.Any(e => e.Id == id);
    }
}
