using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportBack.Data;
using ReportBack.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReportBack.Controllers
{
    [Route("paragraph")]
    [ApiController]
    public class ParagraphController : ControllerBase
    {
        private readonly DataContext _context;

        public ParagraphController(DataContext dataContext)
        {
            this._context = dataContext;
        }

        // GET: paragraph
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paragraph>>> Get()
        {
            var a = await _context.Paragraphs
                .Include(s => s.Planet)
                .Include(s => s.House)
                .Include(s => s.Sign)
                .AsNoTracking()
                .ToListAsync();
            return a;
        }

        // GET paragraph/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Paragraph>> Get(Guid id)
        {
            var item = await _context.Paragraphs
                .Include(s => s.Planet)
                .Include(s => s.House)
                .Include(s => s.Sign)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST api/paragraph
        [HttpPost]
        public async Task<ActionResult<Paragraph>> Post([FromBody] ParagraphDTO paragraphDTO)
        {
            var paragraph = new Paragraph(paragraphDTO)
            {
                Planet = await _context.Planets.FindAsync(paragraphDTO.PlanetId),
                House = await _context.Houses.FindAsync(paragraphDTO.HouseId),
                Sign = await _context.Signs.FindAsync(paragraphDTO.SignId)
            };

            _context.Paragraphs.Add(paragraph);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new
                {
                    id = paragraph.Id
                }
                );
        }

        // PUT api/paragraph/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] ParagraphDTO paragraphDTO)
        {
            //if (id != paragraphDTO.Id)
            //{
            //    return BadRequest();
            //}

            var paragraph = await _context.Paragraphs.FindAsync(id);
            if (paragraph == null)
            {
                return NotFound();
            }

            paragraph.Name = paragraphDTO.Name;
            paragraph.Gender = paragraphDTO.Gender;
            paragraph.Age = paragraphDTO.Age;
            paragraph.Planet = await _context.Planets.FindAsync(paragraphDTO.PlanetId);
            paragraph.House = await _context.Houses.FindAsync(paragraphDTO.HouseId);
            paragraph.Sign = await _context.Signs.FindAsync(paragraphDTO.SignId);

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

        // DELETE api/<ParagraphController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var paragraph = await _context.Paragraphs.FindAsync(id);

            if (paragraph == null)
            {
                return NotFound();
            }

            _context.Paragraphs.Remove(paragraph);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(Guid id) =>
         _context.Paragraphs.Any(e => e.Id == id);
    }
}
