using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Authorize(Roles = "Admin,Staff")]
    [Route("api/parts")]
    [ApiController]
    public class PartsController : ControllerBase
    {
        private readonly IPartService _service;

        public PartsController(IPartService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var parts = await _service.GetAllAsync();
            return Ok(parts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var part = await _service.GetByIdAsync(id);
            if (part == null)
                return NotFound(new { message = "Part not found." });

            return Ok(part);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePartDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.PartName))
                return BadRequest(new { message = "Part name is required." });

            if (dto.Price <= 0)
                return BadRequest(new { message = "Price must be greater than zero." });

            if (dto.StockQuantity < 0)
                return BadRequest(new { message = "Stock quantity cannot be negative." });

            var part = await _service.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = part.Id }, new
            {
                message = "Part created successfully.",
                part
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdatePartDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.PartName))
                return BadRequest(new { message = "Part name is required." });

            if (dto.Price <= 0)
                return BadRequest(new { message = "Price must be greater than zero." });

            if (dto.StockQuantity < 0)
                return BadRequest(new { message = "Stock quantity cannot be negative." });

            var part = await _service.UpdateAsync(id, dto);

            if (part == null)
                return NotFound(new { message = "Part not found." });

            return Ok(new
            {
                message = "Part updated successfully.",
                part
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Part not found." });

            return Ok(new { message = "Part deleted successfully." });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStock([FromQuery] int threshold = 10)
        {
            var parts = await _service.GetLowStockPartsAsync(threshold);
            return Ok(parts);
        }
    }
}