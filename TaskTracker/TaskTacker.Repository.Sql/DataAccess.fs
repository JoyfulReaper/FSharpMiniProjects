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



module Tasks =
    open Connection
    open TaskTraker.Repository.Extensions

    let CreateTable =
        task {
            do!
                """
                CREATE TABLE [Persons](
                    [Id] [TEXT] NOT NULL PRIMARY KEY,
                    [FirstName] [TEXT] NOT NULL,
                    [LastName] [TEXT] NOT NULL,
                    [Position] [INTEGER] NOT NULL,
                    [DateOfBirth] [TEXT] NULL
                )
                """
                |> getConnection().ExecuteIgnore
            return ()
        }