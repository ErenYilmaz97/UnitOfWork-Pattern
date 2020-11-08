namespace Core.Results
{
    public abstract class DataResultBase<T> : ResultBase, IDataResult<T>
    {
        public T Data { get; set; }
    }
}