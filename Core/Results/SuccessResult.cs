using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Results
{
    public class SuccessResult : IResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
