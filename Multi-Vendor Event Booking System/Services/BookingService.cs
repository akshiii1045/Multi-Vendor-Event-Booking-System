using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Multi_Vendor_Event_Booking_System.Data.Config;
using Multi_Vendor_Event_Booking_System.Data.DTO;
using Multi_Vendor_Event_Booking_System.Data.Repository;
using Multi_Vendor_Event_Booking_System.Models;

namespace Multi_Vendor_Event_Booking_System.Services
{
    public class BookingService : IBookingService
    {
        private readonly IMapper _mapper;
        private readonly IBookingRepository _bookingRepository;
        private readonly IServiceCatalogRepository _serviceCatalogRepository;
        private readonly IValidator<BookingDTO> _bookingValidator;

        public BookingService(IMapper mapper, IBookingRepository bookingRepository, IServiceCatalogRepository serviceCatalogRepository, IValidator<BookingDTO> bookingValidator)
        {
            _mapper = mapper;
            _bookingRepository = bookingRepository;
            _serviceCatalogRepository = serviceCatalogRepository;
            _bookingValidator = bookingValidator;
        }

        public async Task<ActionResult> CreateBookingAsync(BookingDTO dto)
        {
            if (dto == null)
                return new BadRequestObjectResult("Enter valid data.");
            var validationResult = await _bookingValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(validationResult.Errors.Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage
                }));
            }


            var service = await _serviceCatalogRepository.GetAsync(s => s.Id == dto.Service_CatalogId);
            if (service == null)
                return new NotFoundObjectResult("The service does not exist");

            if (!service.isActive)
                return new BadRequestObjectResult("Booking cannot be made as the service is inactive");

            if (!DateTime.TryParse(dto.StartDate, out DateTime startDate) || !DateTime.TryParse(dto.EndDate, out DateTime endDate))
            {
                return new BadRequestObjectResult("Invalid start or end date format.It should be (YYYY-MM-DD)");
            }

            if (startDate >= endDate)
                return new BadRequestObjectResult("Start date must be before end date.");

            var hasOverlap = await _bookingRepository.BookAsync(b =>
            b.Service_CatalogId == dto.Service_CatalogId &&
            b.Status != Data.Config.BookingStatus.Cancelled && b.StartDate < endDate &&
            startDate < b.EndDate);
            if (hasOverlap)
                return new ConflictObjectResult("Booking overlaps.");

            if (startDate.Date < DateTime.Today)
                return new BadRequestObjectResult("Cannot book in the past.");


            var booking = _mapper.Map<Booking>(dto);
            booking.StartDate = startDate;
            booking.EndDate = endDate;

            await _bookingRepository.CreateAsync(booking);

            dto.Id = booking.Id;
            return new CreatedResult($"api/bookings/{dto.Id}", dto);
        }

        public async Task<List<BookingDTO>> GetAllBookings()
        {
            var bookings = await _bookingRepository.GetAllBookingsAsync();
            return _mapper.Map<List<BookingDTO>>(bookings);
        }

        public async Task<ActionResult> GetBookingByDate(string startDate)
        {
            if (!DateTime.TryParse(startDate, out DateTime startDatestr))
                return new BadRequestObjectResult("Invalid start date format");

            var bookings = await _bookingRepository.GetBookingsByDateAsync(startDatestr);

            if (bookings == null || bookings.Count == 0)
                return new NotFoundObjectResult("No bookings found for that start date.");

            return new OkObjectResult(bookings);
        }

        public async Task<ActionResult> GetBookingByStatus(BookingStatus status)
        {
            var bookings = await _bookingRepository.GetBookingByStatus(status);

            if (bookings == null || bookings.Count == 0)
                return new NotFoundObjectResult("No bookings found with the specified");

            return new OkObjectResult(bookings);
        }

        public async Task<ActionResult> GetBookingByVendor(int id)
        {
            var getbyID = await _bookingRepository.GetBookingByVendorId(id);

            if (getbyID == null || getbyID.Count == 0)
                return new NotFoundObjectResult("No bookings found for this vendor.");

            return new OkObjectResult(getbyID);
        }
    }
}
