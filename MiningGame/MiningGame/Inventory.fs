namespace MiningGame.Inventory

open MiningGame.Models
open FsToolkit.ErrorHandling

type InventoryError =
    | StackableItemHasInventory
    | NonStackableItemHasQuantityGreaterThanOne
    member this.Message =
        match this with
        | StackableItemHasInventory -> "Stackable item has inventory"

module Inventory =
    
    let addItem (item:Item) (inventory:Inventory) =
        result {
            let items =
                item::inventory

            let nonStackableItems =
                items
                |> List.filter (fun i -> not i.Stackable)
                
            let stackableItems =
                items
                |> List.filter (fun i ->  i.Stackable)
                |> List.groupBy (fun i -> i.Id)
                |> List.map (fun (id, items) ->
                     let quantity = items |> List.sumBy (fun i -> i.Quantity |> PositiveQuantity.toInt)
                     let positiveQuantity = PositiveQuantity.create quantity
                     { Id = id
                       Name = item.Name
                       Description  = item.Description
                       CraftRecipe = item.CraftRecipe
                       Material = item.Material
                       Inventory = None // Only non-stackable can have an inventory - TODO need to enforce this somehow
                       Quantity = items |> List.sumBy (fun i -> i.Quantity |> PositiveQuantity.toInt)
                       Value = item.Value; Stackable = item.Stackable })
                
            let newInventory =
                nonStackableItems @ stackableItems
                |> List.sortBy (fun i -> i.Name)
            
            return newInventory
        }
        
        
    