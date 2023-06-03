namespace TaskTracker.Repository.Sql.Entities

open TaskTracker.Models.Task

module Entities =
    open System

    [<CLIMutable>]
    type TaskEntity =
            { TaskId: Guid
              Title: string
              Description: string option
              Completed: bool
              DateCompleted: DateTime option }

    let toDomain (task : TaskEntity) : Task =
        { TaskId = task.TaskId
          Title = task.Title
          Description = task.Description
          Completed = task.Completed
          DateCompleted = task.DateCompleted }

    let toEntity (task: Task) (generateId: bool) : TaskEntity =
        { TaskId = match generateId with
                   | true -> Guid.NewGuid()
                   | false -> task.TaskId
          Title = task.Title
          Description = task.Description
          Completed = task.Completed
          DateCompleted = task.DateCompleted }