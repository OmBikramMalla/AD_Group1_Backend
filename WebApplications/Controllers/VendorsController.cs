using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplications.Application.DTOs;
using WebApplications.Application.Interfaces.IServices;

namespace WebApplications.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/vendors")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorService _vendorService;

        public VendorsController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var vendors = await _vendorService.GetAllAsync();
            return Ok(vendors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var vendor = await _vendorService.GetByIdAsync(id);

            if (vendor == null)
                return NotFound(new { message = "Vendor not found." });

            return Ok(vendor);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateVendorDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vendor = await _vendorService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = vendor.Id }, new
            {
                message = "Vendor created successfully",
                vendor
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, UpdateVendorDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vendor = await _vendorService.UpdateAsync(id, dto);

            if (vendor == null)
                return NotFound(new { message = "Vendor not found." });

            return Ok(new
            {
                message = "Vendor updated successfully",
                vendor
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _vendorService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Vendor not found." });

            return Ok(new { message = "Vendor deleted successfully" });
        }
    }
}