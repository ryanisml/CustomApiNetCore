using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CustomApiNetCore.Middlewares
{
    public class CustomBadRequestMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomBadRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            if (context.Response.StatusCode == 400)
            {
                context.Response.ContentType = "application/json";
                string text = "The requested resource is not found...";
                var json = JsonSerializer.Serialize(text);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
