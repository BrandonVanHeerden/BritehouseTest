using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class BaseNewsError : INewsError
    {
        public BaseNewsError(string message, int code)
        {
            Message = message;
            Code = code;
        }
        public string Message { get; init ; }
        public int Code { get; init; }
    }
}
