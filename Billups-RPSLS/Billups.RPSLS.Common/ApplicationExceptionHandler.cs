using Billups.RPSLS.DataModel.Responses;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Billups.RPSLS.Common;

public class ApplicationExceptionHandler : ExceptionHandlerMiddlewareBase
{
    /// <summary>
    /// Creates Application Exception Handler.
    /// </summary>
    /// <param name="next">Request delegate.</param>
    public ApplicationExceptionHandler(RequestDelegate next) : base(next)
    {
    }

    /// <summary>
    /// Method that filters and handles exception.
    /// </summary>
    /// <param name="response">Http response.</param>
    /// <param name="exception">Concrete exception.</param>
    /// <returns>Populated Http response.</returns>
    public override async Task HandleException(HttpResponse response, Exception exception)
    {
        switch (exception)
        {
            default:
                await base.HandleException(response, exception);
                break;
        }
    }

    /// <summary>
    /// Populates response with status code and error response.
    /// </summary>
    /// <param name="response">Response to be populated.</param>
    /// <param name="statusCode">Http status code.</param>
    /// <param name="message">Error message.</param>
    /// <returns>Populated Http response.</returns>
    private async Task HandleErrorResponse(HttpResponse response, HttpStatusCode statusCode, string message)
    {
        response.StatusCode = (int)statusCode;
        await response.WriteAsync(SerializeResponse(new ErrorResponse { ErrorMessage = message }));
    }
}