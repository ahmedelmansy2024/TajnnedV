using System;
using System.Collections.Generic;
using System.Text;

namespace Tajnned.Application.Common
{
    public record ApiResponse<T>
    {
        public bool Success { get; init; }
        public string Message { get; init; }
        public T? Data { get; init; }

        public static ApiResponse<T> Ok(T data)
            => new() { Success = true, Data = data, Message = "Success" };

        public static ApiResponse<T> Fail(string message)
            => new() { Success = false, Message = message };
    }
}
