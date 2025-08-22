using Multi_Vendor_Event_Booking_System.Models;

namespace Multi_Vendor_Event_Booking_System.Data.Repository
{
    public interface IServiceCatalogRepository : IEventRepository<Services_Catalog>
    {
        Task<List<Services_Catalog>> GetAllAsync();

    }
}
