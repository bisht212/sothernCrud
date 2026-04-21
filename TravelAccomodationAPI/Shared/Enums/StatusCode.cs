namespace TravelAccomodationAPI.Shared.Enums
{
    public enum StatusCode 
    {
        Success = 200,
        Created = 201,
        Badrequest = 400,
        Unauthorized = 401,
        Forbidden = 403,
        NotFound = 404,
        Conflict = 409,
        InternalServer = 500, 
        ServiceUnnavilvabel = 503
    }
}
