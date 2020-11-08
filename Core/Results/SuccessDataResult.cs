namespace Core.Results
{
    public class SuccessDataResult<T> : DataResultBase<T> where T: class,new()
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

    }
}