module PropTest

open FsCheck
open NUnit.Framework
open Trie

[<Test>]
let monoid1 () =
    let property (value: string) =
        let result1 = create |> insertWord value |> mergeTrie  create
        let result2 = create |> mergeTrie  (create |> insertWord value)
        let result3 = create |> insertWord value

        (areEqual result1 result2) && (areEqual result1 result3) && (areEqual result2 result3)

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10).WithEndSize(5), property)

[<Test>]
let monoid2 () =
    let property (value1: string) (value2: string) (value3: string) =
        let trie1 = create |> insertWord value1
        let trie2 = create |> insertWord value2
        let trie3 = create |> insertWord value3

        let result1 =  mergeTrie trie1 (mergeTrie trie2 trie3)
        let result2 = mergeTrie (mergeTrie trie1 trie2) trie3

        areEqual result1 result2
    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10).WithEndSize(5),property)

[<Test>]
let propertyOfSet () =
    let property (value: string) =
        let result1 = create |> insertWord value

        let result2 =
            create
            |> insertWord value
            |> insertWord value
            |> insertWord value
            |> insertWord value
        
        let trie = create |> insertWord value

        let result3 = mergeTrie trie trie

        (areEqual result1 result2) && (areEqual result1 result3) && (areEqual result2 result3)

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10), property)

[<Test>]
let polymorphism () =
    let property (value: byte list) =
        let result1 =
            create
            |> insertWord value

        let result2 =
            create
            |> insertWord value 
            |> insertWord value
            |> insertWord value 
            |> insertWord value
        
        let trie = create |> insertWord value
        let result3 = mergeTrie trie trie

        (areEqual result1 result2) && (areEqual result1 result3) && (areEqual result2 result3)

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10), property)