﻿namespace TaskTracker.Repository

open TaskTracker.Models
open System

type ITaskRepository =
    abstract member Get : Guid -> Async<Task>
    abstract member GetAll : unit -> Async<Task list>
    abstract member Create : Task -> Async<Task>
    abstract member Update : Task -> Async<Task>
    abstract member Delete : Guid -> Async<unit>