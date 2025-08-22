using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Multi_Vendor_Event_Booking_System.Data.Config;
using Multi_Vendor_Event_Booking_System.Models;

namespace Multi_Vendor_Event_Booking_System.Data
{
    public class EventDBContext : IdentityDbContext<IdentityUser>
    {
        public EventDBContext(DbContextOptions<EventDBContext> options) : base(options)
        {
            
        }

        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Services_Catalog> Services_Catalogs { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new VendorConfig());
            modelBuilder.ApplyConfiguration(new ServicesCatalogConfig());
            modelBuilder.ApplyConfiguration(new BookingConfig());
        }
    }
}
