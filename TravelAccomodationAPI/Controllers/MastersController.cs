using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelAccomodationAPI.BusinessClass.Interface;
using TravelAccomodationAPI.ModelClass;
using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.ResponseModule;

namespace TravelAccomodationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MastersController : ControllerBase
    {
        private readonly IMaster_Business _master_Business;
        public MastersController(IMaster_Business master_Business)
        {
            _master_Business = master_Business; 
        }


        /// <summary>
        /// Get All List Constant veg non veg
        /// </summary>

        [HttpGet("Get_Veg_Non_Veg")]
        public async Task<IActionResult> GetAll_Veg_Non_Veg()
        {

            var result = await _master_Business.GetAll_Veg_Non_Veg();

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
        public async Task<IActionResult> Get_Veg_Non_Veg(int Id)
        {

            var result = await _master_Business.Get_Veg_Non_Veg(Id);

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

            IEnumerable<GetCuisine> result = await _master_Business.GetAll_Cuisine();

            return Ok(new ApiResponse<IEnumerable<GetCuisine>>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = result
            });
        }


        /// <summary>
        /// Get cuisine details by cuisine Id.
        /// <request>Id</request>
        /// </summary>
        [HttpGet("Get_Cuisine/{Id}")]
        public async Task<IActionResult> Get_Cuisine(int Id)
        {

            GetCuisine result = await _master_Business.Get_Cuisine(Id);

            return Ok(new ApiResponse<GetCuisine>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = result
            });
        }

        /// <summary>
        /// Add a new phone type (e.g., Mobile, Landline, WhatsApp).
        /// <request>AddPhoneType</request>
        /// </summary>
        [HttpPost("add_phone_type")]
        public async Task<IActionResult> AddPhoneType(AddPhoneType phoneTypeRequest)
        {
            await _master_Business.AddPhoneType(phoneTypeRequest);

            return StatusCode(201, new ApiResponse<dynamic>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Success"
            });
        }

        /// <summary>
        /// Get the list of all phone types.
        /// </summary>
        [HttpGet("get_phone_types")]
        public async Task<IActionResult> GetPhoneTypes()
        {
            var response = await _master_Business.GetPhoneTypes();
            return Ok(new ApiResponse<IEnumerable<PhoneTypeResponse>>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = response
            });

        }

        /// <summary>
        /// Get phone type by phoneTypeId.
        /// <request>phoneTypeId</request>
        /// </summary>
        [HttpGet("get_phone_type/{phoneTypeId}")]
        public async Task<IActionResult> GetPhoneType(int phoneTypeId)
        {
            var response = await _master_Business.GetPhoneType(phoneTypeId);
            return Ok(new ApiResponse<PhoneTypeResponse>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = response
            });

        }

        /// <summary>
        /// Add Aminity.
        /// <request>AddAmenitiesRequest</request>
        /// </summary>
        [HttpPost("add_aminity")]
        public async Task<IActionResult> AddAminity(AddAmenitiesRequest aminityRequest)
        {

            await _master_Business.AddAminity(aminityRequest);

            return StatusCode(201, new ApiResponse<dynamic>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Success"
            });
        }

        /// <summary>
        /// update Aminity.
        ///  <request>amenityId</request>
        /// <request>AddAmenitiesRequest</request>
        /// </summary>
        [HttpPut("update_aminity/{amenityId}")]
        public async Task<IActionResult> AddAminity(int amenityId, AddAmenitiesRequest aminityRequest)
        {

            await _master_Business.UpdateAminity(amenityId, aminityRequest);

            return StatusCode(204, new ApiResponse<dynamic>
            {
                StatusCode = 204,
                IsError = false,
                Message = "Success"
            });
        }

        /// <summary>
        /// Get List Aminites.
        /// </summary>
        [HttpGet("get_aminities")]
        public async Task<IActionResult> GetAminities()
        {
            var response = await _master_Business.GetAminities();
            return Ok(new ApiResponse<IEnumerable<GetAminityResponse>>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = response
            });
        }

        /// <summary>
        /// Get  Aminity by Id.
        /// <request>aminityId</request>
        /// </summary>
        [HttpGet("get_aminity/{aminityId}")]
        public async Task<IActionResult> GetAminity(int aminityId)
        {

            var response = await _master_Business.GetAminity(aminityId);
            return Ok(new ApiResponse<GetAminityResponse>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = response
            });
        }

        /// <summary>
        /// GetRoomFacilities.
        /// <request>roomFacility optional</request>
        /// </summary>
        [HttpGet("get_room_facilities")]
        public async Task<IActionResult> GetRoomFacilities(string? roomFacility)
        {

            var response = await _master_Business.GetHotelRoomFacilities(roomFacility);
            return Ok(new ApiResponse<IEnumerable<GetRoomFacilityResponse>>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = response
            });
        }

        /// <summary>
        /// GetRoomFacilities.
        /// <request>roomFacility optional</request>
        /// </summary>
        [HttpGet("get_room_facility/{roomFacility}")]
        public async Task<IActionResult> GetRoomFacility(int roomFacility)
        {

            var response = await _master_Business.GetHotelRoomFacility(roomFacility);
            return Ok(new ApiResponse<GetRoomFacilityResponse>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = response
            });
        }

        /// <summary>
        /// Add Room Facility.
        /// <request>roomFacility</request>
        /// </summary>
        [HttpPost("add_room_facility")]
        public async Task<IActionResult> AddRoomFacility(string roomFacilities)
        {

            await _master_Business.AddRoomFacility(roomFacilities);
            return StatusCode(201, new ApiResponse<dynamic>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Room facility add successfully",
               
            });
        }

        /// <summary>
        /// Update Room Facility.
        /// <request>roomFacilities_id, roomFacility</request>
        /// </summary>
        [HttpPut("update-room-facility/{roomFacilities_id}")]
        public async Task<IActionResult> UpdateRoomFacility(int roomFacilities_id, string roomFacilities)
        {

            await _master_Business.UpdateRoomFacility(roomFacilities_id, roomFacilities);
            return StatusCode(201, new ApiResponse<dynamic>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Room facility updated successfully",

            });
        }

        /// <summary>
        /// Delte Room Facility.
        /// <request>roomFacilities_id</request>
        /// </summary>
        /// 
        [HttpDelete("delete-room-facility/{roomFacilities_id}")]
        public async Task<IActionResult> DeleteRoomFacility(int roomFacilities_id)
        {

            await _master_Business.DeleteRoomFacility(roomFacilities_id);
            return StatusCode(201, new ApiResponse<dynamic>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Room facility Deleted successfully",

            });
        }
    }
}
