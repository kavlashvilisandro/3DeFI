namespace _3DeFI.API.Domain.Exceptions
{
public class IncorrectFileType : BaseResponseException
    {
        public IncorrectFileType() : base(402, "Incorrect file type")
        {

        }
    }
}
