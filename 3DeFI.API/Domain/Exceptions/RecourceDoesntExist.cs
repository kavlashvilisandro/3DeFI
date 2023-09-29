namespace _3DeFI.API.Domain.Exceptions
{
    public class RecourceDoesntExist : BaseResponseException
    {
        public RecourceDoesntExist() : base(404, "Not found")
        {

        }
    }
}
