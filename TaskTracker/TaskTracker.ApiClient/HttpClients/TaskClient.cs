using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
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

    public Task<TaskResponse> Create(TaskRequest taskCreateRequest)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<TaskResponse> Get(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<TaskResponse>(_httpClient.BaseAddress + $"/task/{id}", cancellationToken);
        return response!;
    }

    public Task<IEnumerable<TaskResponse>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<TaskResponse> Update(TaskUpdateRequest taskUpdateRequest)
    {
        throw new NotImplementedException();
    }
}
