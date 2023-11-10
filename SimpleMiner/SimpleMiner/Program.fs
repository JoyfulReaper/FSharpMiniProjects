// For more information see https://aka.ms/fsharp-console-apps

open SimpleMiner

printfn "Hello from F#"

open Mining.Inventory
open Mining.Models
open Mining.Items

let inventory:Inventory = []

let io = Item.createOre Iron 1

let xinventory = Inventory.add io.Id io.Quantity inventory
let yinventory = Inventory.add (ItemId.CopperIngot |> int) io.Quantity xinventory
let zinventory = Inventory.remove (ItemId.CopperIngot |> int) 1 yinventory

printfn "%A" zinventory

let player =
    {
        Name = "Player"
        Inventory = []
    }

let result = Game.goMining player
printf "result: %A" result