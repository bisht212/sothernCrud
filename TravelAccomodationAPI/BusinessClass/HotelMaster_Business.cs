using Dapper;
using System.Data;
using TravelAccomodationAPI.BusinessClass.Interface;
using TravelAccomodationAPI.DataAccessClass.InterFaces;
using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.ResponseModule;
using TravelAccomodationAPI.Shared.CommonMethods;
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
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TenantID", hotelFilter.TenantId, DbType.Int32);

                parameters.Add("@hotel_name", hotelFilter.HotelName, DbType.String);
                var response = await _da.GetListAsync<HotelMasterResponse>(Stored_Procedures.GET_HOTEL_MASTER, parameters);

                return response.ToList();
            }

            catch (Exception ex)
            {

                throw new Exception("Failed to retrieve the user list.", ex);
            }


        }


        public async Task<AddHotelMasterResponse> AddhotelsMaster(AddHotelMasterRequest hotel)
        {
            try
            {
                var parameters = new DynamicParameters();

                parameters.Add("@TenantID", hotel.TeanatId, DbType.Int32);

                parameters.Add("@hotel_name", hotel.Hotel_Name, DbType.String);
                parameters.Add("@propertyType_id", hotel.Property_Type_Id, DbType.Int32);
                parameters.Add("@starRating_id", hotel.Star_Rating_Id, DbType.Int32);

                parameters.Add("@owner_name", hotel.Owner_Name, DbType.String);
                parameters.Add("@owner_phone", hotel.Owner_Phone, DbType.String);

                parameters.Add("@chainstand_id", hotel.ChainStandId, DbType.Int32);

                parameters.Add("@address_line1", hotel.AddressLine1, DbType.String);
                parameters.Add("@address_line2", hotel.AddressLine2, DbType.String);

                parameters.Add("@city", hotel.City, DbType.Int32);
                parameters.Add("@state", hotel.State, DbType.Int32);
                parameters.Add("@country", hotel.Country, DbType.Int32);
                parameters.Add("@pin_code", hotel.PinCode, DbType.String);

                parameters.Add("@landmark", hotel.LandMark, DbType.String);

                parameters.Add("@year_of_construction", hotel.Year_of_construction, DbType.Int32);
                parameters.Add("@number_of_floors", hotel.number_of_floors, DbType.Int32);
                parameters.Add("@number_of_rooms", hotel.number_of_rooms, DbType.Int32);

                parameters.Add("@check_in_time", hotel.Check_In_Time, DbType.Time);
                parameters.Add("@check_out_time", hotel.Check_Out_Time, DbType.Time);

                parameters.Add("@how_to_reach", hotel.How_To_Reach, DbType.String);

                parameters.Add("@google_map_link", hotel.Google_Map_Link, DbType.String);
                parameters.Add("@google_hotel_link", hotel.Google_Hotel_Link, DbType.String);
                parameters.Add("@trip_advisor_link", hotel.Trip_Advisor_Link, DbType.String);

                parameters.Add("@distance_from_airport", hotel.Distance_From_Airport, DbType.String);
                parameters.Add("@distance_from_railway", hotel.Distance_From_Railway, DbType.String);
                parameters.Add("@distance_from_isbt", hotel.Distance_From_ISBT, DbType.String);

                parameters.Add("@created_by", hotel.Created_By, DbType.String);

                var response = await _da.GetAsync<AddHotelMasterResponse>(
                    Stored_Procedures.ADD_HOTEL_MASTER,
                    parameters
                );

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add Hotel", ex);
            }
        }

    }
}
