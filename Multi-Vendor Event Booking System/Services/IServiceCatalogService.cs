using Microsoft.AspNetCore.Mvc;
using Multi_Vendor_Event_Booking_System.Data.DTO;

namespace Multi_Vendor_Event_Booking_System.Services
{
    public interface IServiceCatalogService
    {

        Task<ActionResult> CreateServiceAsync(Services_CatalogDTO dto);

        Task<ActionResult> UpdateServiceAsync(Services_CatalogDTO dto);

        Task<ActionResult> DeleteServiceAsync(int id);

        Task<ActionResult> GetAllServiceAsync();
    }
}
