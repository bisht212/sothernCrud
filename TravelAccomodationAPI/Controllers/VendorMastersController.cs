 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccomodationAPI.BusinessClass.Interface;
using TravelAccomodationAPI.ModelClass;
using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.RequestModel.VendorMaster;
using TravelAccomodationAPI.ModelClass.ResponseModule;
using TravelAccomodationAPI.ModelClass.ResponseModule.VendorMaster;

namespace TravelAccomodationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorMastersController : ControllerBase
    {
        private readonly  IVendorMaster_Business _vendorMaster;

        private readonly ILogger<HotelMastersController> _logger;

        public VendorMastersController(IVendorMaster_Business vendorMaster, ILogger<HotelMastersController> logger)
        {
            
            _vendorMaster= vendorMaster;
            _logger = logger;
        }


        [HttpPost("AddHotel")]
        public async Task<IActionResult> AddHotel([FromBody] AddUpdateVendorMasterRequest vendorMasterRequest)
        {
            var result = await _vendorMaster.AddVendorMaster(vendorMasterRequest);

            return Ok(new ApiResponse<VendorMasterResponse>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Vendor Added successfully",
                Data = result
            });
        }

    }
}
