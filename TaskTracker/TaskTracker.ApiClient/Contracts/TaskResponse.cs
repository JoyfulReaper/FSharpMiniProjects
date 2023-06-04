namespace TaskTracker.ApiClient.Contracts;
public record TaskResponse
{
    public required string TaskId { get; init; }
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required bool Completed { get; init; }
    public required DateTime? DateCompleted { get; init; }
}
