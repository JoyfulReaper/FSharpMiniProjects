namespace TaskTracker.ApiClient.Contracts;
public record TaskRequest
{
    public required string Title { get; init; }
    public required string Description { get; init; }
}