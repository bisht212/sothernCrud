using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using TravelAccomodationAPI.BusinessClass.Interface;
using TravelAccomodationAPI.ModelClass;
using TravelAccomodationAPI.ModelClass.RequestModel;

namespace TravelAccomodationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginUser _loginUser; 
        public LoginController(ILoginUser loginUser)
        {
            _loginUser = loginUser; 
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationRequest user) {

            try {
                string token = await _loginUser.Login(user);
                var response = new ApiResponse<string>
                {
                    StatusCode = 201,
                    IsError = false,
                    Message =  "Token",
                    Data = token
                };
                return Ok(response);
            }
            catch(Exception ex) 
            
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
