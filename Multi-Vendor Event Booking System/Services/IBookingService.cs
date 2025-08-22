using Microsoft.AspNetCore.Mvc;
using Multi_Vendor_Event_Booking_System.Data.Config;
using Multi_Vendor_Event_Booking_System.Data.DTO;

namespace Multi_Vendor_Event_Booking_System.Services
{
    public interface IBookingService
    {
        Task<ActionResult> CreateBookingAsync(BookingDTO dto);

        Task<List<BookingDTO>> GetAllBookings();

        Task<ActionResult> GetBookingByDate(string startDate);

        Task<ActionResult> GetBookingByStatus(BookingStatus status);

        Task<ActionResult> GetBookingByVendor(int id);

    }
}
