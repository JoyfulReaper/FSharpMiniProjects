namespace TaskTracker.Repository.Sql

open Microsoft.Data.Sqlite

module Connection =
    let private getConnectionString (dataSource : string) =
        sprintf "Data Source = %s;" dataSource

    let private getInMemoryConnectionString (dataSource : string) =
        sprintf "Data Source = %s; Mode = Memory; Cache = Shared;" dataSource

    let getConnection () =
        new SqliteConnection (getConnectionString "TaskTracker.db")

    let getInMemoryConnection () =
        new SqliteConnection (getInMemoryConnectionString "MASTER")



module DatabaseCreator =
    open Connection
    open TaskTraker.Repository.Extensions

    let ensureDatabaseReady =
        task {
            do!
                """
                CREATE TABLE IF NOT EXISTS [Task](
                    [TaskId] [TEXT] NOT NULL PRIMARY KEY,
                    [Title] [TEXT] NOT NULL,
                    [Description] [TEXT] NULL,
                    [Completed] [INTEGER] NOT NULL,
                    [DateCompleted] [TEXT] NULL
                )
                """
                |> getConnection().ExecuteIgnore
        }