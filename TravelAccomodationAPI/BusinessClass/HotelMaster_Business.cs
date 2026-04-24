using Dapper;
using System.Data;
using TravelAccomodationAPI.BusinessClass.Interface;
using TravelAccomodationAPI.DataAccessClass.InterFaces;
using TravelAccomodationAPI.ModelClass;
using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.ResponseModule;
using TravelAccomodationAPI.Shared.CommonMessage;
using TravelAccomodationAPI.Shared.CommonMethods;
using TravelAccomodationAPI.Shared.DBHelper;
using TravelAccomodationAPI.Shared.Enums;
using TravelAccomodationAPI.Shared.StoredProcedures;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TravelAccomodationAPI.BusinessClass
{
    public class HotelMaster_Business : IHotelMaster_Business
    {

        private readonly IDataAccess _da;
        private readonly DbContext _context;
        public HotelMaster_Business(IDataAccess da, DbContext context)
        {
            _da = da;
            _context = context;
        }

        public async Task<IEnumerable<HotelMasterResponse>> GetHotelMasterList(HotelFilterRequest hotelFilter)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TenantID", hotelFilter.TenantId);
            parameters.Add("@hotel_name", hotelFilter.HotelName);

            var result = await _da.GetListAsync<HotelMasterResponse>(
                Stored_Procedures.GET_HOTEL_MASTER, parameters);

            if (result == null)
            {
                throw new ApiException(
                    string.Format(ErrorMessage.HOTEL_NOT_FOUND, hotelFilter.TenantId), 404);
            }

            return result;
        }

        public async Task<AddHotelMasterResponse> AddhotelsMaster(AddHotelMasterRequest hotel)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@TenantID", hotel.TeanatId);
            parameters.Add("@hotel_name", hotel.Hotel_Name);
            parameters.Add("@propertyType_id", hotel.Property_Type_Id);
            parameters.Add("@starRating_id", hotel.Star_Rating_Id);

            parameters.Add("@owner_name", hotel.Owner_Name);
            parameters.Add("@owner_phone", hotel.Owner_Phone);

            parameters.Add("@chainstand_id", hotel.ChainStandId);

            parameters.Add("@address_line1", hotel.AddressLine1);
            parameters.Add("@address_line2", hotel.AddressLine2);

            parameters.Add("@city", hotel.City);
            parameters.Add("@state", hotel.State);
            parameters.Add("@country", hotel.Country);
            parameters.Add("@pin_code", hotel.PinCode);

            parameters.Add("@landmark", hotel.LandMark);

            parameters.Add("@year_of_construction", hotel.Year_of_construction);
            parameters.Add("@number_of_floors", hotel.number_of_floors);
            parameters.Add("@number_of_rooms", hotel.number_of_rooms);

            parameters.Add("@check_in_time", hotel.Check_In_Time);
            parameters.Add("@check_out_time", hotel.Check_Out_Time);

            parameters.Add("@how_to_reach", hotel.How_To_Reach);

            parameters.Add("@google_map_link", hotel.Google_Map_Link);
            parameters.Add("@google_hotel_link", hotel.Google_Hotel_Link);
            parameters.Add("@trip_advisor_link", hotel.Trip_Advisor_Link);

            parameters.Add("@distance_from_airport", hotel.Distance_From_Airport);
            parameters.Add("@distance_from_railway", hotel.Distance_From_Railway);
            parameters.Add("@distance_from_isbt", hotel.Distance_From_ISBT);

            parameters.Add("@created_by", hotel.Created_By);

            var response = await _da.GetAsync<AddHotelMasterResponse>(
                Stored_Procedures.ADD_HOTEL_MASTER,
                parameters);

            if (response == null)
            {
                throw new ApiException(ErrorMessage.HOTEL_ADD_FAILES , Convert.ToInt32(StatusCode.Badrequest));
            }

            return response;
        }

        public async Task<IEnumerable<GetVeg_NonVegResponse>> GetAll_Veg_Non_Veg()
        {
            var result = await _da.GetListAsync<GetVeg_NonVegResponse>(
                Stored_Procedures.GET_ALL_VEG_NON_VEG);

            return result;
        }

        public async Task<GetVeg_NonVegResponse> Get_Veg_Non_Veg(int Id)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@veg_id", Id);
            var result = await _da.GetAsync<GetVeg_NonVegResponse>(
                  Stored_Procedures.GET_VEG_NON_VEG, parameters);

            return result;
        }

        public async Task<IEnumerable<GetCuisine>> GetAll_Cuisine()
        {
            var result = await _da.GetListAsync<GetCuisine>(
                 Stored_Procedures.GET_ALL_CUISINE);

            return result;
        }

        public async Task<GetCuisine> Get_Cuisine(int Id)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@cuisine_id", Id);
            var result = await _da.GetAsync<GetCuisine>(
                  Stored_Procedures.GET_VEG_NON_VEG, parameters);

            return result;
        }

        public async Task InsertRestaurantsWithFiles(List<AddRestaurantsOnPropertyRequest> request)
        {
            //using var connection = _context.GetConnection();
            //using var transaction = connection.BeginTransaction();

            //try
            //{
                foreach (var item in request)
                {
                    var param = new DynamicParameters();
                    param.Add("@Hotel_Id", item.Hotel_Id);
                    param.Add("@Resta_Name", item.Resta_Name);
                    param.Add("@Veg_Id", item.Veg_Id);
                    param.Add("@Cuisine_Id", item.Cuisine_Id);
                    param.Add("@No_of_covers", item.No_of_covers);
                    param.Add("@In_room_dining_facility", item.In_room_dining_facility);

                    var restaId = await _da.ExecuteScalarAsync<int>(
                        "usp_Bulk_RestaurantsOnProperty_Insert",
                        param
                    );
                if(restaId > 0) {
                    if (item.ResturantImage != null && item.ResturantImage.Any())
                    {
                        foreach (var file in item.ResturantImage)
                        {
                            var filePath = await FileUploadCommon.UploadFileAsync(file);

                            var fileParam = new DynamicParameters();
                            fileParam.Add("@HotelId", item.Hotel_Id);
                            fileParam.Add("@RestaurantId", restaId);
                            fileParam.Add("@FilePath", filePath);
                            fileParam.Add("@CreatedBy", "Admin");

                            await _da.ExecuteAsync(
                                "sp_InsertFile",
                                fileParam
                            );
                        }
                    }
                }

                 
                }

            //    transaction.Commit();
            //}
            //catch
            //{
            //    transaction.Rollback();
            //    throw;
            //}
        }

    }
}
