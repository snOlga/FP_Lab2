module FP_Lab2

open System

open NUnit.Framework
open Trie

let nullValueChar = ' '

[<Test>]
let insertTest1 () =
    Assert.AreEqual(
        { Value = ' '
          Children =
            seq {
                yield
                    { Value = 'a'
                      Children =
                        seq {
                            yield
                                { Value = 'b'
                                  Children = seq { yield { Value = 'c'; Children = Seq.empty } } }
                        } }
            } },
        create nullValueChar |> insert nullValueChar "abc"
    )

[<Test>]
let insertTest2 () =
    Assert.AreEqual(
        { Value = ' '
          Children =
            seq {
                yield { Value = 'a'; Children = Seq.empty }
                yield { Value = 'b'; Children = Seq.empty }
            } },
        create nullValueChar
        |> insert nullValueChar "a"
        |> insert nullValueChar "b"
    )

[<Test>]
let insertTest3 () =
    Assert.AreEqual(
        { Value = ' '
          Children =
            seq {
                yield
                    { Value = 'a'
                      Children =
                        seq {
                            yield
                                { Value = 'b'
                                  Children = Seq.empty

                                }
                        } }

                yield { Value = 'b'; Children = Seq.empty }
            } },
        create nullValueChar
        |> insert nullValueChar "ab"
        |> insert nullValueChar "b"
    )

[<Test>]
let insertTest4 () =
    Assert.AreEqual(
        { Value = ' '
          Children =
            seq {
                yield
                    { Value = 'a'
                      Children =
                        seq {
                            yield { Value = 'b'; Children = Seq.empty }
                            yield { Value = 'c'; Children = Seq.empty }
                        } }
            } },
        create nullValueChar
        |> insert nullValueChar "ab"
        |> insert nullValueChar "ac"
    )

[<Test>]
let insertTest5 () =
    Assert.AreEqual(
        { Value = ' '
          Children =
            seq {
                yield
                    { Value = 'a'
                      Children = seq { yield { Value = 'b'; Children = Seq.empty } } }
            } },
        create nullValueChar
        |> insert nullValueChar "ab"
        |> insert nullValueChar "a"
    )

[<Test>]
let insertTest6 () =
    Assert.AreEqual(
        { Value = ' '
          Children = seq { yield { Value = 'a'; Children = Seq.empty } } },
        create nullValueChar
        |> insert nullValueChar "a"
        |> insert nullValueChar "a"
    )

[<Test>]
let insertTest7 () =
    Assert.AreEqual(
        { Value = ' '
          Children =
            seq {
                yield
                    { Value = 'a'
                      Children = seq { yield { Value = 'b'; Children = Seq.empty } } }

                yield { Value = 'c'; Children = Seq.empty }
            } },
        create nullValueChar
        |> insert nullValueChar "ab"
        |> insert nullValueChar "c"
    )

[<Test>]
let insertTest8 () =
    Assert.AreEqual(
        { Value = ' '
          Children =
            seq {
                yield
                    { Value = 'a'
                      Children = seq { yield { Value = 'b'; Children = Seq.empty } } }

                yield
                    { Value = 'c'
                      Children = seq { yield { Value = 'a'; Children = Seq.empty } } }
            } },
        create nullValueChar
        |> insert nullValueChar "ab"
        |> insert nullValueChar "ca"
    )

[<Test>]
let insertTest9 () =
    Assert.AreEqual(
        { Value = ' '
          Children =
            seq {
                yield
                    { Value = 'a'
                      Children =
                        seq {
                            yield
                                { Value = 'a'
                                  Children = seq { yield { Value = 'a'; Children = Seq.empty } } }
                        } }
            } },
        create nullValueChar |> insert nullValueChar "aaa"
    )

[<Test>]
let insertTest10 () =
    Assert.AreEqual(
        { Value = ' '
          Children =
            seq {
                yield
                    { Value = 'a'
                      Children =
                        seq {
                            yield
                                { Value = 'b'
                                  Children =
                                    seq {
                                        yield { Value = 'q'; Children = Seq.empty }
                                        yield { Value = 'c'; Children = Seq.empty }
                                    } }

                            yield
                                { Value = 'd'
                                  Children = seq { yield { Value = 'k'; Children = Seq.empty } } }
                        } }
            } },
        create nullValueChar
        |> insert nullValueChar "abq"
        |> insert nullValueChar "abc"
        |> insert nullValueChar "adk"
    )

[<Test>]
let insertTest11 () =
    Assert.AreEqual(
        { Value = ' '
          Children = seq { yield { Value = 'a'; Children = Seq.empty } } },
        create nullValueChar
        |> insert nullValueChar "a"
        |> insert nullValueChar ""
    )

[<Test>]
let insertTest12 () =
    Assert.AreEqual(
        { Value = ' '
          Children = seq { yield { Value = 'a'; Children = Seq.empty } } },
        create nullValueChar
        |> insert nullValueChar ""
        |> insert nullValueChar "a"
    )

let nullValueInt = 0

[<Test>]
let insertTest13 () =
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
        |> insert
            nullValueInt
            (seq {
                yield 1
                yield 2
                yield 3
            })
    )

[<Test>]
let deleteTest () =
    Assert.AreEqual(
        { Value = ' '
          Children =
            seq {
                yield
                    { Value = 'a'
                      Children =
                        seq {
                            yield
                                { Value = 'b'
                                  Children = seq { yield { Value = 'c'; Children = Seq.empty } } }
                        } }
            } },
        create nullValueChar
        |> insert nullValueChar "abc"
        |> insert nullValueChar "bca"
        |> delete "bca"
    )

[<Test>]
let mapTest () =
    Assert.AreEqual(
        { Value = ' '
          Children =
            seq {
                yield
                    { Value = 'A'
                      Children =
                        seq {
                            yield
                                { Value = 'B'
                                  Children = seq { yield { Value = 'C'; Children = Seq.empty } } }
                        } }
            } },
        create nullValueChar
        |> insert nullValueChar "abc"
        |> mapTrie nullValueChar System.Char.ToUpper
    )

[<Test>]
let filterTest () =
    Assert.AreEqual(
        { Value = ' '
          Children =
            seq {
                yield
                    { Value = 'A'
                      Children = seq { yield { Value = 'B'; Children = Seq.empty } } }
            } },
        create nullValueChar
        |> insert nullValueChar "ABc"
        |> filterTrie nullValueChar System.Char.IsUpper
    )

[<Test>]
let filterTest2 () =
    Assert.AreEqual(
        { Value = ' '
          Children =
            seq {
                yield
                    { Value = 'A'
                      Children = seq { yield { Value = 'B'; Children = Seq.empty } } }
            } },
        create nullValueChar
        |> insert nullValueChar "ABcd"
        |> filterTrie nullValueChar System.Char.IsUpper
    )

[<Test>]
let filterTest3 () =
    Assert.AreEqual(
        { Value = ' '
          Children =
            seq {
                yield
                    { Value = 'A'
                      Children = seq { yield { Value = 'B'; Children = Seq.empty } } }
            } },
        create nullValueChar
        |> insert nullValueChar "AcBcd"
        |> filterTrie nullValueChar System.Char.IsUpper
    )

[<Test>]
let filterTest4 () =
    Assert.AreEqual(
        { Value = ' '
          Children =
            seq {
                yield
                    { Value = 'A'
                      Children = seq { yield { Value = 'B'; Children = Seq.empty } } }

                yield
                    { Value = 'D'
                      Children = seq { yield { Value = 'K'; Children = Seq.empty } } }
            } },
        create nullValueChar
        |> insert nullValueChar "AcBcd"
        |> insert nullValueChar "DcKcd"
        |> filterTrie nullValueChar System.Char.IsUpper
    )

[<Test>]
let foldTest1 () =
    Assert.AreEqual(
        "abc ",
        create nullValueChar
        |> insert nullValueChar "cba"
        |> foldTrie (fun state ch -> string ch + state) ""
    )

[<Test>]
let foldTest2 () =
    Assert.AreEqual(
        "cbcba ",
        create nullValueChar
        |> insert nullValueChar "abc"
        |> insert nullValueChar "bc"
        |> foldTrie (fun state ch -> string ch + state) ""
    )

[<Test>]
let foldTest3 () =
    Assert.AreEqual(
        "dwrcba ",
        create nullValueChar
        |> insert nullValueChar "abc"
        |> insert nullValueChar "arw"
        |> insert nullValueChar "ard"
        |> foldTrie (fun state ch -> string ch + state) ""
    )
