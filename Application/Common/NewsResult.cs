using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{

    public class NewsResult<T>
    {
        private NewsResult(T value)
        {
            Value = value;
            IsSuccess = true;
            Error = null;
        }
        private NewsResult(INewsError error)
        {
            IsSuccess = false;
            Error = error;
            Value = default;
        }

        public bool IsSuccess { get; set; }
        public T? Value { get; set; }

        public INewsError? Error { get; set; }

        public static NewsResult<T> Success(T value)
        {
            return new NewsResult<T>(value);
        }

        public static NewsResult<T> Failure(INewsError error)
        {
            return new NewsResult<T>(error);
        }

        public static NewsResult<T> InternalFailure(string message, int code)
        {
            var error = new BaseNewsError(message, code);
            return new NewsResult<T>(error);
        }

        public static NewsResult<BaseNewsError> ValidationError(string validationError)
        {
            return new NewsResult<BaseNewsError>(new BaseNewsError(validationError, 400));
        }
    }
 
}
