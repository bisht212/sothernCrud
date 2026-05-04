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


        [HttpPost("AddVendor")]
        public async Task<IActionResult> AddVendor([FromBody] AddUpdateVendorMasterRequest vendorMasterRequest)
        {
            var result = await _vendorMaster.AddVendorMaster(vendorMasterRequest);

            return Ok(new ApiResponse<VendorMasterResponse>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Vendor Added or updated successfully",
                Data = result
            });
        }


        [HttpPut("UpdateVendor/{vendorId}")]
        public async Task<IActionResult> UpdateVendor([FromRoute] int vendorId, [FromBody] AddUpdateVendorMasterRequest vendorMasterRequest)
        {
            var result = await _vendorMaster.UpdateVendorMaster(vendorId, vendorMasterRequest);

            return Ok(new ApiResponse<VendorMasterResponse>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Vendor updated successfully",
                Data = result
            });
        }

        [HttpPost("AddVendorContact")]
        public async Task<IActionResult> AddVendorContact([FromBody] List<AddVendorContact> contactRequest) {


            await _vendorMaster.AddVendorContact(contactRequest);

            return Ok(new ApiResponse<dynamic>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Vendor contact added successfully",
               
            });
        }


        [HttpPut("AddVendorContact/{contactId}")]
        public async Task<IActionResult> AddVendorContact([FromRoute]int contactId, [FromBody] AddVendorContact contactRequest)
        {


            await _vendorMaster.UpdateVendorContact(contactId,contactRequest);

            return Ok(new ApiResponse<dynamic>
            {
                StatusCode = 204,
                IsError = false,
                Message = "Vendor contact updated successfully",

            });
        }


        [HttpPost("add_vendor_legal_financial")]
        public async Task<IActionResult> AddVendorLegalFinancial([FromBody] AddVendorLegalFinancialRequest financialRequest)
        {


            int response = await _vendorMaster.AddVendorLegalFinancial(financialRequest);

            return Ok(new ApiResponse<dynamic>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Vendor Financial and leagl added successfully",

            });
        }


        [HttpPut("update_vendor_legal_financial/{vendorLegalFinancialid}")]
        public async Task<IActionResult> UpdateVendorLegalFinancial([FromRoute]int vendorLegalFinancialid, [FromBody] AddVendorLegalFinancialRequest financialRequest)
        {


            int response = await _vendorMaster.UpdateVendorLegalFinancial( vendorLegalFinancialid,financialRequest);

            return Ok(new ApiResponse<dynamic>
            {
                StatusCode = 204,
                IsError = false,
                Message = "Vendor Financial and leagl updated successfully",

            });
        }

    }
}
