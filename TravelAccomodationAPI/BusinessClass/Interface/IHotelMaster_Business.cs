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
        public Task<dynamic> DeleteHotelContact(int contact_id);
        public Task AddPhoneType(AddPhoneType phoneTypeRequest);
        public Task<IEnumerable<PhoneTypeResponse>> GetPhoneTypes();
        public Task<PhoneTypeResponse> GetPhoneType(int phoneTypeId);
        public Task AddHotelContactPhoneNumber(AddHotelContactPhoneNumberRequest phoneNumberRequest);
        public Task<dynamic> DeletePhoneId(long phone_Id);
        public Task AddHotelContactEmail(AddHotelContactEmailRequest emailRequest);
        public Task<dynamic> DeleteEmailId(long email_Id);
        public Task AddAminity(AddAmenitiesRequest aminityRequest);
        public Task UpdateAminity(int amenityId, AddAmenitiesRequest aminityRequest);
        public Task<IEnumerable<GetAminityResponse>> GetAminities();
        public Task<GetAminityResponse> GetAminity(int aminityId);
        public Task<IEnumerable<HotelFacilityCategoryResponse>> GetHotelFacilityCategory();
        public Task<HotelFacilityCategoryResponse> GetHotelFacilityCategoryByID(int facility_CategoryId);
        public Task AddHotelFacility(List<AddHotelFacilitiesRequest> hotelFacilityRequest);
        public Task InsertBanquetWithFiles(List<AddBanquestRequest> request);
        public Task UpdateBanquetWithFiles(int banquetId, AddBanquestRequest request);

    }
}
