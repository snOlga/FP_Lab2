module PropTest

open FsCheck
open NUnit.Framework
open Trie

let rec areEqual node1 node2 = 
        node1.Value = node2.Value &&  (Seq.isEmpty node1.Children && Seq.isEmpty node2.Children) ||
        Seq.forall2 (fun child1 child2 -> areEqual child1 child2) node1.Children node2.Children

let nullValueChar = ' '

[<Test>]
let monoid () =
    let property (value: string) =
        let result1 =
            create nullValueChar
            |> insert nullValueChar value
            |> insert nullValueChar ""

        let result2 =
            create nullValueChar
            |> insert nullValueChar ""
            |> insert nullValueChar value

        areEqual result1 result2

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(20), property)


[<Test>]
let propertyOfSet () =
    let property (value: char) =
        let result1 = { 
                Value = ' '
                Children = seq { yield { Value = value; Children = Seq.empty } } }

        let charSeq = 
            seq {
                yield value
            }

        let result2 =
            create nullValueChar
            |> insert nullValueChar charSeq 
            |> insert nullValueChar charSeq

        areEqual result1 result2

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(20), property)
    Assert.Pass()

let nullValueByte = 0uy

[<Test>]
let polymorphism () =
    let property (value: byte) =
        let result1 = { 
                Value = nullValueByte
                Children = seq { yield { Value = value; Children = Seq.empty } } }

        let intSeq = 
            seq {
                yield value
            }

        let result2 =
            create nullValueByte
            |> insert nullValueByte intSeq 
            |> insert nullValueByte intSeq

        areEqual result1 result2

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(20), property)
    Assert.Pass()