using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.ResponseModule;

namespace TravelAccomodationAPI.BusinessClass.Interface
{
    public interface IMaster_Business
    {
        public Task<IEnumerable<GetVeg_NonVegResponse>> GetAll_Veg_Non_Veg();
        public Task<GetVeg_NonVegResponse> Get_Veg_Non_Veg(int Id);
        public Task<IEnumerable<GetCuisine>> GetAll_Cuisine();
        public Task<GetCuisine> Get_Cuisine(int Id);
        public Task AddPhoneType(AddPhoneType phoneTypeRequest);
        public Task<IEnumerable<PhoneTypeResponse>> GetPhoneTypes();
        public Task<PhoneTypeResponse> GetPhoneType(int phoneTypeId);
    }
}
