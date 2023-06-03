namespace TaskTracker.Models
open System

module Task =
    type Task = 
        { TaskId: Guid
          Title: string
          Description: string option
          Completed: bool
          DateCompleted: DateTime option } 

     let fromTaskRequest (taskRequest: TaskRequest) =
        { TaskId = Guid.Empty
          Title = taskRequest.Title
          Description = 
            match taskRequest.Description with
            | null
            | "" -> None
            | _ -> Some taskRequest.Description
          Completed = false
          DateCompleted = None }