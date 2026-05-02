namespace TravelAccomodationAPI.ModelClass.RequestModel.VendorMaster
{
    public class AddUpdateVendorMasterRequest
    {
        public int TenantId { get; set; }

        public int? VendorId { get; set; }

        public string Business_Name { get; set; }

        public string Legal_Name { get; set; }

        public string Services { get; set; }

        public byte Star_Rating { get; set; }

        public string AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public int City { get; set; }

        public int State { get; set; }

        public int Country { get; set; }

        public string Pin_Code { get; set; }

        public int Business_Type { get; set; }

        public string UserName { get; set; }
    }
}
