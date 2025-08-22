using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Multi_Vendor_Event_Booking_System.Data.DTO;
using Multi_Vendor_Event_Booking_System.Data.Repository;
using Multi_Vendor_Event_Booking_System.Models;

namespace Multi_Vendor_Event_Booking_System.Services
{
    public class ServiceCatalogService : IServiceCatalogService
    {
        private readonly IMapper _mapper;
        private readonly IServiceCatalogRepository _serviceCatalogRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IVendorRepository _vendorRepository;
        private readonly IValidator<Services_CatalogDTO> _catalogValidator;

        public ServiceCatalogService(IMapper mapper,IServiceCatalogRepository serviceCatalogRepository,IBookingRepository bookingRepository, IValidator<Services_CatalogDTO> catalogValidator, IVendorRepository vendorRepository)
        {
            _mapper = mapper;
            _serviceCatalogRepository = serviceCatalogRepository;
            _bookingRepository = bookingRepository;
            _catalogValidator = catalogValidator;
            _vendorRepository = vendorRepository;
        }
        public async Task<ActionResult> CreateServiceAsync(Services_CatalogDTO dto)
        {
            if (dto == null)
                return new BadRequestObjectResult("Enter valid data.");

            var validationResult = await _catalogValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(validationResult.Errors.Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage
                }));
            }

            var vendor = await _vendorRepository.GetAsync(service => service.Id == dto.VendorId);
            if (vendor == null)
                return new NotFoundObjectResult("Enter valid Vendor Id.");

            Services_Catalog services_Catalog = _mapper.Map<Services_Catalog>(dto);
            
            var serviceAfterCreation = await _serviceCatalogRepository.CreateAsync(services_Catalog);
            dto.Id = serviceAfterCreation.Id;

            return new CreatedResult($"api/services/{dto.Id}", dto);
        }

        public async Task<ActionResult> UpdateServiceAsync(Services_CatalogDTO dto)
        {
            if (dto == null || dto.Id <= 0 || dto.VendorId <= 0)
                return new BadRequestObjectResult("Give valid data.");

            var validationResult = await _catalogValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(validationResult.Errors.Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage
                }));
            }

            var vendor = await _serviceCatalogRepository.GetAsync(service => service.VendorId == dto.VendorId);
            if (vendor == null)
                return new NotFoundObjectResult("Enter valid Vendor Id.");

            var existingService = await _serviceCatalogRepository.GetAsync(service => service.Id == dto.Id, true);

            if (existingService == null)
                return new NotFoundObjectResult("No service found for this id.Enter valid Id.");

            var newRecord = _mapper.Map<Services_Catalog>(dto);

            await _serviceCatalogRepository.UpdateAsync(newRecord);

            return new NoContentResult();
        }

        public async Task<ActionResult> DeleteServiceAsync(int id)
        {
            if (id <= 0)
                return new BadRequestObjectResult("Enter valid id.");

            var service = await _serviceCatalogRepository.GetAsync(service => service.Id == id);

            if (service == null)
                return new NotFoundObjectResult("No service for this Id found.");

            var bookings = await _bookingRepository.BookAsync(b => b.Service_CatalogId == service.Id);

            if (bookings)
                return new ConflictObjectResult("Service cannot be deleted as bookings are there !!");

            await _serviceCatalogRepository.DeleteAsync(service);

            return new OkObjectResult("Service deleted successfully.");
        }

        public async Task<ActionResult> GetAllServiceAsync()
        {
            var services = await _serviceCatalogRepository.GetAllAsync();

            return new OkObjectResult(services);
        }
    }
}
