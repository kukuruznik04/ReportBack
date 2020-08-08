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
    [Route("report")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly DataContext _context;

        public ReportController(DataContext dataContext)
        {
            this._context = dataContext;
        }
        // GET: report
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> Get()
        {
            return await _context.Reports
                .Include(s => s.Paragraph1).ThenInclude(p => p.Planet)
                .Include(s => s.Paragraph1).ThenInclude(p => p.House)
                .Include(s => s.Paragraph1).ThenInclude(p => p.Sign)
                .Include(s => s.Paragraph2).ThenInclude(p => p.Planet)
                .Include(s => s.Paragraph2).ThenInclude(p => p.House)
                .Include(s => s.Paragraph2).ThenInclude(p => p.Sign)
                .Include(s => s.Paragraph3).ThenInclude(p => p.Planet)
                .Include(s => s.Paragraph3).ThenInclude(p => p.House)
                .Include(s => s.Paragraph3).ThenInclude(p => p.Sign)
                .Include(s => s.Paragraph4).ThenInclude(p => p.Planet)
                .Include(s => s.Paragraph4).ThenInclude(p => p.House)
                .Include(s => s.Paragraph4).ThenInclude(p => p.Sign)
                .Include(s => s.Paragraph5).ThenInclude(p => p.Planet)
                .Include(s => s.Paragraph5).ThenInclude(p => p.House)
                .Include(s => s.Paragraph5).ThenInclude(p => p.Sign)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET report/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> Get(Guid id)
        {
            var item = await _context.Reports
                .Include(s => s.Paragraph1).ThenInclude(p => p.Planet)
                .Include(s => s.Paragraph1).ThenInclude(p => p.House)
                .Include(s => s.Paragraph1).ThenInclude(p => p.Sign)
                .Include(s => s.Paragraph2).ThenInclude(p => p.Planet)
                .Include(s => s.Paragraph2).ThenInclude(p => p.House)
                .Include(s => s.Paragraph2).ThenInclude(p => p.Sign)
                .Include(s => s.Paragraph3).ThenInclude(p => p.Planet)
                .Include(s => s.Paragraph3).ThenInclude(p => p.House)
                .Include(s => s.Paragraph3).ThenInclude(p => p.Sign)
                .Include(s => s.Paragraph4).ThenInclude(p => p.Planet)
                .Include(s => s.Paragraph4).ThenInclude(p => p.House)
                .Include(s => s.Paragraph4).ThenInclude(p => p.Sign)
                .Include(s => s.Paragraph5).ThenInclude(p => p.Planet)
                .Include(s => s.Paragraph5).ThenInclude(p => p.House)
                .Include(s => s.Paragraph5).ThenInclude(p => p.Sign)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return item;
        }

        // POST report
        [HttpPost]
        public async Task<ActionResult<Report>> Post([FromBody] ReportDTO reportDTO)
        {
            var report = new Report()
            {
                Paragraph1 = await _context.Paragraphs.FindAsync(reportDTO.Paragraph1Id),
                Paragraph2 = await _context.Paragraphs.FindAsync(reportDTO.Paragraph2Id),
                Paragraph3 = await _context.Paragraphs.FindAsync(reportDTO.Paragraph3Id),
                Paragraph4 = await _context.Paragraphs.FindAsync(reportDTO.Paragraph4Id),
                Paragraph5 = await _context.Paragraphs.FindAsync(reportDTO.Paragraph5Id)
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new
                {
                    id = report.Id
                }
                );
        }

        // PUT report/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] ReportDTO reportDTO)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            report.Paragraph1 = await _context.Paragraphs.FindAsync(reportDTO.Paragraph1Id);
            report.Paragraph2 = await _context.Paragraphs.FindAsync(reportDTO.Paragraph2Id);
            report.Paragraph3 = await _context.Paragraphs.FindAsync(reportDTO.Paragraph3Id);
            report.Paragraph4 = await _context.Paragraphs.FindAsync(reportDTO.Paragraph4Id);
            report.Paragraph5 = await _context.Paragraphs.FindAsync(reportDTO.Paragraph5Id);

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

        // DELETE report/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemExists(Guid id) =>
         _context.Reports.Any(e => e.Id == id);
    }
}
