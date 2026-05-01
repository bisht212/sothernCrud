namespace TravelAccomodationAPI.ModelClass.RequestModel
{
    public class AddRoomDetailsRequest
    {
        public AddRoomRequest RoomRequest { get; set; }

        public  List<AddHotelRoomMediaRequest> RoomMediaRequste { get; set; }

        public List<AddRoomFacilityRequest> RoomFacilityRequeste { get; set; }
    }
}
