namespace MiningGame.Models.Errors

type ModelError =
    | InvalidMaterial of string
    member this.Message =
        match this with
        | InvalidMaterial material -> sprintf "Invalid material: %s" material

