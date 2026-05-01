using Dapper;
using System.Data;
using TravelAccomodationAPI.BusinessClass.Interface;
using TravelAccomodationAPI.DataAccessClass.InterFaces;
using TravelAccomodationAPI.ModelClass;
using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.ResponseModule;
using TravelAccomodationAPI.Shared.DBHelper;
using TravelAccomodationAPI.Shared.Enums;
using TravelAccomodationAPI.Shared.StoredProcedures;

namespace TravelAccomodationAPI.BusinessClass
{
    public class Master_Business : IMaster_Business
    {
        private readonly IDataAccess _da;
        private readonly DbContext _context;

        public Master_Business(IDataAccess da, DbContext context)
        {
            _da = da;
            _context = context; 
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

        public async Task AddPhoneType(AddPhoneType phoneTypeRequest)
        {
            var param = new DynamicParameters();
            param.Add("@phonetype", phoneTypeRequest.PhoneType);

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                   Stored_Procedures.ADD_PHONE_TYPE,
                   param
               );
        }

        public async Task<IEnumerable<PhoneTypeResponse>> GetPhoneTypes()
        {

            IEnumerable<PhoneTypeResponse> result = await _da.GetListAsync<PhoneTypeResponse>(
                Stored_Procedures.GET_PHONE_TYPES);

            return result.ToList();
        }

        public async Task<PhoneTypeResponse> GetPhoneType(int phoneTypeId)
        {
            var param = new DynamicParameters();
            param.Add("@phonetype_id", phoneTypeId);

            PhoneTypeResponse result = await _da.GetAsync<PhoneTypeResponse>(
               Stored_Procedures.GET_PHONE_TYPE, param);

            return result;
        }

        public async Task AddAminity(AddAmenitiesRequest aminityRequest)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@AmenityName", aminityRequest.AmenityName, DbType.String);
            parameters.Add("@SortOrder", aminityRequest.SortOrder, DbType.Int32);

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                   Stored_Procedures.ADD_AMINITY,
                   parameters
               );

            if (result.Status <= 0)
            {
                throw new ApiException(
                    result.Message,
                    Convert.ToInt32(StatusCode.Badrequest)
                );
            }
        }

        public async Task UpdateAminity(int amenityId, AddAmenitiesRequest aminityRequest)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@AmenityId", amenityId, DbType.Int32);
            parameters.Add("@AmenityName", aminityRequest.AmenityName, DbType.String);
            parameters.Add("@SortOrder", aminityRequest.SortOrder, DbType.Int32);

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                Stored_Procedures.UPDATE_AMINITY,
                parameters
            );

            if (result.Status <= 0)
            {
                throw new ApiException(
                    result.Message,
                    Convert.ToInt32(StatusCode.Badrequest)
                );
            }
        }

        public async Task<IEnumerable<GetAminityResponse>> GetAminities()
        {
            var result = await _da.GetListAsync<GetAminityResponse>(
               Stored_Procedures.GET_AMINITIES);

            return result.ToList();
        }

        public async Task<GetAminityResponse> GetAminity(int aminityId)
        {
            var param = new DynamicParameters();
            param.Add("@AmenityID", aminityId);

            GetAminityResponse result = await _da.GetAsync<GetAminityResponse>(
               Stored_Procedures.GET_AMINITY, param);

            return result;
        }

        public async Task<IEnumerable<GetRoomFacilityResponse>> GetHotelRoomFacilities(string? roomFacility)
        {
                 var parameters = new DynamicParameters();

            parameters.Add("@roomFacilities_name", roomFacility, DbType.String);
            var result = await _da.GetListAsync<GetRoomFacilityResponse>(
               Stored_Procedures.GET_HOTEL_FACILITIES);

            return result.ToList();
        }

        public async Task<GetRoomFacilityResponse> GetHotelRoomFacility(int roomFacilities_id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@roomFacilities_id", roomFacilities_id);

            var result = await _da.GetAsync<GetRoomFacilityResponse>(
               Stored_Procedures.GET_HOTEL_FACILITY, parameters);

            return result;
        }

        public async Task AddRoomFacility(string roomFacilities)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@roomFacilities", roomFacilities);

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                   Stored_Procedures.UPDATE_HOTEL_FACILITY,
                   parameters
               );

            if (result.Status <= 0)
            {
                throw new ApiException(
                    result.Message,
                    Convert.ToInt32(StatusCode.Badrequest)
                );
            }
        }

        public async Task UpdateRoomFacility(int roomFacilities_id, string roomFacilities)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@roomFacilities_id", roomFacilities_id);
            parameters.Add("@roomFacilities", roomFacilities);
          

            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                Stored_Procedures.UPDATE_HOTEL_FACILITY,
                parameters
            );

            if (result.Status <= 0)
            {
                throw new ApiException(
                    result.Message,
                    Convert.ToInt32(StatusCode.Badrequest)
                );
            }
        }

        public async Task DeleteRoomFacility(int roomFacilities_id)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@roomFacilities_id", roomFacilities_id);


            var result = await _da.ExecuteWithResponseAsync<dynamic>(
                Stored_Procedures.DELETE_HOTEL_FACILITY,
                parameters
            );

            if (result.Status <= 0)
            {
                throw new ApiException(
                    result.Message,
                    Convert.ToInt32(StatusCode.Badrequest)
                );
            }
        }
    }
}
