namespace TravelAccomodationAPI.ModelClass.ResponseModule
{
    public class GetBanquetResponse
    {
        public long BanqueId { get; set; }
        public long HotelId { get; set; }
        public string banquetName { get; set; }
        public int NoOfCovers { get; set; }
        public decimal PerPlateCostVeg { get; set; }
        public decimal PerPlateCostNonVeg { get; set; }
        public int SqFeetArea { get; set; }
        public string Description { get; set; }
        public int TenantId { get; set; }
        public string HotelName { get; set; }
        public string HotelCode { get; set; }
        public long? BanquetFileId { get; set; }
        public long? BanquetId { get; set; }
        public string FilePath { get; set; }

    }
}
