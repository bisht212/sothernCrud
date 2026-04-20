namespace TravelAccomodationAPI.ModelClass
{
    public class DBBaseClass
    {
        public DateTime Created_At { get; set; } = DateTime.Now;

        public string Created_By { get; set; } = "";

        public DateTime? Updated_At { get; set; } = DateTime.Now;

        public string? Updated_By { get; set; } = "";
    }
}
