using Dapper;
using TravelAccomodationAPI.BusinessClass.Interface;
using TravelAccomodationAPI.DataAccessClass.InterFaces;
using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.ResponseModule;
using TravelAccomodationAPI.Shared.CommonMethods;

namespace TravelAccomodationAPI.BusinessClass
{
    public class User : IUsers
    {
        private readonly IDataAccess _da; 
        public User(IDataAccess da)
             
        {
            _da = da;   
        }

        public async Task<IEnumerable<GetUser>> GetUserList()
        {
            try
            {
                string sqlSP = "[GetALLUsers]";
                var response = await _da.GetListAsync<GetUser>(sqlSP);

                return response.ToList();
            }

            catch (Exception ex) {

                throw new Exception("Failed to retrieve the user list.", ex);
            }
           
            
        }

        public async Task<int> AddUser(AddUser user)
        {
            try {
                string sql = "AddUser";
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", user.UserId);
                parameters.Add("@FirstName", user.FirstName);
                parameters.Add("@LastName", user.LastName);
                parameters.Add("@Age", user.Age);
                parameters.Add("@Email", user.Email);
                parameters.Add("@Mobile", user.Mobile);
                parameters.Add("@CreatedAt", user.CreatedAt);
                parameters.Add("@CreatedBy", user.CreatedBy);
                parameters.Add("@ModifiedAt", user.@ModifiedAt);
                parameters.Add("@ModifiedBy", user.@ModifiedBy);

                var response = await _da.ExecuteAsync(sql, parameters);

                if (response > 0) {
                    var (hash, salt) = paaswordHashing.HashPassword(user.Password);
                    string createLogin = "AddUserLogins";
                    var parameter = new DynamicParameters();
                    parameter.Add("@UserId", user.UserId);
                    parameter.Add("@passwordHash", hash);
                    parameter.Add("@passwordSalt", salt);
                    var loginPassword = await _da.ExecuteAsync(createLogin, parameter);

                }
                return response; 

            }
            catch (Exception ex) {
                throw new Exception("Failed to add user", ex);
            }
        }



    }
}
