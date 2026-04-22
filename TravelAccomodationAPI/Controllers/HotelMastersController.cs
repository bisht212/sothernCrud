using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using TravelAccomodationAPI.BusinessClass;
using TravelAccomodationAPI.BusinessClass.Interface;
using TravelAccomodationAPI.ModelClass;
using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.ResponseModule;

namespace TravelAccomodationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableRateLimiting("fixed")]
    public class HotelMastersController : ControllerBase
    {
        private readonly IHotelMaster_Business _hotelMaster;

        private readonly ILogger<HotelMastersController> _logger;

        public HotelMastersController(IHotelMaster_Business hotelMaster, ILogger<HotelMastersController> logger)
        {
            _hotelMaster = hotelMaster;
            _logger = logger;
        }

        /// <summary>
        /// Get Hotel Master details
        /// </summary>
        /// <param name="request">Hotel filter request payload</param>
        /// <returns>It  returns response  is Hotel Id , Hotel code, hotel name (HotelMasterResponse)</returns>
        [HttpGet("Get_Hotel_Master_List")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> Get_Hotel_Master_List([FromQuery] HotelFilterRequest hotelFilter)
        {
            try
            {
                var result = await _hotelMaster.GetHotelMasterList(hotelFilter);


                var response = new ApiResponse<IEnumerable<HotelMasterResponse>>
                {
                    StatusCode = 200,
                    IsError = false,
                    Message = "Success",
                    Data = result
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Use the same generic type for the error response


                _logger.LogError(
                           ex,
                           "Error occurred in Get_Hotel_Master_List for request: {@HotelFilter}",
                           hotelFilter);

                var errorResponse = new ApiResponse<HotelMasterResponse>
                {
                    StatusCode = 500,
                    IsError = true,
                    Message = ex.Message, // Or "An internal error occurred"
                    Data = null
                };

                return StatusCode(500, errorResponse);
            }

        }


        /// <summary>
        /// Add Hotel Master details
        /// </summary>
        /// <param name="request">Hotel master request payload</param>
        /// <returns>Hotel Id and Hotel Code</returns>

        [HttpPost("AddHotel")]
        public async Task<IActionResult> AddHotel([FromBody] AddHotelMasterRequest hotelrequest)
        {
            try
            {
                AddHotelMasterResponse result = await _hotelMaster.AddhotelsMaster(hotelrequest);

                if (result.Hotel_Id>0)
                {
                    var response = new ApiResponse<AddHotelMasterResponse>
                    {
                        StatusCode = 201,
                        IsError = false,
                        Message = "Hotel Added successfully",
                        Data = result
                    };


                    return Ok(response);
                }
                else {
                    return BadRequest(new ApiResponse<AddHotelMasterResponse>
                    {
                        StatusCode = 400,
                        IsError = true,
                        Message = result.Message
                    });
                }
                  
            }
            catch (Exception ex)
            {
                // System failure (e.g., DB is down)
                return StatusCode(500, new ApiResponse<int>
                {
                    StatusCode = 500,
                    IsError = true,
                    Message = ex.Message
                });
            }
        }


    }

}
