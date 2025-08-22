using Multi_Vendor_Event_Booking_System.Data.Config;
using Multi_Vendor_Event_Booking_System.Models;

namespace Multi_Vendor_Event_Booking_System.Data.DTO
{
    public class BookingDTO
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public int Service_CatalogId { get; set; }

    }
}
