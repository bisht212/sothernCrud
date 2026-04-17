namespace TravelAccomodationAPI.ModelClass.ResponseModule
{
    public class GetUser
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public bool IsActive { get; set; }
    }
}
