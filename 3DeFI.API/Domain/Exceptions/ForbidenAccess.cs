namespace _3DeFI.API.Domain.Exceptions
{
    public class ForbidenAccess : BaseResponseException
    {
        public ForbidenAccess() : base(403, "Don't have access to the recource")
        {

        }
    }
}
