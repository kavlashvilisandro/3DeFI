namespace _3DeFI.API.Domain;

public class UserAlreadyExists : BaseResponseException
{
    public UserAlreadyExists() : base(400, "User already exists")
    {
        
    }
}