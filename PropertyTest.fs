module PropTest

open NUnit.Framework
open Trie

let nullValueChar = ' '

[<Test>]
let monoid () =
    Assert.AreEqual(
        create nullValueChar
        |> insert nullValueChar "a"
        |> insert nullValueChar "",
        create nullValueChar
        |> insert nullValueChar ""
        |> insert nullValueChar "a"
    )

[<Test>]
let propertyOfSet () =
    Assert.AreEqual(
        { Value = ' '
          Children = seq { yield { Value = 'a'; Children = Seq.empty } } },
        create nullValueChar
        |> insert nullValueChar "a"
        |> insert nullValueChar "a"
    )

let nullValueInt = 0

[<Test>]
let polymorphism () =
    Assert.AreEqual(
        { Value = 0
          Children =
            seq {
                yield
                    { Value = 1
                      Children =
                        seq {
                            yield
                                { Value = 2
                                  Children = seq { yield { Value = 3; Children = Seq.empty } } }
                        } }
            } },
        create nullValueInt
        |> insert nullValueInt (seq { 
                                        yield 1 
                                        yield 2
                                        yield 3})
    )