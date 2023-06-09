﻿namespace TaskTracker.Models
open System

[<CLIMutable>]
type TaskRequest = 
    { Title: string
      Description: string } 

[<CLIMutable>]
type TaskUpdateRequest = 
    { TaskId: string
      Title: string
      Description: string
      Completed: bool
      DateCompleted: Nullable<DateTime> } 

[<CLIMutable>]
type TaskResponse = 
    { TaskId: string
      Title: string
      Description: string
      Completed: bool
      DateCompleted: Nullable<DateTime> } 

[<CLIMutable>]
type ErrorResponse = 
    { Message: string }