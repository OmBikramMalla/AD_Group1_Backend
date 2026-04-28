using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplications.Domain.Models;
using WebApplications.Infrastructure.Presistance;

namespace WebApplications.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/parts")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PartsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var parts = await _context.Parts
                .OrderBy(p => p.Id)
                .ToListAsync();

            return Ok(parts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var part = await _context.Parts.FindAsync(id);

            if (part == null)
                return NotFound(new { message = "Part not found." });

            return Ok(part);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Part part)
        {
            if (string.IsNullOrWhiteSpace(part.PartName))
                return BadRequest(new { message = "Part name is required." });

            if (part.Price <= 0)
                return BadRequest(new { message = "Price must be greater than zero." });

            if (part.StockQuantity < 0)
                return BadRequest(new { message = "Stock quantity cannot be negative." });

            _context.Parts.Add(part);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Part created successfully.",
                part
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, Part dto)
        {
            var part = await _context.Parts.FindAsync(id);

            if (part == null)
                return NotFound(new { message = "Part not found." });

            if (string.IsNullOrWhiteSpace(dto.PartName))
                return BadRequest(new { message = "Part name is required." });

            if (dto.Price <= 0)
                return BadRequest(new { message = "Price must be greater than zero." });

            if (dto.StockQuantity < 0)
                return BadRequest(new { message = "Stock quantity cannot be negative." });

            part.PartName = dto.PartName;
            part.Price = dto.Price;
            part.StockQuantity = dto.StockQuantity;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Part updated successfully.",
                part
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var part = await _context.Parts.FindAsync(id);

            if (part == null)
                return NotFound(new { message = "Part not found." });

            _context.Parts.Remove(part);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Part deleted successfully." });
        }
    }
}