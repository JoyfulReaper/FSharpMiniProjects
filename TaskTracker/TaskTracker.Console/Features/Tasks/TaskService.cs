using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.ApiClient.HttpClients;

namespace TaskTracker.Console.Features.Tasks;

internal class TaskService
{
    private readonly ITaskClient _taskClient;

    public TaskService(ITaskClient taskClient)
    {
        _taskClient = taskClient;
    }

    public async Task ListTasksAsync()
    {
        var allTasks = await _taskClient.GetAllAsync();
        foreach (var task in allTasks)
        {
            System.Console.WriteLine($"Task: {task.Title}");
        }
    }
}
