using Microsoft.AspNetCore.Mvc;
using Multi_Vendor_Event_Booking_System.Data.DTO;

namespace Multi_Vendor_Event_Booking_System.Services
{
    public interface IVendorService
    {
        Task<ActionResult> CreateVendorAsync(VendorDTO dto);

        Task<ActionResult> UpdateVendorAsync(VendorDTO dto);

        Task<ActionResult> DeleteVendorAsync(int id);

        Task<List<GetVendorDTO>> GetAllVendorsAsync();
    }
}
