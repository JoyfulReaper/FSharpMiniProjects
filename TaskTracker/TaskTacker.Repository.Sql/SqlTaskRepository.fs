namespace TaskTracker.Repository.Sql

    module TaskRepository =
        open TaskTracker.Repository.Sql.Entities.Entities
        open System
        open TaskTracker.Repository
        open TaskTracker.Models.Task
        open Dapper.FSharp.SQLite
        open TaskTracker.Repository.Sql.Connection
        open TaskTracker.Repository.Sql.Entities

        let getTable =
            table'<TaskEntity> "Task"

        let getTask id =
            async {
                let! result =
                        select {
                            for p in getTable do
                            where (p.TaskId = id)
                        } |> getConnection().SelectAsync<TaskEntity> |> Async.AwaitTask

                match Seq.length result with
                | 0 -> return None
                | _ -> return Some (result |> Seq.head |> toDomain)
            }

        let getAllTasks() =
            async {
                let! result =
                        select {
                            for p in getTable do
                            orderBy p.Title
                        } |> getConnection().SelectAsync<TaskEntity> |> Async.AwaitTask


                match Seq.length result with
                | 0 -> return []
                | _ -> return result |> Seq.map toDomain |> Seq.toList
            }

        let createTask (task: Task) =
            async {
                let taskToAdd = toEntity task

                do!
                    insert {
                        into getTable
                        value taskToAdd
                    }
                    |> getConnection().InsertAsync
                    |> Async.AwaitTask
                    |> Async.Ignore

                let! result = getTask taskToAdd.TaskId
                return result
            }

        let updateTask (task: Task) =
            async {
                let taskToUpdate = toEntity task
                do!
                    update {
                        for t in getTable do
                        set taskToUpdate
                        where (t.TaskId = task.TaskId)
                    } |> getConnection().UpdateAsync |> Async.AwaitTask |> Async.Ignore

                let! result = getTask task.TaskId
                return result
            }

        let deleteTask id =
            async {
                do!
                    delete {
                        for p in getTable do
                        where (p.TaskId = id)
                    } |> getConnection().DeleteAsync |> Async.AwaitTask |> Async.Ignore
            }

        type SqlTaskRepository() =
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
                member __.EnsureTableCreated() =
                    async {
                        do! DatabaseCreator.ensureDatabaseReady |> Async.AwaitTask
                    }