//Final resolved version
// hello world 

using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Multi_Vendor_Event_Booking_System.Data.DTO;
using Multi_Vendor_Event_Booking_System.Data.Repository;
using Multi_Vendor_Event_Booking_System.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Multi_Vendor_Event_Booking_System.Services
{
    public class VendorService : IVendorService
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;
        private readonly IVendorRepository _vendorRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly ISqlRepository _sqlRepository;
        private readonly IValidator<VendorDTO> _validator;
        private readonly ILogger<VendorService> _logger;

        public VendorService(IConfiguration configuration,ILogger<VendorService> logger, IMapper mapper, IVendorRepository vendorRepository, IBookingRepository bookingRepository, IValidator<VendorDTO> validator, ISqlRepository sqlRepository)
        {
            _mapper = mapper;
            _vendorRepository = vendorRepository;
            _bookingRepository = bookingRepository;
            _validator = validator;
            _logger = logger;
            _sqlRepository = sqlRepository;
            _connectionString = configuration.GetConnectionString("EventDBConnection");
        }

        public async Task<ActionResult> CreateVendorAsync(VendorDTO dto)
        {

            //_logger.LogInformation("CreateVEndorAsync called with dto : {@VendorDTO}", dto);

            //if (dto == null)
            //    return new BadRequestObjectResult("Enter valid data");

            //var validatonResult = await _validator.ValidateAsync(dto);

            //if (!validatonResult.IsValid)
            //{
            //    return new BadRequestObjectResult(validatonResult.Errors.Select(e => new
            //    {
            //        e.PropertyName,
            //        e.ErrorMessage
            //    }));
            //}

            //Vendor vendor = _mapper.Map<Vendor>(dto);

            //var vendorAfterCreation = await _vendorRepository.CreateAsync(vendor);
            //dto.Id = vendorAfterCreation.Id;

            //return new CreatedResult($"api/vendors/GetAllVendors/{dto.Id}",dto);
            var id = await _sqlRepository.ExecuteScalarAsync<int>(
                "sp_AddVendor",
                new SqlParameter ("@Name",dto.Name),
                new SqlParameter("@Email", dto.Email),
                new SqlParameter("@Phone", dto.Phone)
                );

            dto.Id = id;
            return new CreatedResult($"api/vendors/GetAllVendors/{dto.Id}", dto);
        }
            
        public async Task<ActionResult> DeleteVendorAsync(int id)
        {
            //if (id <= 0)
            //    return new BadRequestObjectResult("Enter valid Id.");

            //var vendor = await _vendorRepository.GetAsync(vendor => vendor.Id == id);

            //if (vendor == null)
            //    return new NotFoundObjectResult("No vendor found for this Id.Enter valid Id.");

            //var bookings = await _bookingRepository.GetBookingByVendorId(vendor.Id);

            //if (bookings != null && bookings.Count > 0)
            //    return new ConflictObjectResult("Vendor cannot be deleted as bookings are there.");

            //await _vendorRepository.DeleteAsync(vendor);

            //hello world

            await _sqlRepository.ExecuteNonQueryAsync(
                "sp_DeleteVendor",
                new SqlParameter("@Id", id)
                );

            return new OkObjectResult("Vendor Deleted Successfully.");
        }

        public async Task<ActionResult> UpdateVendorAsync(VendorDTO dto)
        {
            //if (dto == null || dto.Id <= 0)
            //    return new BadRequestObjectResult("Enter valid data.");

            //var validatonResult = await _validator.ValidateAsync(dto);

            //if (!validatonResult.IsValid)
            //{
            //    return new BadRequestObjectResult(validatonResult.Errors.Select(e => new
            //    {
            //        e.PropertyName,
            //        e.ErrorMessage
            //    }));
            //}

            //var existingVendor = await _vendorRepository.GetAsync(vendor => vendor.Id == dto.Id, true);

            //if (existingVendor == null)
            //    return new NotFoundObjectResult("No vendor found");

            //var newRecord = _mapper.Map<Vendor>(dto);

            //await _vendorRepository.UpdateAsync(newRecord);

            //return new OkObjectResult("Vendor Updated Successfully.");

            var parameters = new[]
                {
                    new SqlParameter("@Id", dto.Id),
                    new SqlParameter("@Name", dto.Name ?? (object)DBNull.Value),
                    new SqlParameter("@Email", dto.Email ?? (object)DBNull.Value),
                    new SqlParameter("@Phone", dto.Phone ?? (object)DBNull.Value)
                };

            await _sqlRepository.ExecuteNonQueryAsync("sp_UpdateVendor", parameters);

            return new OkObjectResult("Vendor Updated Successfully.");
        }

        public async Task<List<GetVendorDTO>> GetAllVendorsAsync()
        {
            // Call stored procedure and get 2 result sets
            var (vendors, services) = await _sqlRepository.ExecuteMultiResultAsync<GetVendorDTO, ServicesDTO>("usp_GetAllVendors");

            // Now map services into each vendor
            foreach (var vendor in vendors)
            {
                vendor.Services = services
                    .Where(s => s.VendorId == vendor.VendorId)
                    .ToList();
            }

            return vendors;
        }
    }
}
