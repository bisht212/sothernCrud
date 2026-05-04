using Dapper;
using System.Data;
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


        public async Task<VendorMasterResponse> UpdateVendorMaster(int vendorId, AddUpdateVendorMasterRequest vendorMasterRequest)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TenantID", vendorMasterRequest.TenantId);
            parameters.Add("@VendorID", vendorId);
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

            return response;
        }

        public async Task AddVendorContact(List<AddVendorContact> contactRequest)
        {
            var param = new DynamicParameters();
            foreach (var request in contactRequest)
            {

                param.Add("@TenantID", request.TenantId);
                param.Add("@VendorID", request.VendorId);
                param.Add("@full_name", request.FullName);
                param.Add("@phone", request.Phone);
                param.Add("@email", request.Email);
                param.Add("@department", request.Department);
                param.Add("@designation", request.Designation);

                //  Output param
                param.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

                // ❗ This will throw SqlException if SP throws
                await _da.ExecuteAsync(Stored_Procedures.ADD_UPDATE_VENDOR, param);

                // ✅ Only runs if no exception
                var isSuccess = param.Get<bool>("@IsSuccess");


            }

        }

        public async Task UpdateVendorContact(int contactId, AddVendorContact contactRequest)
        {
            var param = new DynamicParameters();

            param.Add("@@VendorContactId", contactId);
            param.Add("@TenantID", contactRequest.TenantId);
            param.Add("@VendorID", contactRequest.VendorId);
            param.Add("@full_name", contactRequest.FullName);
            param.Add("@phone", contactRequest.Phone);
            param.Add("@email", contactRequest.Email);
            param.Add("@department", contactRequest.Department);
            param.Add("@designation", contactRequest.Designation);

            //  Output param
            param.Add("@IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);

            // ❗ This will throw SqlException if SP throws
            await _da.ExecuteAsync(Stored_Procedures.ADD_UPDATE_VENDOR, param);

            // ✅ Only runs if no exception
            var isSuccess = param.Get<bool>("@IsSuccess");
        }

        public async Task<int> AddVendorLegalFinancial(AddVendorLegalFinancialRequest financialRequest)
        {
            var param = new DynamicParameters();

            param.Add("@TenantID", financialRequest.TenantId);
            param.Add("@VendorID", financialRequest.VendorId);
            param.Add("@legalName", financialRequest.legalName);
            param.Add("@bank_name", financialRequest.BankName);
            param.Add("@account_number", financialRequest.AccountNumber);
            param.Add("@ifsc_code", financialRequest.Ifsc_Code);
            param.Add("@applicable_tds_percent", financialRequest.Applicable_tds_percent);
            param.Add("@pan_holder_name", financialRequest.Pan_Name_Holder);
            param.Add("@pan_number", financialRequest.Pan_number);
            param.Add("@gst_registered_name", financialRequest.Gst_Registered_Name);
            param.Add("@gstin_number", financialRequest.Gst_in_number);
            param.Add("@msme_certificate_holder_name", financialRequest.Msme_certificate_holder_name);
            param.Add("@msme_registration_number", financialRequest.Msme_registration_number);
            param.Add("@tan_number", financialRequest.Tan_number);

            int result = await _da.ExecuteAsync(Stored_Procedures.ADD_UPDATE_VENDOR_FINANCLIAL_LIAGAL, param);

            if (result <= 0) {
                throw new ApiException(
                    string.Format(ErrorMessage.VENDOR_LegalFinancial_NOT_INSERTED), 400);
            }
            return result; 
        }

        public async Task<int> UpdateVendorLegalFinancial(int vendorLegalFinancialid, AddVendorLegalFinancialRequest financialRequest)
        {
            var param = new DynamicParameters();

            param.Add("@VendorLegalFinancialid", vendorLegalFinancialid);
            param.Add("@TenantID", financialRequest.TenantId);
            param.Add("@VendorID", financialRequest.VendorId);
            param.Add("@bank_name", financialRequest.BankName);
            param.Add("@account_number", financialRequest.AccountNumber);
            param.Add("@ifsc_code", financialRequest.Ifsc_Code);
            param.Add("@applicable_tds_percent", financialRequest.Applicable_tds_percent);
            param.Add("@pan_holder_name", financialRequest.Pan_Name_Holder);
            param.Add("@pan_number", financialRequest.Pan_number);
            param.Add("@gst_registered_name", financialRequest.Gst_Registered_Name);
            param.Add("@gstin_number", financialRequest.Gst_in_number);
            param.Add("@msme_certificate_holder_name", financialRequest.Msme_certificate_holder_name);
            param.Add("@msme_registration_number", financialRequest.Msme_registration_number);
            param.Add("@tan_number", financialRequest.Tan_number);

            int result = await _da.ExecuteAsync(Stored_Procedures.ADD_UPDATE_VENDOR_FINANCLIAL_LIAGAL, param);

            if (result <= 0)
            {
                throw new ApiException(
                    string.Format(ErrorMessage.VENDOR_LegalFinancial_NOT_INSERTED), 400);
            }
            return result;
        }
    }
}
