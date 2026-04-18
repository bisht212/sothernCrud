using TravelAccomodationAPI.ModelClass.ResponseModule;

namespace TravelAccomodationAPI.TokenCreateClass.InterFaces
{
    public interface IToken
    {
        public string CreateToken(UserLoginResponse data); 
        
    }
}
