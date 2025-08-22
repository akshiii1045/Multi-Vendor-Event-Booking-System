using Microsoft.EntityFrameworkCore;
using Multi_Vendor_Event_Booking_System.Models;

namespace Multi_Vendor_Event_Booking_System.Data.Repository
{
    public class ServiceCatalogRepository : EventRepository<Services_Catalog>, IServiceCatalogRepository
    {
        private readonly EventDBContext _dbContext;
        private DbSet<Services_Catalog> _dbSet;

        public ServiceCatalogRepository(EventDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
            _dbSet = _dbContext.Set<Services_Catalog>();
        }

        public async Task<List<Services_Catalog>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}