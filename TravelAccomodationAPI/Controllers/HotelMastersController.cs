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
        [HttpGet("Get_Hotel_Master_List")]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> Get_Hotel_Master_List([FromQuery] HotelFilterRequest hotelFilter)
        {
            var result = await _hotelMaster.GetHotelMasterList(hotelFilter);

            return Ok(new ApiResponse<IEnumerable<HotelMasterResponse>>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = result
            });
        }

        /// <summary>
        /// Add Hotel Master details
        /// </summary>
        [HttpPost("AddHotel")]
        public async Task<IActionResult> AddHotel([FromBody] AddHotelMasterRequest hotelrequest)
        {
            var result = await _hotelMaster.AddhotelsMaster(hotelrequest);

            return Ok(new ApiResponse<AddHotelMasterResponse>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Hotel Added successfully",
                Data = result
            });
        }

        /// <summary>
        /// Get All List Constant veg non veg
        /// </summary>

        [HttpGet("Get_Veg_Non_Veg")]
        public async Task<IActionResult> GetAll_Veg_Non_Veg() {

            var result = await _hotelMaster.GetAll_Veg_Non_Veg();

            return Ok(new ApiResponse<IEnumerable<GetVeg_NonVegResponse>>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = result
            });
        }


        /// <summary>
        /// Get Constant veg non veg by there Id
        /// <request>vegnonvegId</request>
        /// </summary>

        [HttpGet("Get_Veg_Non_Veg/{Id}")]
        public async Task<IActionResult> Get_Veg_Non_Veg(int Id) {

            var result = await _hotelMaster.Get_Veg_Non_Veg(Id);

            return Ok(new ApiResponse<GetVeg_NonVegResponse>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = result
            });
        }


        /// <summary>
        /// Get All List Cuisines
        /// </summary>
        [HttpGet("GetAll_Cuisine")]
        public async Task<IActionResult> GetAll_Cuisine()
        {

            IEnumerable<GetCuisine> result = await _hotelMaster.GetAll_Cuisine();

            return Ok(new ApiResponse<IEnumerable<GetCuisine>>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = result
            });
        }

        /// <summary>
        /// Get Constant veg non veg by there Id
        /// <request>cuisineId</request>
        /// </summary>
        [HttpGet("Get_Cuisine/{Id}")]
        public async Task<IActionResult> Get_Cuisine(int Id)
        {

            GetCuisine result = await _hotelMaster.Get_Cuisine(Id);

            return Ok(new ApiResponse<GetCuisine>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = result
            });
        }

        [HttpPost("Add_Resturant_On_Property")]
        public async Task<IActionResult> AddRestaurantsOnProperty(AddRestaurantsOnPropertyRequest resturantRequest) {

            int result = await _hotelMaster.AddrestaurantsOnProperty(resturantRequest);

            return Ok(new ApiResponse<string>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Success",
                Data = "Resturant propert Added"
            });

        }
    }
}