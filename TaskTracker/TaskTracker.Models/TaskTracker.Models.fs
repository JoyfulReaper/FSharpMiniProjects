namespace TaskTracker.Models
open System

type Task = 
    { TaskId: Guid
      Title: string
      Description: string option
      Completed: bool
      DateCompleted: DateTime option } 