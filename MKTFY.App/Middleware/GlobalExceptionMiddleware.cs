using Microsoft.AspNetCore.Http;
using MKTFY.App.Exceptions;
using System;
using System.Net;
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
                string errorMessage = err.Message ?? null;
                string id = null;

                switch (err)
                {
                    case NotFoundException e:
                        response.StatusCode = (int)HttpStatusCode.NotFound; // 404
                        errorMessage = e.Message;
                        id = e.id;
                        break;

                    case MismatchingId e:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;   //400
                        id = e.id;
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                }

                // return the response
                var result = id == null ? 
                        JsonSerializer.Serialize(new { message = errorMessage }) 
                    :   JsonSerializer.Serialize(new { message = errorMessage, id = id });

                await response.WriteAsync(result);
            }
        }
    }
}
