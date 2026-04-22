namespace TravelAccomodationAPI.Shared.StoredProcedures
{
    public static class Stored_Procedures
    {
        // Hotel master
        public static string ADD_HOTEL_MASTER = "dbo.AddHotelsMaster_SP";
        public static string GET_HOTEL_MASTER = "dbo.usp_HotelsMaster_Get";
        public static string GET_ALL_VEG_NON_VEG = "dbo.usp_VegNonveg_GetAll";
        public static string GET_VEG_NON_VEG = "dbo.usp_VegNonveg_GetById";
        public static string GET_ALL_CUISINE = "dbo.usp_Cuisine_GetAll";
        public static string ADD_RESTURANTPROPERTY = "dbo.usp_RestaurantsOnProperty_Insert";

    }
}
