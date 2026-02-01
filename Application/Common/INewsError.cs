using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public interface INewsError
    {
        public string Message { get; init; }
        public int Code { get; init; }
    }
}
