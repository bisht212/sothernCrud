namespace TravelAccomodationAPI.ModelClass.RequestModel
{
    public class AddBanquestRequest
    {

        public long Hotel_Id { get; set; }
        public string Banquet_Name { get; set; }
        public int No_of_covers { get; set; }
        public int Per_plate_cost_veg { get; set; }
        public int Per_plate_cost_non_veg { get; set; }
        public int Sq_feet_area { get; set; }
        public string Description { get; set; }
        public List<IFormFile> BanquetImages { get; set; }


    }
}
