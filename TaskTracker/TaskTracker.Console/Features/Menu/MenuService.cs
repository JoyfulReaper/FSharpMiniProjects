using TaskTracker.ApiClient.Contracts;
using TaskTracker.ApiClient.HttpClients;
using TaskTracker.Console.Common.Enums;

namespace TaskTracker.Console.Features.Menu;

public class MenuService
{
    private readonly ITaskClient _taskClient;

    public MenuService(ITaskClient taskClient)
    {
        _taskClient = taskClient;
    }

    public static void DisplayHeader()
    {
        System.Console.WriteLine("Task Tracker 1.0.0");
    }

    public async Task<MenuChoice> DisplayMenuAsync()
    {
        System.Console.WriteLine();
        System.Console.WriteLine("1. Create task");
        System.Console.WriteLine("2. View tasks");
        System.Console.WriteLine("3. Exit");

        var option = GetOption();
        await ProcessOptionAsync(option);
        return option;
    }

    private static MenuChoice GetOption()
    {
        System.Console.Write("Enter option: ");
        var option = System.Console.ReadLine();

        if (!int.TryParse(option, out int parseResult))
        {
            return MenuChoice.Invalid;
        }

        var result = (MenuChoice)parseResult;
        if (!Enum.IsDefined(typeof(MenuChoice), result))
        {
            return MenuChoice.Invalid;
        }

        return result;
    }

    private async Task ProcessOptionAsync(MenuChoice option)
    {
        switch (option)
        {
            case MenuChoice.CreateTask:
                await CreateTaskAsync();
                break;
            case MenuChoice.ViewTasks:
                await ListTasksAsync();
                break;
            case MenuChoice.Exit:
                Environment.Exit(Environment.ExitCode);
                break;
            case MenuChoice.Invalid:
                System.Console.WriteLine("Invalid option");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(option), option, null);
        }
    }   

    private async Task CreateTaskAsync()
    {
        System.Console.Write("Enter task title: ");
        var title = System.Console.ReadLine();
        System.Console.Write("Enter task description: ");
        var description = System.Console.ReadLine();

        var task = new TaskRequest {
            Title = title,
            Description = description
        };

        await _taskClient.CreateAsync(task);
    }

    private async Task ListTasksAsync()
    {
        System.Console.WriteLine();
        var allTasks = await _taskClient.GetAllAsync();
        var taskDict = new Dictionary<int, TaskResponse>();

        int taskNumber = 1;
        foreach (var t in allTasks)
        {
            taskDict.Add(taskNumber++, t);
        }

        foreach (var t in taskDict)
        {
            System.Console.WriteLine($"Task #{t.Key}: {t.Value.Title}");
        }

        System.Console.Write("Choose a task: ");
        var selectedTaskNumber = int.Parse(System.Console.ReadLine());

        var task = await _taskClient.GetAsync(taskDict[selectedTaskNumber].TaskId);

        System.Console.Clear();
        DisplayTaskResponse(task);
        await GetTaskActionAsync(task);
    }

    private async Task GetTaskActionAsync(TaskResponse task)
    {
        System.Console.WriteLine();
        System.Console.WriteLine($"1. {(task.Completed == true ? "Uncomplete" : "Complete")}");
        System.Console.WriteLine("2. Edit");
        System.Console.WriteLine("3. Delete");
        System.Console.WriteLine("4. Back");

        System.Console.Write("Action: ");
        int action = int.Parse(System.Console.ReadLine());

        switch(action) 
        { 
            case 1: 
                {
                    await InvertCompletionAsync(task);
                    break;
                }
            case 2: 
                {
                    await EditTaskAsync(task);
                    break;
                }
            case 3: 
                {
                    await DeleteTaskAsync(task);
                    break;
                }
            case 4:
                {
                    await ListTasksAsync();
                    break;
                }
            default: 
                {
                    System.Console.WriteLine("Invalid Choice!");
                    await GetTaskActionAsync(task);
                    break;
                }
        }
    }

    private async Task InvertCompletionAsync(TaskResponse task)
    {
        if (task.Completed)
        {
            await _taskClient.UncompleteAsync(task.TaskId);
            System.Console.WriteLine("Task Uncompleted!");
        }
        else
        {
            await _taskClient.CompleteAsync(task.TaskId);
            System.Console.WriteLine("Task Completed!");
        }
    }

    private async Task DeleteTaskAsync(TaskResponse task)
    {
        await _taskClient.DeleteAsync(task.TaskId);
        System.Console.WriteLine("Task Deleted!");
    }

    private async Task EditTaskAsync(TaskResponse task)
    {
        System.Console.Write("Enter new title: ");
        var title = System.Console.ReadLine();
        System.Console.Write("Enter new description: ");
        var description = System.Console.ReadLine();

        var updateRequest = new TaskUpdateRequest(task.TaskId, title, description, task.Completed, task.DateCompleted);
        await _taskClient.UpdateAsync(updateRequest);
    }

    public static void DisplayTaskResponse(TaskResponse task)
    {
        System.Console.WriteLine();
        System.Console.WriteLine($"TaskId: {task.TaskId}");
        System.Console.WriteLine($"Title: {task.Title}");
        System.Console.WriteLine($"Description: {(task.Description ?? "(none)")}");
        System.Console.WriteLine($"Completed: {task.Completed}");
        if(task.Completed)
        {
            System.Console.WriteLine($"Date Completed: {task.DateCompleted}");
        }
    }
}
