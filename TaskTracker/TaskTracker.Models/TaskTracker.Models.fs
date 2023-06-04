namespace TaskTracker.Models
open System

module Task =
    type Task = 
        { TaskId: Guid
          Title: string
          Description: string option
          Completed: bool
          DateCompleted: DateTime option } 

    let fromTaskRequest (taskRequest: TaskRequest) : Task =
        { TaskId = Guid.Empty
          Title = taskRequest.Title
          Description = 
            match taskRequest.Description with
            | null
            | "" -> None
            | _ -> Some taskRequest.Description
          Completed = false
          DateCompleted = None }

    let fromTaskUpdateRequest (taskUpdateRequest: TaskUpdateRequest) : Task =
        { TaskId = Guid.Parse taskUpdateRequest.TaskId
          Title = taskUpdateRequest.Title
          Description = 
            match taskUpdateRequest.Description with
            | null
            | "" -> None
            | _ -> Some taskUpdateRequest.Description
          Completed = taskUpdateRequest.Completed
          DateCompleted = Option.ofNullable taskUpdateRequest.DateCompleted }

    let toDto (task: Task) : TaskResponse =
        { TaskId = task.TaskId.ToString()
          Title = task.Title
          Description = match task.Description with
                        | Some description -> description
                        | None -> null
          Completed = task.Completed
          DateCompleted = match task.DateCompleted with
                          | Some dateCompleted -> Nullable(dateCompleted)
                          | None -> Nullable<DateTime>() }