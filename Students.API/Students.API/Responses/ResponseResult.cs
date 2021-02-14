using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.API.Responses
{
    public class ResponseResult
    {
        public ResponseType Result { get; set; }

        public ResponseResult( bool parameter)
        {
            Result = parameter ? ResponseType.SuccessOperation : ResponseType.FailedOperation;
        }
    }

    public enum ResponseType
    {
        SuccessOperation = 200,
        FailedOperation = 55

    }
}
