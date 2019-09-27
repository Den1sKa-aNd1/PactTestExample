using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using WebAPI.Models;

namespace WebAPI.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                var response = await FormatResponse();

                await context.Response.WriteAsync(response);
            }
        }

        
        private Task<string> FormatResponse()
        {

            //...and copy it into a string
            string text = JsonSerializer.Serialize(new SomeDto { NumberParam = 3, StringParam = "string from middleware" });
            //We need to reset the reader for the response so that the client can read it.

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return Task.FromResult($"{text}");
        }
    }
}
