namespace Resallie.Responses.Authentication;

public class AuthenticatedResponse
{
    public String AccessToken { get; set; }
    
    public AuthenticatedResponse(String token)
    {
        AccessToken = token;
    }
}