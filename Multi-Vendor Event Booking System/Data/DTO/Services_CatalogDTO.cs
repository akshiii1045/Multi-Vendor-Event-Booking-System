namespace Multi_Vendor_Event_Booking_System.Data.DTO
{
    public class Services_CatalogDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public int VendorId { get; set; }

        public int Price { get; set; }

        public bool isActive { get; set; }
    }
}
