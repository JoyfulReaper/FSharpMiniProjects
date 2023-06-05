using TaskTracker.ApiClient.Contracts;

namespace TaskTracker.ApiClient.HttpClients;
public interface ITaskClient
{
    Task<IEnumerable<TaskResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TaskResponse> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TaskResponse> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<TaskResponse> CreateAsync(TaskRequest taskCreateRequest, CancellationToken cancellationToken = default);
    Task<TaskResponse> UpdateAsync(TaskUpdateRequest taskUpdateRequest, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
    Task CompleteAsync(Guid Id, CancellationToken cancellationToken = default);
    Task CompleteAsync(string Id, CancellationToken cancellationToken = default);
    Task UncompleteAsync(Guid Id, CancellationToken cancellationToken = default);
    Task UncompleteAsync(string Id, CancellationToken cancellationToken = default);
}
