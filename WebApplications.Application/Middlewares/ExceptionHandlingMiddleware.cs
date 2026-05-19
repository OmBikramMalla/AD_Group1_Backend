using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace WebApplications.Application.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
            private readonly RequestDelegate _next;

            public ExceptionHandlingMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task Invoke(HttpContext httpContext)
            {
                try
                {

                    await _next(httpContext);
                }
                catch (Exception ex)
                {
                    httpContext.Response.ContentType = "application/json";
                    httpContext.Response.StatusCode = 500;
                    await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new
                    {
                        message = "internal server error",
                        statusCode = 500
                    }));
                }
            }
   

    }
}
