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
