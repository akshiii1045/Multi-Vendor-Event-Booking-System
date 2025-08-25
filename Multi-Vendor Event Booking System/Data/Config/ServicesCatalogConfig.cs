using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Multi_Vendor_Event_Booking_System.Models;

namespace Multi_Vendor_Event_Booking_System.Data.Config
{
    public class ServicesCatalogConfig : IEntityTypeConfiguration<Services_Catalog>
    {
        public void Configure(EntityTypeBuilder<Services_Catalog> builder)
        {
            builder.ToTable("Services_Catalogs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Category).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.Price).HasMaxLength(10);
            builder.Property(x => x.isActive).IsRequired();
            builder.HasOne(n => n.Vendor)
                .WithMany(n => n.Services_Catalogs)
                .HasForeignKey(n => n.VendorId)
                .HasConstraintName("FK_Services_Vendor");
        }
    }
}