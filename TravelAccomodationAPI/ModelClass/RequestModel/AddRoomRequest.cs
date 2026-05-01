namespace TravelAccomodationAPI.ModelClass.RequestModel
{
    public class AddRoomRequest
    {
        public long HotelId { get; set; }

        public string RoomCategoryName { get; set; }

        public string RoomDescription { get; set; }

        public int NoOfBeds { get; set; }

        public int NoOfRooms { get; set; }
    }
}
