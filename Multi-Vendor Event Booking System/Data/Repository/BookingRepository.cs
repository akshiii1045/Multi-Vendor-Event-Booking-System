using Microsoft.EntityFrameworkCore;
using Multi_Vendor_Event_Booking_System.Data.Config;
using Multi_Vendor_Event_Booking_System.Models;
using System.Linq.Expressions;

namespace Multi_Vendor_Event_Booking_System.Data.Repository
{
    public class BookingRepository : EventRepository<Booking>, IBookingRepository
    {
        private readonly EventDBContext _dbContext;
        private DbSet<Booking> _dbSet;
        public BookingRepository(EventDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<Booking>();
        }

        public async Task<bool> BookAsync(Expression<Func<Booking, bool>> predicate)
        {
            return await _dbContext.Bookings.AnyAsync(predicate);
        }

        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<List<Booking>> GetBookingByStatus(BookingStatus bs)
        {
            return await _dbSet.Where(b => b.Status == bs).ToListAsync();
        }

        public async Task<List<Booking>> GetBookingByVendorId (int id)
        {
            return await _dbSet.Where(b => b.Services_Catalog.VendorId == id).ToListAsync();
        }

        public async Task<List<Booking>> GetBookingsByDateAsync(DateTime startDate)
        {
            return await _dbSet.Where(b => b.StartDate.Date == startDate.Date).ToListAsync();
        }
    }
}
