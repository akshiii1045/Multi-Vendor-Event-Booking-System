namespace Multi_Vendor_Event_Booking_System.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual ICollection<Services_Catalog> Services_Catalogs { get; set; }
    }
}
