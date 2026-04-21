using Dapper;
using System.Data;
using TravelAccomodationAPI.BusinessClass.Interface;
using TravelAccomodationAPI.DataAccessClass.InterFaces;
using TravelAccomodationAPI.ModelClass;
using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.ResponseModule;
using TravelAccomodationAPI.Shared.CommonMessage;
using TravelAccomodationAPI.Shared.CommonMethods;
using TravelAccomodationAPI.Shared.Enums;
using TravelAccomodationAPI.Shared.StoredProcedures;

namespace TravelAccomodationAPI.BusinessClass
{
    public class HotelMaster_Business : IHotelMaster_Business
    {

        private readonly IDataAccess _da;

        public HotelMaster_Business(IDataAccess da)
        {
            _da = da; 
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
    }
}
