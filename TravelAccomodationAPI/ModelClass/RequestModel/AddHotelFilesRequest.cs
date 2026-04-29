namespace TravelAccomodationAPI.ModelClass.RequestModel
{
    public class AddHotelFilesRequest :DBBaseClass
    {
      
          //  public long FileId { get; set; }
            public long HotelId { get; set; }

            public string FileName { get; set; }
            public IFormFile FIle { get; set; }

            public string Description { get; set; }
        
    }

}
