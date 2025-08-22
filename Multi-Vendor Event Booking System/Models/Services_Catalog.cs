namespace Multi_Vendor_Event_Booking_System.Models
{
    public class Services_Catalog
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public int VendorId { get; set; }

        public virtual Vendor Vendor { get; set; }

        public int Price { get; set; }

        public bool isActive { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
