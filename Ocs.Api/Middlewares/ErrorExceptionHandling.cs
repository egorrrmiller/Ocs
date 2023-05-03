using System.Net;
using Ocs.BLL.Dto.Errors;

namespace Ocs.Api.Middlewares;

public class ErrorExceptionHandling
{
    private readonly ILogger<ErrorExceptionHandling> _logger;

    private readonly RequestDelegate _requestDelegate;

    public ErrorExceptionHandling(RequestDelegate requestDelegate, ILogger<ErrorExceptionHandling> logger)
    {
        _requestDelegate = requestDelegate;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _requestDelegate(context);
        }
        catch (ArgumentException e)
        {
            await HandleExceptionAsync(context,
                e,
                HttpStatusCode.BadRequest,
                e.Message);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context,
                e,
                HttpStatusCode.InternalServerError,
                e.Message);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode,
                                            string message)
    {
        _logger.LogError(exception.ToString());

        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = (int) httpStatusCode;

        var errorDto = new ErrorDto
        {
            StatusCode = (int) httpStatusCode,
            Message = message
        };

        await response.WriteAsJsonAsync(errorDto);
    }
}