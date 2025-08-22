using Multi_Vendor_Event_Booking_System.Models;

namespace Multi_Vendor_Event_Booking_System.Data.Repository
{
    public interface IVendorRepository : IEventRepository<Vendor>
    {
        Task<List<Vendor>> GetAllVendorsAsync();
    }
}
