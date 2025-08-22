using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Multi_Vendor_Event_Booking_System.Data.DTO;
using Multi_Vendor_Event_Booking_System.Data.Repository;
using Multi_Vendor_Event_Booking_System.Services;
using Multi_Vendor_Event_Booking_System.Validators;

namespace Multi_Vendor_Event_Booking_System.Configurations
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IVendorRepository, VendorRepository>();
            services.AddScoped<IVendorService, VendorService>();
            services.AddScoped<IServiceCatalogRepository, ServiceCatalogRepository>();
            services.AddScoped<IServiceCatalogService, ServiceCatalogService>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ISqlRepository, SqlRepository>();
            services.AddScoped<IValidator<Services_CatalogDTO>, Service_CatalogDTOValidator>();
            services.AddScoped<IValidator<BookingDTO>, BookingDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<VendorDTOValidator>();
            
            return services;
        }
    }
}
