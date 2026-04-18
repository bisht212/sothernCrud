using Microsoft.AspNetCore.Identity.Data;
using TravelAccomodationAPI.ModelClass.RequestModel;

namespace TravelAccomodationAPI.BusinessClass.Interface
{
    public interface ILoginUser
    {
        public Task<string> Login(AuthenticationRequest  user);
    }
}
