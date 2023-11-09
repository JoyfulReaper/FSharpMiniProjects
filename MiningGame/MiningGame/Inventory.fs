namespace MiningGame.Inventory

open MiningGame.Models
open FsToolkit.ErrorHandling

type InventoryError =
    | StackableItemHasInventory
    | NonStackableItemHasQuantityGreaterThanOne
    member this.Message =
        match this with
        | StackableItemHasInventory -> "Stackable item has inventory"
        | NonStackableItemHasQuantityGreaterThanOne -> "Non stackable item has quantity greater than one"

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
            
        
        
    