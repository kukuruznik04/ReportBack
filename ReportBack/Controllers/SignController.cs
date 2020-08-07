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
    [Route("sign")]
    [ApiController]
    public class SignController : ControllerBase
    {
        private readonly DataContext _context;

        public SignController(DataContext dataContext)
        {
            this._context = dataContext;
        }

        // GET: sign
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sign>>> Get()
        {
            return await _context.Signs.ToListAsync();
        }

        // GET sign/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sign>> Get(Guid id)
        {
            var item = await _context.Signs.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST api/sign
        [HttpPost]
        public async Task<ActionResult<Sign>> Post(Sign sign)
        {
            _context.Signs.Add(sign);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { id = sign.Id }
                );
        }

        // PUT api/sign/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Sign signNew)
        {
            //if (id != signDTO.Id)
            //{
            //    return BadRequest();
            //}

            var sign = await _context.Signs.FindAsync(id);
            if (sign == null)
            {
                return NotFound();
            }

            sign.Name = signNew.Name;

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

        // DELETE api/<SignController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var sign = await _context.Signs.FindAsync(id);

            if (sign == null)
            {
                return NotFound();
            }

            _context.Signs.Remove(sign);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(Guid id) =>
         _context.Signs.Any(e => e.Id == id);
    }
}
