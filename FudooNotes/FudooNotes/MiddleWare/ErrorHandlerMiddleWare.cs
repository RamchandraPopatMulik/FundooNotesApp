using CloudinaryDotNet.Actions;
using System.Net;
using System.Text.Json;

namespace FudooNotes.MiddleWare
{
    public class ErrorHandlerMiddleWare
    {
        private readonly RequestDelegate next;

        public ErrorHandlerMiddleWare(RequestDelegate next)
        {
            next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch(Exception Error)
            {
                var resp = context.Response;
                resp.ContentType = "application/json";
                switch(Error)
                {
                    case ApplicationException e:
                        resp.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        resp.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case UnauthorizedAccessException e:
                        resp.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;
                    default:
                        resp.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }
                var result = JsonSerializer.Serialize(new{ success=false, message=Error.Message});
                await resp.WriteAsync(result);
            }
        }
    }
}
