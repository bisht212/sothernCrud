namespace TravelAccomodationAPI.ModelClass.RequestModel
{
    public class AddHotelContactEmailRequest : DBBaseClass
    {
        public long ContactId { get; set; }

        public string Email { get; set; }
    }
}
