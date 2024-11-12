module PropTest

open FsCheck
open NUnit.Framework
open Trie

[<Test>]
let monoid1 () =
    let property (value: string list) =
        let result1 = create |> insertList value |> mergeTrie  create
        let result2 = create |> mergeTrie  (create |> insertList value)
        let result3 = create |> insertList value

        (areEqual result1 result2) && (areEqual result1 result3) && (areEqual result2 result3)

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10).WithEndSize(5), property)

[<Test>]
let monoid2 () =
    let property (value1: string list) (value2: string list) (value3: string list) =
        let trie1 = create |> insertList value1
        let trie2 = create |> insertList value2
        let trie3 = create |> insertList value3

        let result1 =  mergeTrie trie1 (mergeTrie trie2 trie3)
        let result2 = mergeTrie (mergeTrie trie1 trie2) trie3

        areEqual result1 result2
    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10).WithEndSize(5),property)

[<Test>]
let propertyOfSet () =
    let property (value: string list) =
        let result1 = create |> insertList value

        let result2 =
            create
            |> insertList value
            |> insertList value
            |> insertList value
            |> insertList value
        
        let trie = create |> insertList value

        let result3 = mergeTrie trie trie

        (areEqual result1 result2) && (areEqual result1 result3) && (areEqual result2 result3)

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10), property)

[<Test>]
let polymorphism () =
    let property (value: byte list) =
        let result1 =
            create
            |> insertList value

        let result2 =
            create
            |> insertList value 
            |> insertList value
            |> insertList value 
            |> insertList value
        
        let trie = create |> insertList value
        let result3 = mergeTrie trie trie

        (areEqual result1 result2) && (areEqual result1 result3) && (areEqual result2 result3)

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10), property)