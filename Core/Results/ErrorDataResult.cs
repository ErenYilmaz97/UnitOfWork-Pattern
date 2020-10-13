using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public class ErrorDataResult<T> : IDataResult<T> where T : class,new()
    {

        public ErrorDataResult()
        {
            this.Success = false;
        }

        public ErrorDataResult(string message)
        {
            this.Success = false;
            this.Message = message;
        }




        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
