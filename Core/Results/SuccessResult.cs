namespace Core.Results
{
    public class SuccessResult : ResultBase
    {

        public SuccessResult()
        {
            Success = true;
        }


        public SuccessResult(string Message)
        {
            Success = true;
            this.Message = Message;
        }


    }
}