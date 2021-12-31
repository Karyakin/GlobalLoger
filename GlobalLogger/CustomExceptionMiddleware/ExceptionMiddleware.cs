using System;
using System.Net;
using System.Threading.Tasks;
using GlobalLogger.Models;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace GlobalLogger.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (AccessViolationException ex) // любое исключение
            {
                Log.Error($"444444A new violation exception has been thrown: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (DivideByZeroException ex)
            {
                Log.Error($"888888 DivideByZeroException: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }   
            catch (Exception ex)
            {
                Log.Error($"33333333333Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var message = exception switch
            {
                AccessViolationException => "6666666 AccessViolationException",
                DivideByZeroException => "77777777 DivideByZeroException",
                NotImplementedException => "77777777 NotImplementedException",
                _ => exception.Message
            };

            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = message
            }.ToString());
        }
    }
}