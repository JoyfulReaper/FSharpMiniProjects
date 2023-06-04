using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.ApiClient.HttpClients;

namespace TaskTracker.Console.Features.Tasks;
internal class TaskApplicationHostedService : IHostedService
{
    private readonly ITaskClient _taskClient;

    public TaskApplicationHostedService(ITaskClient taskClient)
    {
        _taskClient = taskClient;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var test = await _taskClient.GetAsync("06bd0b89-1b4e-4a58-a3f3-f964748703f2");
        throw new NotImplementedException();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
