namespace TravelAccomodationAPI.ModelClass.RequestModel
{
    public class AddHotelContactPhoneNumberRequest : DBBaseClass
    {
        public long ContactId { get; set; }

        public string CountryCode { get; set; }

        public string Phone_Number { get; set; }

        public int PhoneType_Id { get; set; }
    }
}
