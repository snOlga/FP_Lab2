module PropTest

open FsCheck
open NUnit.Framework
open Trie

[<Test>]
let identityElement () =
    let property (value: string list) =
        // e
        let emptryTrie = create
        // a
        let currentTrie = create |> insertList value
        // a * e
        let currentWithEmpty = mergeTrie currentTrie emptryTrie
        // e * a
        let EmptyWithCurrent = mergeTrie emptryTrie currentTrie

        areEqual currentWithEmpty EmptyWithCurrent
    Check.One(Config.QuickThrowOnFailure.WithMaxTest(1000), property)

[<Test>]
let associativity () =
    let property (value1: string list) (value2: string list) (value3: string list) =
        let A = create |> insertList value1
        let B = create |> insertList value2
        let C = create |> insertList value3

        // m is "merge"
        let AmBmC =  mergeTrie (mergeTrie A B) C
        let BmCmA = mergeTrie A (mergeTrie B C)

        areEqual AmBmC BmCmA
    Check.One(Config.QuickThrowOnFailure.WithMaxTest(1000),property)

[<Test>]
let propertyOfSet () =
    let property (value: string list) =
        let oneInsertion = create |> insertList value

        let manyInsertion =
            create
            |> insertList value
            |> insertList value
            |> insertList value
            |> insertList value

        let doubledOneInsertion = mergeTrie oneInsertion oneInsertion

        // E is "Equal"
        let oneEone = areEqual oneInsertion oneInsertion
        let oneEmany = areEqual oneInsertion manyInsertion
        let oneEdoubled = areEqual oneInsertion doubledOneInsertion

        oneEone && oneEmany && oneEdoubled

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(1000), property)

[<Test>]
let polymorphism () =
    let property (value: byte list) =
        let oneInsertion =
            create
            |> insertList value

        let manyInsertion =
            create
            |> insertList value 
            |> insertList value
            |> insertList value 
            |> insertList value
        
        let doubledOneInsertion = mergeTrie oneInsertion oneInsertion

        // E is "Equal"
        let oneEone = areEqual oneInsertion oneInsertion
        let oneEmany = areEqual oneInsertion manyInsertion
        let oneEdoubled = areEqual oneInsertion doubledOneInsertion

        oneEone && oneEmany && oneEdoubled

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(1000), property)