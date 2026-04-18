using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.ResponseModule;

namespace TravelAccomodationAPI.BusinessClass.Interface
{
    public interface IUsers
    {
        public Task<IEnumerable<GetUser>> GetUserList();

        public Task<int> AddUser(AddUser user);

        public Task<int> UpdateUserDetail(int userId , UpdateUser user);

    }
}
