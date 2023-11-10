namespace Mining.Items

type ItemId =
    | IronOre = 1
    | CopperOre = 2
    | IronIngot = 3
    | CopperIngot = 4

type Item =
    {
        Id: int
        Name: string
        Value: int
        Quantity: int
    }
        

type Material =
    | Iron
    | Copper

module Item =    
    let createOre material quantity =
        let id =
            match material with
            | Iron -> ItemId.IronOre
            | Copper -> ItemId.CopperOre
            
        let value =
            match material with
            | Iron -> 1
            | Copper -> 2
        { Id = id |> int; Name = sprintf "%s Ore" (material.ToString()); Value = value; Quantity = quantity }
        
    let createIngot material quantity =
        let id =
            match material with
            | Iron -> ItemId.IronIngot
            | Copper -> ItemId.CopperIngot
        
        let value =
            match material with
            | Iron -> 2
            | Copper -> 4
            
        { Id = id |> int; Name = sprintf "%s Ingot" (material.ToString()); Value = value; Quantity = quantity }
        
    let create (id:int) quantity =
        let itemId = ItemId.Parse(typeof<ItemId>, id.ToString()) |> unbox<ItemId>
        match itemId with
        | ItemId.IronOre -> createOre Iron quantity
        | ItemId.CopperOre -> createOre Copper quantity
        | ItemId.IronIngot -> createIngot Iron quantity
        | ItemId.CopperIngot -> createIngot Copper quantity
        | _ -> failwith "Invalid item id"