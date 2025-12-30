using System;
using System.Collections.Generic;
using System.Text;

namespace KuaforRandevuAPI.Common.Responses
{
    public class ApiResponse<T>
    {
        public int Status { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public Object? Errors { get; set; }
        public static ApiResponse<T> SuccessResponse(T data, string? message = "")
        {
            return new ApiResponse<T>
            {
                Status = 200,
                Success = true,
                Message = message,
                Data = data
            };
        }
        public static ApiResponse<T> ErrorResponse(string? message, Object? errors = null,int status = 400)
        {
            return new ApiResponse<T>
            {
                Status = status,
                Success = false,
                Message = message,
                Errors = errors
            };
        }
    }
}
