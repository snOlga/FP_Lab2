module FP_Lab2

open NUnit.Framework
open Trie

[<Test>]
let insertToEmptyTest () =
    Assert.AreEqual ( {
        Value = ' '
        Children =
        seq {
            yield {
                Value = 'a'
                Children = 
                seq {
                    yield {
                        Value = 'b'
                        Children = 
                        seq {
                            yield {
                                Value = 'c'
                                Children = Seq.empty
                            }
                        }
                    }
                }
            }
        }
    } , create |> insert "abc")