namespace MiningGame.Tests

open System
open MiningGame.Models
open MiningGame.Mining
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestClass () =

    let getOre () =
        let ore = Item.createOre Iron PositiveQuantity.one |> Result.toOption
        ore.Value
    
    [<TestMethod>]
    member this.CanCreate () =
        let result = Item.createOre Dirt PositiveQuantity.one
        printfn "%A" result
        Assert.IsTrue(true)
        
    [<TestMethod>]
    member this.CanMine () =
        let result = Mining.goMining ()
        printfn "%A" result
        Assert.IsTrue(true)
        
        
    [<TestMethod>]
    member this.CanSmelt () =
        let ore = getOre()
        let result = Mining.smelt ore
        printfn "%A" result
        Assert.IsTrue(true)