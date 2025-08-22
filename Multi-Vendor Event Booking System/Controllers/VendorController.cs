using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Multi_Vendor_Event_Booking_System.Data.DTO;
using Multi_Vendor_Event_Booking_System.Services;

namespace Multi_Vendor_Event_Booking_System.Controllers
{
    [Route("api/vendors")]
    [ApiController]
    public class VendorController : ControllerBase
    {
        private readonly IVendorService _vendorService;
        public VendorController(IVendorService vendorService)
        {
            _vendorService = vendorService;
        }
        [Route("GetAllVendors")]
        [HttpGet]
        public async Task<IActionResult> GetAllVendorsAsync()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _vendorService.GetAllVendorsAsync();
            return Ok(result);
        }

        [Route("CreateVendor")]
        [HttpPost]
        public async Task<IActionResult> CreateVendor([FromBody] VendorDTO dto)
        {
            
            return await _vendorService.CreateVendorAsync(dto);
        }

        [Route("EditVendor")]
        [HttpPut]
        public async Task<IActionResult> UpdateVendor([FromBody] VendorDTO dto)
        {
            return await _vendorService.UpdateVendorAsync(dto);
        }

        [Route("DeleteVendor/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            return await _vendorService.DeleteVendorAsync(id);
        }

    }
}
