using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public interface IResult
    {
        //the operation is success or not ?
        bool Success { get; }
        //we re giving a message whether the operation is success or not
        string Message { get; } 
    }
}
