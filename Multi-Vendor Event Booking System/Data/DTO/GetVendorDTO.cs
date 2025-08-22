namespace Multi_Vendor_Event_Booking_System.Data.DTO
{
    public class GetVendorDTO
    {
        public int VendorId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public List<ServicesDTO> Services { get; set; } = new();

    }

    public class ServicesDTO
    {
        public int? ServiceId { get; set; }
        public int VendorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
    }


}