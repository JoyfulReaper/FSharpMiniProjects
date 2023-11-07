namespace MiningGame.Tests

open System
open MiningGame.Models
open MiningGame.Mining
open Microsoft.VisualStudio.TestTools.UnitTesting

[<TestClass>]
type TestClass () =

    [<TestMethod>]
    member this.CanCreate () =
        let result = Item.createOre Dirt 2
        printfn "%A" result
        Assert.IsTrue(true)
        
    [<TestMethod>]
    member this.CanMine () =
        let result = Mining.goMining ()
        printfn "%A" result
        Assert.IsTrue(true)