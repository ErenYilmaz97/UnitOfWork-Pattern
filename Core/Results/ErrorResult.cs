using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public class ErrorResult : IResult
    {
        public ErrorResult()
        {
            this.Success = false;
        }


        public ErrorResult(string message)
        {
            this.Success = false;
            this.Message = message;
        }


        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
