namespace Core.Results
{
    public class ErrorDataResult<T> : DataResultBase<T> where T: class,new()
    {

        public ErrorDataResult()
        {
            Success = false;
        }


        public ErrorDataResult(string Message)
        {
            Success = false;
            this.Message = Message;
        }

    }
}