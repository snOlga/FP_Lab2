module FP_Lab2

open NUnit.Framework
open Trie

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
        create |> insert "abc"
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
        create |> insert "a" |> insert "b"
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
        create |> insert "ab" |> insert "b"
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
        create |> insert "ab" |> insert "ac"
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
        create |> insert "ab" |> insert "a"
    )

[<Test>]
let insertTest6 () =
    Assert.AreEqual(
        { Value = ' '
          Children = seq { yield { Value = 'a'; Children = Seq.empty } } },
        create |> insert "a" |> insert "a"
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
        create |> insert "ab" |> insert "c"
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
        create |> insert "ab" |> insert "ca"
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
        create |> insert "aaa"
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
        create
        |> insert "abq"
        |> insert "abc"
        |> insert "adk"
    )

[<Test>]
let insertTest11 () =
    Assert.AreEqual(
        { Value = ' '
          Children = seq { yield { Value = 'a'; Children = Seq.empty } } },
        create |> insert "a" |> insert ""
    )

[<Test>]
let insertTest12 () =
    Assert.AreEqual(
        { Value = ' '
          Children = seq { yield { Value = 'a'; Children = Seq.empty } } },
        create |> insert "" |> insert "a"
    )

// [<Test>]
// let deleteTest () =
//     Assert.AreEqual(
//         { Value = ' '
//           Children =
//             seq {
//                 yield
//                     { Value = 'a'
//                       Children =
//                         seq {
//                             yield
//                                 { Value = 'b'
//                                   Children = seq { yield { Value = 'c'; Children = Seq.empty } } }
//                         } }
//             } },
//     create |> insert "abc" |> insert "bca" |> delete "bca"
//   )

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
        create
        |> insert "abc"
        |> mapTrie System.Char.ToUpper
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
        create
        |> insert "ABc"
        |> filterTrie System.Char.IsUpper
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
        create
        |> insert "ABcd"
        |> filterTrie System.Char.IsUpper
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
        create
        |> insert "AcBcd"
        |> filterTrie System.Char.IsUpper
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
        create
        |> insert "AcBcd"
        |> insert "DcKcd"
        |> filterTrie System.Char.IsUpper
    )

[<Test>]
let foldTest1 () =
    Assert.AreEqual( "abc ",
        create
        |> insert "cba"
        |> foldTrie (fun state ch -> string ch + state) ""
    )

[<Test>]
let foldTest2 () =
    Assert.AreEqual( "cbcba ",
        create
        |> insert "abc"
        |> insert "bc"
        |> foldTrie (fun state ch -> string ch + state) ""
    )

[<Test>]
let foldTest3 () =
    Assert.AreEqual( "dwrcba ",
        create
        |> insert "abc"
        |> insert "arw"
        |> insert "ard"
        |> foldTrie (fun state ch -> string ch + state) ""
    )