namespace TravelAccomodationAPI.ModelClass.RequestModel.VendorMaster
{
    public class AddVendorContact
    {
        public int TenantId { get; set; }

        public int VendorId { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int Department { get; set; }

        public int Designation { get; set; }
    }
}
