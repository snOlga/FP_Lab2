module Trie

type Node = { Value: char; Children: seq<Node> }

let create = { Value = ' '; Children = Seq.empty }

let rec insert (word: char seq) (current: Node) : Node =
    match Seq.length current.Children with
    | 0 when current.Value = ' ' ->
        let newChild =
            insert
                (word)
                { Value = Seq.head word
                  Children = Seq.empty }

        { current with Children = (Seq.singleton (newChild)) }
    | 0 when Seq.length word = 1 ->
        { current with
            Value = Seq.head word
            Children = Seq.empty }
    | 0 when Seq.length word > 1 ->
        let newChild =
            insert
                (Seq.tail word)
                { Value = Seq.head word
                  Children = Seq.empty }

        { Value = Seq.head word
          Children = Seq.append current.Children (Seq.singleton (newChild)) }
    | _ ->
        { Value = Seq.head word
                  Children = Seq.empty }
        //TODO: 
