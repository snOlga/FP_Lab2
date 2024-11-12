module FP_Lab2

open System

open NUnit.Framework
open Trie

let nullValueChar = None

[<Test>]
let insertTest1 () =
    Assert.AreEqual(
        { isEnd = false
          Value = None
          Children =
            seq {
                yield
                    { isEnd = false
                      Value = Some 'a'
                      Children =
                        seq {
                            yield
                                { isEnd = false
                                  Value = Some 'b'
                                  Children =
                                    seq {
                                        yield
                                            { isEnd = true
                                              Value = Some 'c'
                                              Children = Seq.empty }
                                    } }
                        } }
            } },
        create |> insertWord "abc"
    )

[<Test>]
let insertTest2 () =
    Assert.AreEqual(
        { isEnd = false
          Value = None
          Children =
            seq {
                yield
                    { isEnd = true
                      Value = Some 'a'
                      Children = Seq.empty }

                yield
                    { isEnd = true
                      Value = Some 'b'
                      Children = Seq.empty }
            } },
        create |> insertWord "a" |> insertWord "b"
    )

[<Test>]
let insertTest3 () =
    Assert.AreEqual(
        { isEnd = false
          Value = None
          Children =
            seq {
                yield
                    { isEnd = false
                      Value = Some 'a'
                      Children =
                        seq {
                            yield
                                { isEnd = true
                                  Value = Some 'b'
                                  Children = Seq.empty

                                }
                        } }

                yield
                    { isEnd = true
                      Value = Some 'b'
                      Children = Seq.empty }
            } },
        create |> insertWord "ab" |> insertWord "b"
    )

[<Test>]
let insertTest4 () =
    Assert.AreEqual(
        { isEnd = false
          Value = None
          Children =
            seq {
                yield
                    { isEnd = false
                      Value = Some 'a'
                      Children =
                        seq {
                            yield
                                { isEnd = true
                                  Value = Some 'b'
                                  Children = Seq.empty }

                            yield
                                { isEnd = true
                                  Value = Some 'c'
                                  Children = Seq.empty }
                        } }
            } },
        create |> insertWord "ab" |> insertWord "ac"
    )

[<Test>]
let insertTest5 () =
    Assert.AreEqual(
        { isEnd = false
          Value = None
          Children =
            seq {
                yield
                    { isEnd = false
                      Value = Some 'a'
                      Children =
                        seq {
                            yield
                                { isEnd = true
                                  Value = Some 'b'
                                  Children = Seq.empty }
                        } }
            } },
        create |> insertWord "ab" |> insertWord "a"
    )

[<Test>]
let insertTest6 () =
    Assert.AreEqual(
        { isEnd = false
          Value = None
          Children =
            seq {
                yield
                    { isEnd = true
                      Value = Some 'a'
                      Children = Seq.empty }
            } },
        create |> insertWord "a" |> insertWord "a"
    )

[<Test>]
let insertTest7 () =
    Assert.AreEqual(
        { isEnd = false
          Value = None
          Children =
            seq {
                yield
                    { isEnd = false
                      Value = Some 'a'
                      Children =
                        seq {
                            yield
                                { isEnd = true
                                  Value = Some 'b'
                                  Children = Seq.empty }
                        } }

                yield
                    { isEnd = true
                      Value = Some 'c'
                      Children = Seq.empty }
            } },
        create |> insertWord "ab" |> insertWord "c"
    )

[<Test>]
let insertTest8 () =
    Assert.AreEqual(
        { isEnd = false
          Value = None
          Children =
            seq {
                yield
                    { isEnd = false
                      Value = Some 'a'
                      Children =
                        seq {
                            yield
                                { isEnd = true
                                  Value = Some 'b'
                                  Children = Seq.empty }
                        } }

                yield
                    { isEnd = false
                      Value = Some 'c'
                      Children =
                        seq {
                            yield
                                { isEnd = true
                                  Value = Some 'a'
                                  Children = Seq.empty }
                        } }
            } },
        create |> insertWord "ab" |> insertWord "ca"
    )

[<Test>]
let insertTest9 () =
    Assert.AreEqual(
        { isEnd = false
          Value = None
          Children =
            seq {
                yield
                    { isEnd = false
                      Value = Some 'a'
                      Children =
                        seq {
                            yield
                                { isEnd = false
                                  Value = Some 'a'
                                  Children =
                                    seq {
                                        yield
                                            { isEnd = true
                                              Value = Some 'a'
                                              Children = Seq.empty }
                                    } }
                        } }
            } },
        create |> insertWord "aaa"
    )

[<Test>]
let insertTest10 () =
    Assert.AreEqual(
        { isEnd = false
          Value = None
          Children =
            seq {
                yield
                    { isEnd = false
                      Value = Some 'a'
                      Children =
                        seq {
                            yield
                                { isEnd = false
                                  Value = Some 'b'
                                  Children =
                                    seq {
                                        yield
                                            { isEnd = true
                                              Value = Some 'q'
                                              Children = Seq.empty }

                                        yield
                                            { isEnd = true
                                              Value = Some 'c'
                                              Children = Seq.empty }
                                    } }

                            yield
                                { isEnd = false
                                  Value = Some 'd'
                                  Children =
                                    seq {
                                        yield
                                            { isEnd = true
                                              Value = Some 'k'
                                              Children = Seq.empty }
                                    } }
                        } }
            } },
        create
        |> insertWord "abq"
        |> insertWord "abc"
        |> insertWord "adk"
    )

[<Test>]
let insertTest11 () =
    Assert.AreEqual(
        { isEnd = false
          Value = None
          Children =
            seq {
                yield
                    { isEnd = true
                      Value = Some 'a'
                      Children = Seq.empty }
            } },
        create |> insertWord "a" |> insertWord ""
    )

[<Test>]
let insertTest12 () =
    Assert.AreEqual(
        { isEnd = false
          Value = None
          Children =
            seq {
                yield
                    { isEnd = true
                      Value = Some 'a'
                      Children = Seq.empty }
            } },
        create |> insertWord "" |> insertWord "a"
    )

[<Test>]
let insertTest13 () =
    Assert.AreEqual(
        { isEnd = false
          Value = None
          Children =
            seq {
                yield
                    { isEnd = false
                      Value = Some '1'
                      Children =
                        seq {
                            yield
                                { isEnd = false
                                  Value = Some '2'
                                  Children =
                                    seq {
                                        yield
                                            { isEnd = true
                                              Value = Some '3'
                                              Children = Seq.empty }
                                    } }
                        } }
            } },
        create |> insertWord 123
    )

[<Test>]
let deleteTest () =
    let expected = create |> insertWord "abc"
    let actual = create |> insertWord "abc" |> insertWord "bca" |> deleteWord "bca"
    Assert.True(areEqual expected actual)

[<Test>]
let mapTest () =
    let expected = create |> insertWord "ABC"
    let actual = create |> insertWord "abc" |> mapTrie (fun (value) -> (System.Char.ToUpper value))
    Assert.True(areEqual expected actual)

[<Test>]
let filterTest () =
    let expected = create |> insertWord "AB"
    let actual = create |> insertWord "ABc" |> filterTrie (fun (Some value) -> System.Char.IsUpper value)
    Assert.True(areEqual expected actual)

[<Test>]
let filterTest2 () =
    let expected = create |> insertWord "AB"
    let actual = create |> insertWord "ABcd" |> filterTrie (fun (Some value) -> System.Char.IsUpper value)
    Assert.True(areEqual expected actual)

[<Test>]
let filterTest3 () =
    let expected = create |> insertWord "AB"
    let actual = create |> insertWord "AcBcd" |> filterTrie (fun (Some value) -> System.Char.IsUpper value)
    Assert.True(areEqual expected actual)

[<Test>]
let filterTest4 () =
    let expected = create |> insertWord "AB" |> insertWord "DK"
    let actual = create |> insertWord "AcBcd" |> insertWord "DcKcd" |> filterTrie (fun (Some value) -> System.Char.IsUpper value)
    Assert.True(areEqual expected actual)

[<Test>]
let foldTest1 () =
    Assert.AreEqual(
        "abc",
        create
        |> insertWord "abc"
        |> foldTrie (fun state  ch -> state + ch) ""
    )

[<Test>]
let foldTest2 () =
    Assert.AreEqual(
        "abcbc",
        create
        |> insertWord "abc" |> insertWord "bc"
        |> foldTrie (fun state  ch -> state + ch) ""
    )

[<Test>]
let foldTest3 () =
    Assert.AreEqual(
        "abcardarw",
        create 
        |> insertWord "abc"
        |> insertWord "ard"
        |> insertWord "arw"
        |> foldTrie (fun state ch -> string state + ch) ""
    )
