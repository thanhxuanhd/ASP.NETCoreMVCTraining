using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace LibraryManagement.Api.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
{
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Call the next delegate/middleware in the pipeline
            await next(context);
        }
        catch (Exception ex)
        {
            // Log the exception
            logger.LogError(ex, "An unhandled exception occurred while processing {Path}", context.Request.Path);

            // Handle the exception and generate the response
            await HandleExceptionAsync(context, ex);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Ensure response hasn't already started
        if (context.Response.HasStarted)
        {
            logger.LogWarning("The response has already started, the exception middleware will not be executed.");
            return; // Can't write response headers/body anymore
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // Default status code

        // Create the response object
        var response = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "An internal server error occurred. Please try again later.",
            // Include details only in Development environment for security reasons
            Detail = env.IsDevelopment() ? exception.ToString() : null
        };

        // Serialize the response object to JSON
        var jsonResponse = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        // Write the JSON response to the client
        await context.Response.WriteAsync(jsonResponse);
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}