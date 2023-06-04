using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.ApiClient.Constants;
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
}
