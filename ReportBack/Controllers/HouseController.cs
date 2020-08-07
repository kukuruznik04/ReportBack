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
    [Route("house")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly DataContext _context;

        public HouseController(DataContext dataContext)
        {
            this._context = dataContext;
        }

        // GET: house
        [HttpGet]
        public async Task<ActionResult<IEnumerable<House>>> Get()
        {
            return await _context.Houses.ToListAsync();
        }

        // GET house/5
        [HttpGet("{id}")]
        public async Task<ActionResult<House>> Get(Guid id)
        {
            var item = await _context.Houses.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST api/house
        [HttpPost]
        public async Task<ActionResult<House>> Post(House house)
        {
            _context.Houses.Add(house);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { id = house.Id }
                );
        }

        // PUT api/house/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, House houseNew)
        {
            //if (id != houseDTO.Id)
            //{
            //    return BadRequest();
            //}

            var house = await _context.Houses.FindAsync(id);
            if (house == null)
            {
                return NotFound();
            }

            house.Name = houseNew.Name;

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

        // DELETE api/<HouseController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var house = await _context.Houses.FindAsync(id);

            if (house == null)
            {
                return NotFound();
            }

            _context.Houses.Remove(house);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(Guid id) =>
         _context.Houses.Any(e => e.Id == id);
    }
}
