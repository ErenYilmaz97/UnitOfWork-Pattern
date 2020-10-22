namespace Core.Results
{
    public class SuccessDataResult<T> :ResultBase, IDataResult<T>
    {

        public SuccessDataResult()
        {
            Success = true;
        }


        public SuccessDataResult(T Data)
        {
            Success = true;
            this.Data = Data;
        }


        public T Data { get; set; }

    }
}