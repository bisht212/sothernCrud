using Dapper;
using TravelAccomodationAPI.BusinessClass.Interface;
using TravelAccomodationAPI.DataAccessClass.InterFaces;
using TravelAccomodationAPI.ModelClass.RequestModel;
using TravelAccomodationAPI.ModelClass.ResponseModule;
using TravelAccomodationAPI.Shared.DBHelper;
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


    }
}
