namespace _3DeFI.API.Domain;

public class IncorrectCredentials : BaseResponseException
{
    public IncorrectCredentials() : base(401, "Incorrect credentials")
    {
        
    }
}