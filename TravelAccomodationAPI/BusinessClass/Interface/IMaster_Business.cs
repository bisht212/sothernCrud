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
        public Task AddAminity(AddAmenitiesRequest aminityRequest);
        public Task UpdateAminity(int amenityId, AddAmenitiesRequest aminityRequest);
        public Task<IEnumerable<GetAminityResponse>> GetAminities();
        public Task<GetAminityResponse> GetAminity(int aminityId);
        public Task<IEnumerable<GetRoomFacilityResponse>> GetHotelRoomFacilities(string? roomFacility);
        public Task<GetRoomFacilityResponse> GetHotelRoomFacility(int roomFacilities_id);
        public Task AddRoomFacility(string roomFacilities);
        public Task UpdateRoomFacility(int roomFacilities_id, string roomFacilities);
        public Task DeleteRoomFacility(int roomFacilities_id);
    }
}
