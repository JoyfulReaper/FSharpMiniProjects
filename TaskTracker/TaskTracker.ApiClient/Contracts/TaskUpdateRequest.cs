namespace TaskTracker.ApiClient.Contracts;

public record TaskUpdateRequest(
    string TaskId,
    string Title,
    string? Description,
    bool Completed,
    DateTime? DateCompleted);