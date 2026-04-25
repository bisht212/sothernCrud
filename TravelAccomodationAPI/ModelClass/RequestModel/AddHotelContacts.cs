namespace TravelAccomodationAPI.ModelClass.RequestModel
{
    public class AddHotelContacts : DBBaseClass

    {
        public string Name { get; set; }

        public long Hotel_Id { get; set; }

        public string Department { get; set; }

        public string Designation { get; set; }

        public string Landline_Country_Code { get; set; }

        public string Landline_Number { get; set; }

        public string Whatsapp_Country_Code { get; set; }

        public string Whatsapp_Number { get;set; }
    }
}
