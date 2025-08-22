using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Multi_Vendor_Event_Booking_System.Data.DTO;
using Multi_Vendor_Event_Booking_System.Services;

namespace Multi_Vendor_Event_Booking_System.Controllers
{
    [Route("api/services")]
    [ApiController]
    public class ServiceCatalogController : ControllerBase
    {
        private readonly IServiceCatalogService _serviceCatalogService;

        public ServiceCatalogController(IServiceCatalogService serviceCatalogService)
        {
            _serviceCatalogService = serviceCatalogService;
        }

        [Route("AddService")]
        [HttpPost]
        public async Task<IActionResult> CreateService([FromBody] Services_CatalogDTO dto)
        {
            return await _serviceCatalogService.CreateServiceAsync(dto);
        }

        [Route("EditService")]
        [HttpPut]
        public async Task<IActionResult> UpdateService([FromBody] Services_CatalogDTO dto)
        {
            return await _serviceCatalogService.UpdateServiceAsync(dto);
        }

        [Route("RemoveService")]
        [HttpDelete]
        public async Task<IActionResult> DeleteService(int id)
        {
            return await _serviceCatalogService.DeleteServiceAsync(id);
        }


        [HttpGet("GetAllServices")]
        public async Task<IActionResult> GetAllService()
        {
            return await _serviceCatalogService.GetAllServiceAsync();
        }
    }
}
