namespace Todo.Abstractions

open Todo.Models

type RepositoryError =
    | TodoAlreadyExists of string
    | TodoDoesNotExist of string
    | ValidationError of ValidationError
    member this.Message =
        match this with
        | ValidationError e -> e.Message
        | TodoAlreadyExists msg -> msg |> sprintf "Todo Already Exists: %s"
        | TodoDoesNotExist msg -> msg |> sprintf "Todo Does Not Exist: %s"