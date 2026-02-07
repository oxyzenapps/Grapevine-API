using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grapevineCommon.Model
{
    public sealed class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool status { get; set; }
        public ResultBody<T> result { get; set; } = new ();

        public static ApiResponse<T> Success(T data, string message, int statusCode,string statusMessage,bool status=true)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Message = statusMessage,
                status = status,
                result = new ResultBody<T> { message = message, data = data }
            };
        }

        public static ApiResponse<T> Error(string message, int statusCode, string statusMessage,T? data = default,bool status=false)
        {
            return new ApiResponse<T>
            {
                StatusCode = statusCode,
                Message = statusMessage,
                status = status,
                result = new ResultBody<T> { message = message, data = data! }
            };
        }
    }

    public sealed class ResultBody<T>
    {
        public string message { get; set; } = string.Empty;
        public T data { get; set; }
    }
}
