using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.ResponseModule;

namespace TravelAccomodationAPI.BusinessClass.Interface
{
    public interface IHotelMaster_Business
    {
        public  Task<IEnumerable<HotelMasterResponse>> GetHotelMasterList(HotelFilterRequest hotelFilter);
        public Task<AddHotelMasterResponse> AddhotelsMaster(AddHotelMasterRequest user);
        // public Task<int> AddrestaurantsOnProperty(List<AddRestaurantsOnPropertyRequest> resturantRequest);
        public Task<IEnumerable<GetRestaurantResponse>> GetRestaurantDetail(long hotelId);  
        public Task InsertRestaurantsWithFiles(List<AddRestaurantsOnPropertyRequest> request);
        public Task UpdateRestaurantsWithFiles(int rest_Id, AddRestaurantsOnPropertyRequest request);
        public Task<dynamic> DeleteRestaurant(int rest_Id);
        public Task AddHotelContacts(List<AddHotelContacts> hotelContacts);
        public Task UpdateHotelContacts(int ContactId, AddHotelContacts hotelContacts);
        public Task<dynamic> DeleteHotelContact(int contact_id);
        public Task AddHotelContactPhoneNumber(AddHotelContactPhoneNumberRequest phoneNumberRequest);
        public Task<dynamic> DeletePhoneId(long phone_Id);
        public Task AddHotelContactEmail(AddHotelContactEmailRequest emailRequest);
        public Task<dynamic> DeleteEmailId(long email_Id);
        public Task<IEnumerable<HotelFacilityCategoryResponse>> GetHotelFacilityCategory();
        public Task<HotelFacilityCategoryResponse> GetHotelFacilityCategoryByID(int facility_CategoryId);
        public Task AddHotelFacility(List<AddHotelFacilitiesRequest> hotelFacilityRequest);
        public Task<IEnumerable<GetBanquetResponse>> GetBanqueteByHotelId(long hotelId);
        public Task InsertBanquetWithFiles(List<AddBanquestRequest> request);
        public Task UpdateBanquetWithFiles(int banquetId, AddBanquestRequest request);
        public Task<dynamic> DeleteBanquete(long banquete_Id);
        public Task<dynamic> BulkUploadHotelFilesAsync(List<AddHotelFilesRequest> files);
        public Task<dynamic> UpdateHotelFIle(long fileId, AddHotelFilesRequest file);
        public Task<dynamic> DeleteHotelFile(long fileId, long hotelId);
        public Task<IEnumerable<GetHotelFilesResponse>> GetHotelFiles(long hotelId);
        public Task<dynamic> HotelMasterAsDraft(long hotelId, bool isDraft, string updatedBy);

    }
}
