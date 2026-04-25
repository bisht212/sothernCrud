using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.ResponseModule;

namespace TravelAccomodationAPI.BusinessClass.Interface
{
    public interface IHotelMaster_Business
    {
        public  Task<IEnumerable<HotelMasterResponse>> GetHotelMasterList(HotelFilterRequest hotelFilter);
        public Task<AddHotelMasterResponse> AddhotelsMaster(AddHotelMasterRequest user);

        public Task<IEnumerable<GetVeg_NonVegResponse>> GetAll_Veg_Non_Veg();

        public Task<GetVeg_NonVegResponse> Get_Veg_Non_Veg(int Id);

        public Task<IEnumerable<GetCuisine>> GetAll_Cuisine();

        public Task<GetCuisine> Get_Cuisine(int Id);

        // public Task<int> AddrestaurantsOnProperty(List<AddRestaurantsOnPropertyRequest> resturantRequest);

        public Task InsertRestaurantsWithFiles(List<AddRestaurantsOnPropertyRequest> request);

        public Task UpdateRestaurantsWithFiles(int rest_Id, AddRestaurantsOnPropertyRequest request);

        public Task<dynamic> DeleteRestaurant(int rest_Id);
        public Task AddHotelContacts(List<AddHotelContacts> hotelContacts);

        public Task UpdateHotelContacts(int ContactId, AddHotelContacts hotelContacts);
    }
}
