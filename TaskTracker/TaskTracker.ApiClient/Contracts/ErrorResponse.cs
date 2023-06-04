namespace TaskTracker.ApiClient.Contracts;
public record ErrorResponse
{
    public required string Message { get; init; }
}
