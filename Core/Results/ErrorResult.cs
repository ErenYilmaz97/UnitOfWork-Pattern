namespace Core.Results
{
    public class ErrorResult : ResultBase
    {

        public ErrorResult()
        {
            Success = false;
        }

        public ErrorResult(string Message)
        {
            Success = false;
            this.Message = Message;
        }
        
    }
}