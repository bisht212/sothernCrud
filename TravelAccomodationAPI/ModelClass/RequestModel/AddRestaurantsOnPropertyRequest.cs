namespace TravelAccomodationAPI.ModelClass.RequestModel
{
    public class AddRestaurantsOnPropertyRequest
    {
        public long Hotel_Id { get; set; }

        public int Resta_Name { get; set; }

        public int Veg_Id { get; set; }

        public int Cuisine_Id { get; set; }

        public int No_of_covers { get; set; }

        public bool In_room_dining_facility { get; set; }
    }
}
