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
//    [EnableRateLimiting("fixed")]
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
        /// Add restaurant details on a hotel property.
        /// <request>List of AddRestaurantsOnPropertyRequest</request>
        /// </summary>
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

        /// <summary>
        /// Update restaurant details on a hotel property.
        /// <request>Id, AddRestaurantsOnPropertyRequest</request>
        /// </summary>
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

        /// <summary>
        /// Delete restaurant details on a hotel property (soft delete).
        /// <request>Id</request>
        /// </summary>
        [HttpDelete("delete_restaurant_on_property/{Id}")]
        public async Task<IActionResult> DeleteRestaurantsOnProperty(int Id)
        {
          var response =   await _hotelMaster.DeleteRestaurant(Id);

            return StatusCode(201, new ApiResponse<dynamic>
            {
                StatusCode = 204,
                IsError = false,
                Message = response.Message
            });
        }

        /// <summary>
        /// Add hotel contacts in bulk.
        /// <request>List of AddHotelContacts</request>
        /// </summary>
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

        /// <summary>
        /// Update hotel contact information.
        /// <request>contactId, AddHotelContacts</request>
        /// </summary>
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

        /// <summary>
        /// Delete hotel contact (soft delete).
        /// <request>Id</request>
        /// </summary>
        [HttpDelete("delete_hotel_contact/{Id}")]
        public async Task<IActionResult> DeleteHotelContact(int Id)
        {
            var response = await _hotelMaster.DeleteHotelContact(Id);

            return StatusCode(204, new ApiResponse<dynamic>
            {
                StatusCode = 201,
                IsError = false,
                Message = response.Message
            });
        }

        /// <summary>
        /// Add phone number to a hotel contact.
        /// <request>AddHotelContactPhoneNumberRequest</request>
        /// </summary>
        [HttpPost("add_hotel_contact_phone")]
        public async Task<IActionResult> AddHotelContactPhone(AddHotelContactPhoneNumberRequest phoneNumberRequest)
        {
            await _hotelMaster.AddHotelContactPhoneNumber(phoneNumberRequest);

            return StatusCode(201, new ApiResponse<dynamic>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Success"
            });
        }

        /// <summary>
        /// Delete a contact phone.
        /// <request>phone_id</request>
        /// </summary>
        [HttpDelete("delete_hotel_contact_phone_number/{phone_id}")]
        public async Task<IActionResult> DeleteContactPhone(long phone_id)
        {
            await _hotelMaster.DeletePhoneId(phone_id);

            return StatusCode(204, new ApiResponse<dynamic>
            {
                StatusCode = 204,
                IsError = false,
                Message = "Success"
            });

        }

        /// <summary>
        /// Add an email to a hotel contact.
        /// <request>AddHotelContactEmailRequest</request>
        /// </summary>
        [HttpPost("add_hotel_contact_email")]
        public async Task<IActionResult> AddHotelContactEmail(AddHotelContactEmailRequest emailRequest)
        {
            await _hotelMaster.AddHotelContactEmail(emailRequest);

            return StatusCode(201, new ApiResponse<dynamic>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Success"
            });
        }

        /// <summary>
        /// Delete a contact email.
        /// <request>email_id</request>
        /// </summary>
        [HttpDelete("delete_hotel_contact_email/{email_id}")]
        public async Task<IActionResult> DeleteContactEmail(long email_id)
        {
            await _hotelMaster.DeleteEmailId(email_id);

            return StatusCode(204, new ApiResponse<dynamic>
            {
                StatusCode = 204,
                IsError = false,
                Message = "Success"
            });
        }

        /// <summary>
        /// Add Aminity.
        /// <request>AddAmenitiesRequest</request>
        /// </summary>
        [HttpPost("add_aminity")]
        public async Task<IActionResult> AddAminity(AddAmenitiesRequest aminityRequest) {
            
            await _hotelMaster.AddAminity(aminityRequest);

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

            await _hotelMaster.UpdateAminity(amenityId, aminityRequest);

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
            var response = await _hotelMaster.GetAminities();
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

            var response = await _hotelMaster.GetAminity(aminityId);
            return Ok(new ApiResponse<GetAminityResponse>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = response
            });
        }

        /// <summary>
        /// get_hotel_facility_categories.
        /// </summary>
        [HttpGet("get_hotel_facility_categories")]
        public async Task<IActionResult> GetHotelFacilityCategories()
        {
            IEnumerable<HotelFacilityCategoryResponse> response = await _hotelMaster.GetHotelFacilityCategory();
            return Ok(new ApiResponse<IEnumerable<HotelFacilityCategoryResponse>>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = response
            });
        }

        /// <summary>
        /// Get  facility category by Id.
        /// <request>facility_CategoryId</request>
        /// </summary>
        [HttpGet("get_facility_category/{facility_CategoryId}")]
        public async Task<IActionResult> GetHotelFacilityCategoryByID(int facility_CategoryId)
        {

            HotelFacilityCategoryResponse response = await _hotelMaster.GetHotelFacilityCategoryByID(facility_CategoryId);
            return Ok(new ApiResponse<HotelFacilityCategoryResponse>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = response
            });
        }

        // <summary>
        /// Add hotel facility.
        /// <request>List<AddHotelFacilitiesRequest></request>
        /// </summary>
        [HttpPost("add_hotel_facility")]
        public async Task<IActionResult> AddHotelFacility(List<AddHotelFacilitiesRequest> hotelFacilityRequest)
        {
            await _hotelMaster.AddHotelFacility(hotelFacilityRequest);
            return Ok(new ApiResponse<dynamic>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
               
            });
        }

        /// <summary>
        /// Add Banquet details.
        /// <request>List<AddBanquestRequest></request>
        /// </summary>
        [HttpPost("add_hotel_banquest")]
        public async Task<IActionResult> InsertBanquetWithFiles( List<AddBanquestRequest> request)
        {
            await _hotelMaster.InsertBanquetWithFiles(request);

            return StatusCode(201, new ApiResponse<string>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Banquet added successfully"
            });
        }

        /// <summary>
        /// Update restaurant details on a hotel property.
        /// <request>Id, AddBanquestRequest</request>
        /// </summary>
        [HttpPut("update_Banquet_on_property/{banquetId}")]
        public async Task<IActionResult> UpdateBanquetOnProperty(int banquetId,
        [FromForm] AddBanquestRequest request)
        {
            if (request == null)
            {
                return BadRequest(new ApiResponse<dynamic>
                {
                    StatusCode = 400,
                    IsError = true,
                    Message = "Request data is empty"
                });
            }

            await _hotelMaster.UpdateBanquetWithFiles(banquetId, request);

            return StatusCode(204, new ApiResponse<dynamic>
            {
                StatusCode = 201,
                IsError = false,
                Message = "Restaurant property updated successfully"
            });
        }

        /// <summary>
        /// Delete Banquest details on a hotel property.
        /// <request>banquete_Id</request>
        /// </summary>
        [HttpDelete("delete_banquete_on_property/{banquete_Id}")]
        public async Task<IActionResult> DeleteRestaurantsOnProperty(long banquete_Id)
        {
            var response = await _hotelMaster.DeleteBanquete(banquete_Id);

            return StatusCode(204, new ApiResponse<dynamic>
            {
                StatusCode = 204,
                IsError = false,
                Message = response.Message
            });
        }


        /// <summary>
        /// Add hotel Files.
        /// <request>List of AddHotelFilesRequest</request>
        /// </summary>
        [HttpPost("hotel-img-upload")]
        public async Task<IActionResult> BulkUploadHotelFiles(
             [FromForm] List<AddHotelFilesRequest> files)
        {
           
            var result = await _hotelMaster
                .BulkUploadHotelFilesAsync(files);

            return StatusCode(201, new ApiResponse<string>
            {
                StatusCode = 201,
                IsError = false,
                Message = "File upload successfully"
            });
        }


        /// <summary>
        /// update hotel Files.
        /// <request>AddHotelFilesRequest</request>
        /// </summary>
        [HttpPut("update-hotel-img")]
        public async Task<IActionResult> UpdateHotelFile(long fileId,
             [FromForm] AddHotelFilesRequest file)
        {

            var result = await _hotelMaster
                .UpdateHotelFIle(fileId,file);

            return StatusCode(204, new ApiResponse<string>
            {
                StatusCode = 204,
                IsError = false,
                Message = "File upload successfully"
            });
        }


        /// <summary>
        /// Delete Banquest details on a hotel property.
        /// <request>banquete_Id</request>
        /// </summary>
        [HttpDelete("delete_hotel_file/{fileId}/")]
        public async Task<IActionResult> DeleteRestaurantsOnProperty(long fileId, long hotelId)
        {
            var response = await _hotelMaster.DeleteHotelFile(fileId, hotelId);

            return StatusCode(204, new ApiResponse<dynamic>
            {
                StatusCode = 204,
                IsError = false,
                Message = response.Message
            });
        }

        [HttpGet("Hotel_files/{hotelId}/")]
        public async Task<IActionResult> GetHotelFIles(long hotelId)
        {
            var response = await _hotelMaster.GetHotelFiles(hotelId);

            return StatusCode(200, new ApiResponse<dynamic>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Success",
                Data = response
            });
        }

    }
}