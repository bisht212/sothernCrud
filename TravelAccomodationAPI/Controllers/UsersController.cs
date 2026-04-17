using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TravelAccomodationAPI.BusinessClass;
using TravelAccomodationAPI.BusinessClass.Interface;
using TravelAccomodationAPI.ModelClass;
using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.ResponseModule;

namespace TravelAccomodationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase

    {

        private readonly IUsers _user;    

        public UsersController(IUsers user)
        {
            _user = user; 
        }


        [HttpGet("GetUserList")]
     
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var result = await _user.GetUserList();

              
                var response = new ApiResponse<IEnumerable<GetUser>>
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
                var errorResponse = new ApiResponse<IEnumerable<User>>
                {
                    StatusCode = 500,
                    IsError = true,
                    Message = ex.Message, // Or "An internal error occurred"
                    Data = null
                };

                return StatusCode(500, errorResponse);
            }
       
        }


        [HttpPost("CreateUsers")]
        public async Task<IActionResult> CreateUser(AddUser user)
        {
            try
            {
                var result = await _user.AddUser(user);

                if (result > 0)
                {
                    var response = new ApiResponse<int>
                    {
                        StatusCode = 201,
                        IsError = false,
                        Message = "User created successfully",
                    };

                
                    return Created();
                }

                return BadRequest(new ApiResponse<int>
                {
                    StatusCode = 400,
                    IsError = true,
                    Message = "User could not be added."
                });
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