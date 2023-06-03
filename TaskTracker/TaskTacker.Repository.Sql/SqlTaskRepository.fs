module SqlTaskRepository

open System
open TaskTracker.Repository
open TaskTracker.Models
open Dapper.FSharp.SQLite
open TaskTracker.Repository.Sql.Connection

let getTask id =
    async {
        let taskTable = table<Task>
        let! result =
                select {
                    for p in taskTable do
                    where (p.TaskId = id)
                } |> getConnection().SelectAsync<Task> |> Async.AwaitTask

        return result |> Seq.head
    }

let getAllTasks() =
    async {
        let taskTable = table<Task>
        let! result =
                select {
                    for p in taskTable do
                    orderBy p.Title
                } |> getConnection().SelectAsync<Task> |> Async.AwaitTask

        return result |> Seq.toList
    }

let createTask task =
    async {
        let taskTable = table<Task>
        let taskToAdd: Task = { task with TaskId = Guid.NewGuid() }

        do!
            insert {
                into taskTable
                value taskToAdd
            }
            |> getConnection().InsertAsync
            |> Async.AwaitTask
            |> Async.Ignore

        let! result = getTask taskToAdd.TaskId
        return result
    }

let updateTask task =
    async {
        let taskTable = table<Task>
        do!
            update {
                for t in taskTable do
                set task
                where (t.TaskId = task.TaskId)
            } |> getConnection().UpdateAsync |> Async.AwaitTask |> Async.Ignore

        let! result = getTask task.TaskId
        return result
    }

let deleteTask id =
    async {
        let taskTable = table<Task>
        do!
            delete {
                for p in taskTable do
                where (p.TaskId = id)
            } |> getConnection().DeleteAsync |> Async.AwaitTask |> Async.Ignore
    }

type SqlTaskRepository =
    interface ITaskRepository with
        member __.Get id = 
            getTask id
        member __.GetAll() =
            getAllTasks()
        member __.Create task =
            createTask task
        member __.Update task =
            updateTask task
        member __.Delete id =
            deleteTask id