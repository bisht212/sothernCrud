using TravelAccomodationAPI.ModelClass.RequestModel.VendorMaster;
using TravelAccomodationAPI.ModelClass.ResponseModule.VendorMaster;

namespace TravelAccomodationAPI.BusinessClass.Interface
{
    public interface IVendorMaster_Business
    {
        public Task<VendorMasterResponse> AddVendorMaster(AddUpdateVendorMasterRequest vendorMasterRequest);
    }
}
