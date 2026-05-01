namespace TravelAccomodationAPI.Shared.StoredProcedures
{
    public static class Stored_Procedures
    {
        // Hotel master
        public static string ADD_HOTEL_MASTER = "dbo.AddHotelsMaster_SP";
        public static string GET_HOTEL_MASTER = "dbo.usp_HotelsMaster_Get";
      
        public static string GET_RESTURANT_LIST = "GetRestaurantsByHotelId";
        public static string ADD_BULK_RESTURANTPROPERTY = "usp_Bulk_RestaurantsOnProperty_Insert";
        public static string UPLOAD_FILE_SP = "sp_InsertFile";
        public static string UPDATE_RESTURANTPROPERTY = "usp_Update_RestaurantOnProperty";
        public static string UPDATE_RESTURANTFILE = "sp_ReplaceRestaurantFiles"; 
        public static string ADD_BULK_BANQUET = "usp_Insert_BanquetOnProperty";
        public static string UPLOAD_BANQUET_FILES = "sp_InsertBanquetFile";
        public static string ADD_BULK_HOTEL_CONTACTS = "usp_HotelsContacts_Insert";
        public static string UPDATE_BULK_HOTEL_CONTACTS = "usp_HotelsContacts_Update";
        public static string DELETE_RESTAURANT = "usp_RestaurantsOnProperty_Delete";
        public static string DELTE_HOTEL_CONTACT = "usp_HotelsContacts_Delete";
        public static string ADD_PHONE_TYPE = "usp_Phonetype_Insert";
        public static string GET_PHONE_TYPES = "usp_Phonetype_GetAll";
        public static string GET_PHONE_TYPE = "usp_Phonetype_GetById";
        public static string ADD_HOTEL_CONTACT_PHONE_NUMBER = "usp_HotelsContactPhone_Insert";
        public static string DELETE_HOTEL_CONTACT_PHONE_NUMBER = "usp_HotelsContactPhone_Delete";
        public static string ADD_HOTEl_CONTACT_EMAIL = "usp_HotelsContactEmail_Insert";
        public static string DELETE_HOTEL_CONTTACT_EMAIL = "usp_HotelsContactEmail_Delete";
        public static string ADD_AMINITY = "usp_HotelAmenities_Insert";
        public static string UPDATE_AMINITY = "usp_HotelAmenities_Update";
        public static string GET_AMINITIES = "usp_HotelAmenities_GetAll";
        public static string GET_AMINITY = "usp_HotelAmenities_GetById";
        public static string GET_HOTEL_FACILITY_CATEGORIES = "usp_Hotel_Facility_Category_GetAll";
        public static string GET_HOTEL_FACILITY_CATEGORYID = "usp_Hotel_Facility_Category_GetById";
        public static string ADD_HOTEL_FACILITIES = "usp_HotelFacilities_Insert";
        public static string GET_BANQUETS = "usp_GetBanquetsByHotelId";
        public static string UPLOAD_BANQUET_FILE_SP = "usp_Update_BanquetOnProperty";
        public static string DELETE_BANQUET = "usp_BanqueteOnProperty_Delete";
        public static string ADD_HOTEL_FILES = "AddHotelFiles";
        public static string UPDATE_HOTEL_FILE = "UpdateHotelFile";
        public static string DELETE_HOTEL_FILE = "dbo.DeleteHotelFile"; 
        public static string GET_HOTEL_FILES = "GetHotelImages";
        public static string HOTELMASTER_SAVE_AS_DRAFT = "usp_HotelsMaster_UpdateIsDraft";

        public static string ADD_HOTEL_ROOMS = "usp_AddRoom";
        public static string ADD_ROOM_MEDIA = "usp_AddRoomMedia";
        public static string ADD_ROOM_FACILITY = "usp_addRoomFacility";

        //Master
        public static string GET_ALL_VEG_NON_VEG = "dbo.usp_VegNonveg_GetAll";
        public static string GET_VEG_NON_VEG = "dbo.usp_VegNonveg_GetById";
        public static string GET_ALL_CUISINE = "dbo.usp_Cuisine_GetAll";
        public static string GET_HOTEL_FACILITIES = "usp_RoomFacilitiesMaster_Get";
        public static string GET_HOTEL_FACILITY = "usp_RoomFacilitiesMaster_GetById";
        public static string ADD_HOTEL_FACILITY = "usp_RoomFacilitiesMaster_Insert";
        public static string UPDATE_HOTEL_FACILITY = "usp_RoomFacilitiesMaster_Update";
        public static string DELETE_HOTEL_FACILITY = "usp_RoomFacilitiesMaster_Delete";


    }
}
