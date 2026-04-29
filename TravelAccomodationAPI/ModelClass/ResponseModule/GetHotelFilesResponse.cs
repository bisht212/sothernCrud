namespace TravelAccomodationAPI.ModelClass.ResponseModule
{
    public class GetHotelFilesResponse
    {
        public long FileId { get; set; }

        public long HotelId { get; set; }

        public string FileName { get; set; }

        public string Description { get; set; }

        public string FilePath { get; set; }

    }
}
