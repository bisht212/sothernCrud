using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpPost("add_restaurant_on_property")]
        public async Task<IActionResult> AddRestaurantsOnProperty(
        [FromForm] List<AddRestaurantsOnPropertyRequest> request)
        {
            if (request == null || !request.Any())
            {
                return BadRequest(new ApiResponse<string>
                {
                    StatusCode = 400,
                    IsError = true,
                    Message = "Request data is empty"
                });
            }

            await _hotelMaster.InsertRestaurantsWithFiles(request);

            return StatusCode(201, new ApiResponse<string>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Restaurant property added successfully"
            });
        }



        [HttpPut("update_restaurant_on_property/{Id}")]
        public async Task<IActionResult> UpdateRestaurantsOnProperty(int Id,
        [FromForm] AddRestaurantsOnPropertyRequest request)
        {
            if (request == null )
            {
                return BadRequest(new ApiResponse<dynamic>
                {
                    StatusCode = 400,
                    IsError = true,
                    Message = "Request data is empty"
                });
            }

            await _hotelMaster.UpdateRestaurantsWithFiles(Id, request);

            return StatusCode(201, new ApiResponse<dynamic>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Restaurant property updated successfully"
            });
        }

        [HttpDelete("delete_restaurant_on_property/{Id}")]
        public async Task<IActionResult> DeleteRestaurantsOnProperty(int Id)
        {
          var response =   await _hotelMaster.DeleteRestaurant(Id);

            return StatusCode(201, new ApiResponse<dynamic>
            {
                StatusCode = 201,
                IsError = false,
                Message = response.Message
            });
        }

        [HttpPost("add_hotel_contacts")]
        public async Task<IActionResult> Addhotelscontacts(List<AddHotelContacts> hotelContacts) {

           
             await _hotelMaster.AddHotelContacts(hotelContacts);

            return Ok(new ApiResponse<dynamic>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Success",
              
            });


        }


        [HttpPut("update_hotel_contacts/{contactId}")]
        public async Task<IActionResult> Updatehotelscontacts(int contactId, AddHotelContacts hotelContacts)
        {


            await _hotelMaster.UpdateHotelContacts(contactId,hotelContacts);

            return Ok(new ApiResponse<dynamic>
            {
                StatusCode = 204,
                IsError = false,
                Message = "Success",

            });


        }
    }
}