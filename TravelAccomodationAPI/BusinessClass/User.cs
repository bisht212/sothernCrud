using Dapper;
using System.Linq.Expressions;
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

        public async Task<int> UpdateUserDetail(int userId, UpdateUser user)
        {
            try {
                int response = 0;
                var sql = "GetUserDetailById";
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                var userdetail = await _da.GetAsync<GetUser>(sql, parameters);
                if (userdetail != null)
                {
                   
                    var updateSql = "UpdateUserDetail";

                    var parameters1 = new DynamicParameters();
                    parameters1.Add("@UserId", userId);
                    parameters1.Add("@FirstName", user.FirstName);
                    parameters1.Add("@LastName", user.LastName);
                    parameters1.Add("@Age", user.Age);
                    parameters1.Add("@Email", user.Email);
                    parameters1.Add("@Mobile", user.Mobile);
                    parameters1.Add("@ModifiedAt", DateTime.Now);
                    parameters1.Add("@ModifiedBy", user.FirstName);

                    response = await _da.ExecuteAsync(updateSql, parameters1);
                   
                }
                return response;
            }
                 catch (Exception ex)
            {
                throw new Exception("Failed to update user", ex);
            }


        }

        public async Task<int> DeleteUserDetail(int userId)
        {
            try
            {
                int response = 0;
                var sql = "GetUserDetailById";
                var parameters = new DynamicParameters();
                parameters.Add("@UserId", userId);
                var userdetail = await _da.GetAsync<GetUser>(sql, parameters);
                if (userdetail != null)
                {

                    var updateSql = "DeleteUserDetail";

                    var parameters1 = new DynamicParameters();
                    parameters1.Add("@UserId", userId);
                   
                    response = await _da.ExecuteAsync(updateSql, parameters1);

                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update user", ex);
            }
        }
    }
    }

