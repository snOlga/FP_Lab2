module Trie

type Node = { Value: char; Children: seq<Node> }

let create = { Value = ' '; Children = Seq.empty }

let rec insert (word: char seq) (current: Node) : Node =
    match Seq.length current.Children with // is children exsit
    | 0 when current.Value = ' ' -> // to root
        let newChild =
            insert
                (word)
                { Value = Seq.head word
                  Children = Seq.empty }

        { current with Children = (Seq.singleton (newChild)) }
    | 0 when Seq.length word = 1 -> // last char as leaf
        { current with
            Value = Seq.head word
            Children = Seq.empty }
    | 0 when Seq.length word > 1 -> // part of word as "leaf"
        let newChild =
            insert
                (Seq.tail word)
                { Value = Seq.head word
                  Children = Seq.empty }

        { Value = Seq.head word
          Children = Seq.append current.Children (Seq.singleton (newChild)) }
    | _ ->
        match Seq.tryFind (fun node -> node.Value = (Seq.head word)) current.Children with
        | Some child when Seq.length word <> 1 ->
            let updatedChild = insert (Seq.tail word) child

            { current with
                Children =
                    Seq.map
                        (fun node ->
                            if node.Value = (Seq.head word) then
                                updatedChild
                            else
                                node)
                        current.Children }
        | Some child when Seq.length word = 1 -> current
        | None when Seq.length word <> 1 ->
            let newChild =
                { Value = Seq.head word
                  Children =
                    Seq.singleton (
                        insert
                            (Seq.tail word)
                            { Value = (Seq.head word)
                              Children = Seq.empty }
                    ) }

            { current with Children = Seq.append current.Children (Seq.singleton newChild) }
        | None when Seq.length word = 1 ->
            let newChild =
                { Value = (Seq.head word)
                  Children = Seq.empty }

            { current with Children = Seq.append current.Children (Seq.singleton newChild) }
