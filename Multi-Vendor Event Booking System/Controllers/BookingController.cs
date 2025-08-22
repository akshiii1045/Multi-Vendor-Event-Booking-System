using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Multi_Vendor_Event_Booking_System.Data.Config;
using Multi_Vendor_Event_Booking_System.Data.DTO;
using Multi_Vendor_Event_Booking_System.Services;

namespace Multi_Vendor_Event_Booking_System.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] BookingDTO dto)
        {
            return await _bookingService.CreateBookingAsync(dto);
        }

        [HttpGet("GetAllBookings")]
        public async Task<IActionResult> GetAllBookings()
        {
            var result = await _bookingService.GetAllBookings();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetByDate")]
        public async Task<IActionResult> GetBookingByDate(string Date)
        {
            return await _bookingService.GetBookingByDate(Date);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetByStatus")]
        public async Task<IActionResult> GetByStatus([FromQuery] BookingStatus status)
        {
            return await _bookingService.GetBookingByStatus(status);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetByVendorID")]
        public async Task<IActionResult> GetBookingByVendorId(int id)
        {
            return await _bookingService.GetBookingByVendor(id);
        }

    }
}