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
        public async Task<IActionResult> Login(AuthenticationRequest user)
        {
            var token = await _loginUser.Login(user);

            return Ok(new ApiResponse<string>
            {
                StatusCode = 200,
                IsError = false,
                Message = "Token generated successfully",
                Data = token
            });
        }
    }
}
