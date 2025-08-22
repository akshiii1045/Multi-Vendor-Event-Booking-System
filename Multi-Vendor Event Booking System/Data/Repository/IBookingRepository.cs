using Microsoft.AspNetCore.Mvc;
using Multi_Vendor_Event_Booking_System.Data.Config;
using Multi_Vendor_Event_Booking_System.Models;
using System.Linq.Expressions;

namespace Multi_Vendor_Event_Booking_System.Data.Repository
{
    public interface IBookingRepository : IEventRepository<Booking>
    {
        Task<bool> BookAsync(Expression<Func<Booking, bool>> predicate);

        Task<List<Booking>> GetAllBookingsAsync();

        Task<List<Booking>> GetBookingByStatus(BookingStatus bs);

        Task<List<Booking>> GetBookingByVendorId(int id);

        Task<List<Booking>> GetBookingsByDateAsync(DateTime startdate);

    }
}
