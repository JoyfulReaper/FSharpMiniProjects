namespace MiningGame.Mining

open System
open MiningGame.Models
open FsToolkit.ErrorHandling
open MiningGame.Models.Errors

type MiningError =
    | ModelError of ModelError
    member this.Message =
        match this with
        | ModelError e -> e.Message

module Mining =
    
    let goMining () =
        result {
            // 5 percent chance for diamond, 10 percent chance for gold, 15 percent chance for iron, 30 percent chance for copper, 30 percent chance for stone
            let randNum = Random.Shared.Next(1, 101)
            
            let! ore =
                (if randNum <= 5 then
                    Ok <| Item.createDiamond 1
                elif randNum <= 15 then
                    Item.createOre Gold 1
                elif randNum <= 30 then
                    Item.createOre Iron 1
                elif randNum <= 60 then
                    Item.createOre Copper 1
                else
                    Ok <| Item.createStone 1) |> Result.mapError ModelError
            return ore   
        }
        
        