using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tajnned.Application.Common
{
   
    public record BaseRequest<T> : IRequest<ApiResponse<T>>
    {
        public int UserId { get; init; }
        public string Language { get; init; } = "ar";
    }
}
