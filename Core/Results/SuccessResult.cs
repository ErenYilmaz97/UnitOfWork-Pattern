using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public class SuccessResult : IResult
    {
        public SuccessResult()
        {
            this.Success = true;
        }


        public SuccessResult(string message)
        {
            this.Success = true;
            this.Message = message;
        }


        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
