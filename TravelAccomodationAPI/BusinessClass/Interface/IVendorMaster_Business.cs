using TravelAccomodationAPI.ModelClass.RequestModel.VendorMaster;
using TravelAccomodationAPI.ModelClass.ResponseModule.VendorMaster;

namespace TravelAccomodationAPI.BusinessClass.Interface
{
    public interface IVendorMaster_Business
    {
        public Task<VendorMasterResponse> AddVendorMaster(AddUpdateVendorMasterRequest vendorMasterRequest);

        public Task<VendorMasterResponse> UpdateVendorMaster(int vendorId, AddUpdateVendorMasterRequest vendorMasterRequest);

        public Task AddVendorContact(List<AddVendorContact> contactRequest);

        public Task UpdateVendorContact(int contactId, AddVendorContact contactRequest);

        public Task<int> AddVendorLegalFinancial(AddVendorLegalFinancialRequest financialRequest); 

        public Task<int> UpdateVendorLegalFinancial(int VendorLegalFinancialid, AddVendorLegalFinancialRequest financialRequest);


    }
}
