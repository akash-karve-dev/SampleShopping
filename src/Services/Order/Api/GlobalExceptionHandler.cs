using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Order.Api
{
    /// <summary>
    ///  Handle Validation errors and all unhandled exceptions
    /// </summary>
    /// <param name="logger"></param>
    internal class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IProblemDetailsService problemDetailsService) : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;
        private readonly IProblemDetailsService _problemDetailsService = problemDetailsService;

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, message: exception.Message);

            switch (exception)
            {
                case ValidationException validationException:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
                    {
                        HttpContext = httpContext,
                        ProblemDetails = new ProblemDetails
                        {
                            Title = "Validation errors",
                            Detail = exception.Message,
                            Extensions = new Dictionary<string, object?>
                            {
                                { "errors", validationException.Errors.Select(x => new { x.PropertyName, x.ErrorMessage }) }
                            }
                        }
                    });

                    break;

                default:
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                    await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
                    {
                        HttpContext = httpContext,
                        ProblemDetails = new ProblemDetails
                        {
                            Status = (int)HttpStatusCode.InternalServerError,
                            Title = "An unexpected error occurred",
                            Detail = exception.Message,
                        }
                    });
                    break;
            }

            return true;
        }
    }
}