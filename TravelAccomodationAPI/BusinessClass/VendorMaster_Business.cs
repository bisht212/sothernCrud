using Dapper;
using TravelAccomodationAPI.BusinessClass.Interface;
using TravelAccomodationAPI.DataAccessClass.InterFaces;
using TravelAccomodationAPI.ModelClass;
using TravelAccomodationAPI.ModelClass.RequestModel.VendorMaster;
using TravelAccomodationAPI.ModelClass.ResponseModule;
using TravelAccomodationAPI.ModelClass.ResponseModule.VendorMaster;
using TravelAccomodationAPI.Shared.CommonMessage;
using TravelAccomodationAPI.Shared.Enums;
using TravelAccomodationAPI.Shared.StoredProcedures;

namespace TravelAccomodationAPI.BusinessClass
{
    public class VendorMaster_Business : IVendorMaster_Business
    {
        private readonly IDataAccess _da;

        public VendorMaster_Business(IDataAccess da)
        {
            _da = da;
        }
        public async Task<VendorMasterResponse> AddVendorMaster(AddUpdateVendorMasterRequest vendorMasterRequest)
        {
            // throw new NotImplementedException();
            var parameters = new DynamicParameters();
            parameters.Add("@TenantID", vendorMasterRequest.TenantId);
            parameters.Add("@business_name", vendorMasterRequest.Business_Name);
            parameters.Add("@legal_name", vendorMasterRequest.Legal_Name);
            parameters.Add("@services", vendorMasterRequest.Services);
            parameters.Add("@star_rating", vendorMasterRequest.Star_Rating);
            parameters.Add("@address_line1", vendorMasterRequest.AddressLine1);
            parameters.Add("@address_line2", vendorMasterRequest.AddressLine2);
            parameters.Add("@city", vendorMasterRequest.City);
            parameters.Add("@state", vendorMasterRequest.State);
            parameters.Add("@country", vendorMasterRequest.Country);
            parameters.Add("@pin_code", vendorMasterRequest.Pin_Code);
            parameters.Add("@business_type", vendorMasterRequest.Business_Type);
            parameters.Add("@UserName", vendorMasterRequest.UserName);

            var response = await _da.GetAsync<VendorMasterResponse>(
                Stored_Procedures.ADD_VENDOR,
                parameters);

            if (response == null)
            {
                throw new ApiException(ErrorMessage.HOTEL_ADD_FAILES, Convert.ToInt32(StatusCode.Badrequest));
            }

            return response;

        }
    }
}
