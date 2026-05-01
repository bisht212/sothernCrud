namespace TravelAccomodationAPI.ModelClass.RequestModel
{
    public class AddHotelRoomMediaRequest
    {
        public int RoomId { get; set; }

        public string ImageName { get; set; }

        public string Description { get; set; }

        public IFormFile ImagePath { get; set; }
    }
}
