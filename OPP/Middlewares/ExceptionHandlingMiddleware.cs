using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using OPP.Domain.Exceptions;

namespace OPP.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        int statusCode;
        string errorCode;
        string message;

        if (ex is AppException appEx)
        {
            statusCode = appEx.StatusCode;
            errorCode = appEx.ErrorCode;
            message   = appEx.Message;
        }
        else
        {
            statusCode = StatusCodes.Status500InternalServerError;
            errorCode  = "internal_error";
            message    = "Unexpected error occurred";
        }

        var problem = new ProblemDetails
        {
            Status  = statusCode,
            Title   = errorCode,
            Detail  = message,
            Type    = $"https://httpstatuses.io/{statusCode}",
            Instance = context.Request.Path
        };

        context.Response.StatusCode  = statusCode;
        context.Response.ContentType = "application/problem+json";

        await context.Response.WriteAsJsonAsync(problem);
    }
}