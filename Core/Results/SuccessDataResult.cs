using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public class SuccessDataResult<T> : IDataResult<T> where T : class,new()
    {

        public SuccessDataResult()
        {
            this.Success = true;
        }



        public SuccessDataResult(T data)
        {
            this.Success = true;
            this.Data = data;
        }




        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
