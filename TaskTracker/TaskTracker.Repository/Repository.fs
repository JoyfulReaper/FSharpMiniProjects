namespace TaskTracker.Repository

open TaskTracker.Models.Task
open System

type ITaskRepository =
    abstract member Get : Guid -> Async<Task option>
    abstract member GetAll : unit -> Async<Task list>
    abstract member Create : Task -> Async<Task option>
    abstract member Update : Task -> Async<Task option>
    abstract member Delete : Guid -> Async<unit>
    abstract member EnsureTableCreated : unit -> Async<unit>