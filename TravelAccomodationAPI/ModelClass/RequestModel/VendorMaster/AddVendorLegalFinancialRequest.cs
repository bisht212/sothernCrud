namespace TravelAccomodationAPI.ModelClass.RequestModel.VendorMaster
{
    public class AddVendorLegalFinancialRequest
    {
        public int TenantId { get; set; }

        public int VendorId { get; set; }

        public string legalName { get; set; }
        public string BankName { get; set; }

        public string AccountNumber { get; set; }

        public string Ifsc_Code { get; set; }

        public decimal Applicable_tds_percent { get; set; }

        public string Pan_Name_Holder { get; set; }

        public string Pan_number { get; set; }

        public string Gst_Registered_Name { get; set; }

        public string Gst_in_number { get; set; }

        public string Msme_certificate_holder_name { get; set; }

        public string Msme_registration_number { get; set; }

        public string Tan_number { get; set; }

    }
}
