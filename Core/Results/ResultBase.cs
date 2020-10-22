namespace Core.Results
{
    public abstract class ResultBase : IResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }

    }
}