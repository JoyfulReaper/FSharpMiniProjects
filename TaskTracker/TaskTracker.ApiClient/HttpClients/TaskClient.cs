using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TaskTracker.ApiClient.Constants;
using TaskTracker.ApiClient.Contracts;
using TaskTracker.ApiClient.Options;

namespace TaskTracker.ApiClient.HttpClients;
public class TaskClient : ITaskClient
{
    private readonly TaskApiClientOptions _taskApiClientOptions;
    private readonly HttpClient _httpClient;

    public TaskClient(IOptions<TaskApiClientOptions> taskClientOptions,
        HttpClient httpClient)
    {
        _taskApiClientOptions = taskClientOptions.Value;
        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri(_taskApiClientOptions.BaseUrl);
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(TaskClientConstants.UserAgent, TaskClientConstants.Version));
        _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue(TaskClientConstants.UserAgentComment));
    }

    public async Task<TaskResponse> CreateAsync(TaskRequest taskCreateRequest, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PostAsJsonAsync(_httpClient.BaseAddress + "task", taskCreateRequest, cancellationToken);
        response.EnsureSuccessStatusCode();
        var taskResponse = await response.Content.ReadFromJsonAsync<TaskResponse>(cancellationToken: cancellationToken);

        return taskResponse!;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync(_httpClient.BaseAddress + $"task/{id}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task<TaskResponse> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<TaskResponse>(_httpClient.BaseAddress + $"task/{id}", cancellationToken);
        return response!;
    }

    public Task<TaskResponse> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        return GetAsync(Guid.Parse(id), cancellationToken);
    }

    public async Task<IEnumerable<TaskResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<TaskResponse>>(_httpClient.BaseAddress + "task", cancellationToken);
        return response!;
    }

    public async Task<TaskResponse> UpdateAsync(TaskUpdateRequest taskUpdateRequest, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.PutAsJsonAsync(_httpClient.BaseAddress + $"task/{taskUpdateRequest.TaskId}", taskUpdateRequest, cancellationToken);
        response.EnsureSuccessStatusCode();
        var taskResponse = await response.Content.ReadFromJsonAsync<TaskResponse>(cancellationToken: cancellationToken);

        return taskResponse!;
    }

    public Task CompleteAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        var response = _httpClient.PostAsync(_httpClient.BaseAddress + $"task/{Id}/complete", null, cancellationToken);
        return response;
    }

    public Task CompleteAsync(string Id, CancellationToken cancellationToken = default)
    {
        return CompleteAsync(Guid.Parse(Id), cancellationToken);
    }

    public async Task UncompleteAsync(Guid Id, CancellationToken cancellationToken = default)
    {
        var task = await GetAsync(Id, cancellationToken);
        if (task.Completed)
        {
            var updateRequest = new TaskUpdateRequest(task.TaskId.ToString(), task.Title, task.Description, false, null);
            await UpdateAsync(updateRequest, cancellationToken);
        }
    }

    public async Task UncompleteAsync(string Id, CancellationToken cancellationToken = default)
    {
        await UncompleteAsync(Guid.Parse(Id), cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        await DeleteAsync(Guid.Parse(id), cancellationToken);
    }
}
