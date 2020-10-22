namespace Core.Results
{
    public class ErrorDataResult<T> :ResultBase, IDataResult<T>
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

        public T Data { get; set; }
    }
}