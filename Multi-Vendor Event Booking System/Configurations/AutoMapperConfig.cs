using AutoMapper;
using Multi_Vendor_Event_Booking_System.Data.DTO;
using Multi_Vendor_Event_Booking_System.Models;

namespace Multi_Vendor_Event_Booking_System.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<VendorDTO, Vendor>().ReverseMap();
            CreateMap<GetVendorDTO, Vendor>().ReverseMap();
            CreateMap<Services_CatalogDTO, Services_Catalog>().ReverseMap();
            CreateMap<BookingDTO, Booking>().ReverseMap();
        }
    }
}
