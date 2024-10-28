module PropTest

open FsCheck
open NUnit.Framework
open Trie

let rec areEqual node1 node2 = 
        node1.Value = node2.Value &&  (Seq.isEmpty node1.Children && Seq.isEmpty node2.Children) ||
        Seq.forall2 (fun child1 child2 -> areEqual child1 child2) node1.Children node2.Children

let nullValueChar = ' '

[<Test>]
let monoid1 () =
    let property (value: string) =
        let result1 =
            create nullValueChar
            |> insert nullValueChar value
            |> insert nullValueChar ""

        let result2 =
            create nullValueChar
            |> insert nullValueChar ""
            |> insert nullValueChar value

        let result3 =
            create nullValueChar
            |> insert nullValueChar value
        
        printf "%s" value

        (areEqual result1 result2) && (areEqual result1 result3) && (areEqual result2 result3)

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10), property)

[<Test>]
let monoid2 () =
    let property (value1: string) (value2: string) =
        let result1 =
            create nullValueChar
            |> insert nullValueChar value1
            |> insert nullValueChar (Seq.tail value1)
            |> insert nullValueChar value2

        let result2 =
            create nullValueChar
            |> insert nullValueChar value2
            |> insert nullValueChar (Seq.tail value1)
            |> insert nullValueChar value1

        areEqual result1 result2

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10).WithStartSize(2), property)


[<Test>]
let propertyOfSet () =
    let property (value: string) =
        let result1 =
            create nullValueChar
            |> insert nullValueChar value 

        let result2 =
            create nullValueChar
            |> insert nullValueChar value 
            |> insert nullValueChar value
            |> insert nullValueChar value
            |> insert nullValueChar value

        areEqual result1 result2

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10), property)
    Assert.Pass()

let nullValueByte = 0uy

[<Test>]
let polymorphism () =
    let property (value: byte) =
        let intSeq = 
            seq {
                yield value
            }
        
        let result1 =
            create nullValueByte
            |> insert nullValueByte intSeq 

        let result2 =
            create nullValueByte
            |> insert nullValueByte intSeq 
            |> insert nullValueByte intSeq
            |> insert nullValueByte intSeq 
            |> insert nullValueByte intSeq

        areEqual result1 result2

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10), property)
    Assert.Pass()