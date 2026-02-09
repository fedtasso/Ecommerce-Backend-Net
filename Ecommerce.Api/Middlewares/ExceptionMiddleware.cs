using System.Net;
using System.Text.Json;
using Ecommerce.Application.Exceptions;

namespace Ecommerce.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = exception switch
        {
            UserNotFoundException => CreateResponse(HttpStatusCode.NotFound, exception.Message, context),
            ProductNotFoundException => CreateResponse(HttpStatusCode.NotFound, exception.Message, context),
            ActiveCartNotFoundException => CreateResponse(HttpStatusCode.NotFound, exception.Message, context),
            ProductNotInCartException => CreateResponse(HttpStatusCode.NotFound, exception.Message, context),
            InvalidCredentialsException => CreateResponse(HttpStatusCode.Unauthorized, exception.Message, context),
            OperationNotAllowedException => CreateResponse(HttpStatusCode.Forbidden, exception.Message, context),
            EmailAlreadyExistsException => CreateResponse(HttpStatusCode.Conflict, exception.Message, context),
            SamePasswordException => CreateResponse(HttpStatusCode.BadRequest, exception.Message, context),

            BusinessException => CreateResponse(HttpStatusCode.BadRequest, exception.Message, context),

            _ => CreateResponse(HttpStatusCode.InternalServerError, "Error interno del servidor", context)
        };

        context.Response.StatusCode = (int)response.Status;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static ApiErrorResponse CreateResponse(
        HttpStatusCode status,
        string message,
        HttpContext context)
    {
        return new ApiErrorResponse
        {
            Type = "https://api.ecommerce.com/errors/" + status.ToString().ToLower(),
            Title = status.ToString(),
            Status = (int)status,
            Detail = message,
            Instance = context.Request.Path,
            Timestamp = DateTime.UtcNow
        };
    }
}
