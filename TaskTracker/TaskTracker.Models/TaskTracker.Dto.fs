namespace TaskTracker.Models
open System

[<CLIMutable>]
type TaskRequest = 
    { Title: string
      Description: string } 

[<CLIMutable>]
type TaskUpdateRequest = 
    { TaskId: string
      Title: string
      Description: string option
      Completed: bool } 

[<CLIMutable>]
type TaskResponse = 
    { TaskId: string
      Title: string
      Description: string option
      Completed: bool
      DateCompleted: DateTime option } 