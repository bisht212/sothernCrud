namespace TravelAccomodationAPI.ModelClass.ResponseModule
{
    public class GetAminityResponse
    {
        public int AmenityID { get; set; }
        public string AmenityName { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
