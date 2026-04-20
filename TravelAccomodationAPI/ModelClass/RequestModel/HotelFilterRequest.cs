namespace TravelAccomodationAPI.ModelClass.RequestModel
{
    public class HotelFilterRequest
    {
        public int TenantId { get; set; }

        public string? HotelName { get; set; }
    }
}
