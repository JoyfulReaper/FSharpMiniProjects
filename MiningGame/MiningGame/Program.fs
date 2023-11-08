namespace MiningGame

open MiningGame.Models
open MiningGame.Mining
open FsToolkit.ErrorHandling
open System

module Application =

    let showPlayerInventory player =
        ()
        
    let goMining player =
        result {
            let ore = Mining.goMining ()
            match ore with
            | Error e ->
                printfn "Something went wrong: %s" e.Message
            | Ok ore ->
                printfn "You mined %i %s." ore.Quantity ore.Name
                
        }
    
    let showMenu () =
        printfn "1) Go Mining"
    
    
    
    [<EntryPoint>]
    let main argv =
        let exitCode = 0
        
        let player = {
            Name = "Player"
            Inventory = []
            Money = 0
        }
        
        goMining player |> ignore
        
        exitCode