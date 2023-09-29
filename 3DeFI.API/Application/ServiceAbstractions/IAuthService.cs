namespace _3DeFI.API.Application;

public interface IAuthService
{
    Task AddNewUser(NewUserRequestModel newUser);
    Task<LoginResponseModel> Login(LoginUserRequestModel user);
}