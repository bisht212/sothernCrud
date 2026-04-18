using Dapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.Data;
using TravelAccomodationAPI.BusinessClass.Interface;
using TravelAccomodationAPI.DataAccessClass.InterFaces;
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
            UserLoginResponse user = await _da.GetAsync<UserLoginResponse>(sql,parameters);

            if (user != null)
            {
                bool isUserTrue = paaswordHashing.VerifyPassword(auth.Password, user.PasswordHash, user.PasswordSalt);
                if (isUserTrue)
                {
                    string token = _token.CreateToken(user);
                    return token;
                }

                else
                {
                    return "Unauthorised";
                }

            }
            else {
                return "Unauthorised";
            }
          //  throw new Exception();

        }
    }
}
