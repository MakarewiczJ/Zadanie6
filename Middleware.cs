using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zadanie6
{
    public class Middleware
    {
        private RequestDelegate _next;
        public Middleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task Invoke(HttpContext context)
        {
            var request = context.Request.Headers["User-Agent"];
            if(request.ToString().ToLower().Contains("edg") || request.ToString().ToLower().Contains("trident"))
            {
                context.Response.WriteAsync("<p>Przegladarka nie jest obslugiwana</p>");
            }
            return _next(context);
        }
    }
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Middleware>();
        }
    }
}
