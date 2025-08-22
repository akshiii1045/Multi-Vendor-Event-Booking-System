using Multi_Vendor_Event_Booking_System.Data.Config;

namespace Multi_Vendor_Event_Booking_System.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public string CustomerName { get; set; }

        public string CustomerEmail { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Service_CatalogId { get; set; }

        public virtual Services_Catalog Services_Catalog { get; set; }   

        public BookingStatus Status { get; set; }
    }
}
