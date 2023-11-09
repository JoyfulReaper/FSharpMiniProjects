namespace MiningGame.Inventory

open MiningGame.Models
open FsToolkit.ErrorHandling

type InventoryError =
    | ItemNotFound of string
    member this.Message =
        match this with
        | ItemNotFound item -> sprintf "Item %s not found in inventory" item

module Result =
    let ofOption error result =
        match result with
        | Some value -> Ok value
        | None -> Error error

module Inventory =
    
    let addItem (inventory:Inventory) (item:Item) =
        let items =
            item::inventory
        
        let nonStackableItems =
            items
            |> List.filter (fun item -> not item.Stackable)
            
        let stackableItems =
            items
            |> List.filter (fun item -> item.Stackable)
            |> List.groupBy (fun item -> item.Id)
            |> List.map (fun (id, items) ->
                let quantity =
                    items
                    |> List.sumBy (fun item -> item.IntQuantity())
                    |> PositiveQuantity.create
                    |> Result.defaultValue PositiveQuantity.one
                { item with Quantity = quantity} )
                
        let newInventory =
            nonStackableItems @ stackableItems
        newInventory
            
        
        
    let removeItem (inventory:Inventory) (item:Item) (quantity:int)=
        result {
            let! item =
                inventory
                |> List.tryFind (fun item -> item.Id = item.Id)
                |> Result.ofOption (ItemNotFound item.Name)
                
            let newInventory =
                inventory
                |> List.map (fun i ->
                    if i.Id = item.Id then
                        if not i.Stackable then
                            let newQty = PositiveQuantity.subtract i.Quantity quantity |> Result.defaultValue PositiveQuantity.one
                            { i with Quantity = newQty } else i)
                        else
                            
        }
        