namespace Billups.RPSLS.DataModel.Responses;

public sealed class ErrorResponse
{
    public ErrorResponse()
    {
    }

    public ErrorResponse(string? errorMessage)
    {
        ErrorMessage = errorMessage;
    }

    public string? ErrorMessage { get; set; }
}
