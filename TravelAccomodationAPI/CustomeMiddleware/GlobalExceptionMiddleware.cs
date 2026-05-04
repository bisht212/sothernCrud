using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Data.SqlClient;
using Serilog;
using System.Text.Json;
using TravelAccomodationAPI.ModelClass;

namespace TravelAccomodationAPI.CustomeMiddleware
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
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            var endpoint = context.GetEndpoint();

            var controller = endpoint?
                .Metadata
                .GetMetadata<ControllerActionDescriptor>()?.ControllerName;

            var action = endpoint?
                .Metadata
                .GetMetadata<ControllerActionDescriptor>()?.ActionName;

            int statusCode = 500;
            string message = "Something went wrong";

            switch (exception)
            {
                case ApiException apiEx:
                    statusCode = apiEx.StatusCode;
                    message = apiEx.Message;
                    break;

                case SqlException sqlEx:
                    statusCode = sqlEx.Number switch
                    {
                        50001 => 400, // Bad Request
                        50002 => 404, // Not Found
                        50003 => 409,
                        _ => 500      // Internal Server Error
                    };
                    message = sqlEx.Message;
                    break;

                case KeyNotFoundException:
                    statusCode = 404;
                    message = "Resource not found";
                    break;

                case UnauthorizedAccessException:
                    statusCode = 401;
                    message = "Unauthorized";
                    break;

                case ArgumentException:
                    statusCode = 400;
                    message = exception.Message;
                    break;
            }

            // FULL LOG 
            Log.Error(exception,
                "Error at {Path} | Controller {Controller} | Action {Action} | Method {Method} | Query {Query} | TraceId {TraceId}",
                context.Request.Path,
                controller,
                action,
                context.Request.Method,
                context.Request.QueryString,
                context.TraceIdentifier);

            var response = new ApiResponse<object>
            {
                StatusCode = statusCode,
                IsError = true,
                Message = statusCode == 500
                    ? "Internal server error"
                    : message,
                Data = null
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));

        }
   
    
    }
}
