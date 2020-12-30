using Microsoft.AspNetCore.Http;
using MKTFY.App.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MKTFY.App.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception err)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                string errorMessage = null;

                switch (err)
                {
                    case NotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound; // 404
                        errorMessage = e.Message;
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorMessage = "Sorry, your request cound not be completed.\n" + err.Message;
                        break;
                }

                // return the response
                var result = JsonSerializer.Serialize(new { message = errorMessage });
                await response.WriteAsync(result);
            }
        }
    }
}
