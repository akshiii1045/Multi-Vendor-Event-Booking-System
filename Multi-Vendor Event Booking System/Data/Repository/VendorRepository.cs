using Microsoft.EntityFrameworkCore;
using Multi_Vendor_Event_Booking_System.Models;

namespace Multi_Vendor_Event_Booking_System.Data.Repository
{
    public class VendorRepository : EventRepository<Vendor>, IVendorRepository
    {
        private readonly EventDBContext _dbContext;
        public VendorRepository(EventDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<List<Vendor>> GetAllVendorsAsync()
        {
            return await _dbContext.Vendors
                .FromSqlRaw("EXEC usp_GetAllVendors")
                .ToListAsync();
        }   
    }
}
