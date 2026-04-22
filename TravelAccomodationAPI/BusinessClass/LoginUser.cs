using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using TravelAccomodationAPI.BusinessClass.Interface;
using TravelAccomodationAPI.DataAccessClass.InterFaces;
using TravelAccomodationAPI.ModelClass;
using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.ResponseModule;
using TravelAccomodationAPI.Shared.CommonMethods;
using TravelAccomodationAPI.TokenCreateClass.InterFaces;

namespace TravelAccomodationAPI.BusinessClass
{
    public class LoginUser : ILoginUser
    {

        private readonly IDataAccess _da;
        private readonly IToken _token; 

        public LoginUser(IDataAccess da, IToken token)
        {
            _da = da; 
            _token = token;
        }
        public async Task<string> Login(AuthenticationRequest auth)
        {
            string sql = "AuthenticateUser";

            var parameters = new DynamicParameters();
            parameters.Add("@Email", auth.Email);

            var user = await _da.GetAsync<UserLoginResponse>(sql, parameters);

            if (user == null)
            {
                throw new ApiException("Invalid email or password", 401);
            }

            bool isValid = paaswordHashing.VerifyPassword(
                auth.Password,
                user.PasswordHash,
                user.PasswordSalt);

            if (!isValid)
            {
                throw new ApiException("Invalid email or password", 401);
            }

            string token = _token.CreateToken(user);

            return token;
        }

    }
}
