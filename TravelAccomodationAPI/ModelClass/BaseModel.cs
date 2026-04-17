namespace TravelAccomodationAPI.ModelClass
{
    public class BaseModel
    {
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; } = ""; 

        public DateTime ModifiedAt { get; set; } = DateTime.Now;

        public string ModifiedBy { get; set; } = ""; 
    }
}
