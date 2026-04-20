using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;

namespace TravelAccomodationAPI.ModelClass.RequestModel
{
    public class AddHotelMasterRequest : DBBaseClass
    {
  //      public int? HotelId { get; set; }
        public int TeanatId { get; set; }

        public string Hotel_Name { get; set; }

        public int Property_Type_Id { get; set; }

        public int Star_Rating_Id { get; set; }

        public string Owner_Name { get; set; }

        public string Owner_Phone { get; set; }

        public int ChainStandId { get; set; }
        public string AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public int City { get; set; }

        public int State { get; set; }

        public int Country { get; set; }

        public string PinCode { get; set; }

        public string LandMark { get; set; }

        public int Year_of_construction { get; set; }

        public int number_of_floors { get; set; }

        public int number_of_rooms { get; set; }

        public TimeSpan Check_In_Time { get; set; }

        public TimeSpan Check_Out_Time { get; set; }

        public string? How_To_Reach { get; set; }

        public string? Google_Map_Link { get; set; }

        public string? Google_Hotel_Link { get; set; }

        public string? Trip_Advisor_Link { get; set; }

        public string? Distance_From_Airport { get; set; }

        public string? Distance_From_Railway { get; set; }

        public string? Distance_From_ISBT { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public string? ApprovedBy { get; set; }
        public Boolean IsPublish { get; set; }

        public Boolean IsDraft { get; set; }

        public Boolean IsDeleted { get; set; }

        public string? Approval_Remarks { get; set; }
    }
}
