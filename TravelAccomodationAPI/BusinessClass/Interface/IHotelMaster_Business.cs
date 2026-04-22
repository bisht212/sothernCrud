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

        public Task<int> AddrestaurantsOnProperty(AddRestaurantsOnPropertyRequest resturantRequest);
    }
}
