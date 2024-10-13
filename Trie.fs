module Trie

type Node = { Value: char; Children: seq<Node> }

let create = { Value = ' '; Children = Seq.empty }

let rec insert (word: char seq) (current: Node) : Node =
    match Seq.length word with
    | 0 -> current
    | _ ->
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

// let rec delete (word: char seq) (current: Node) : Node =
//     match Seq.tryFind (fun node -> node.Value = (Seq.head word)) current.Children with
//     | None -> current
//     | Some child when Seq.length word > 1 ->
//         { current with
//             Children =
//                 seq {
//                     for node in current.Children do
//                         if node.Value <> (Seq.head word) then
//                             yield node
//                         else
//                             yield! (delete (Seq.tail word) node).Children
//                 } }
//     | Some child when Seq.length word = 1 ->
//         { current with
//             Children =
//                 Seq.append
//                     (child.Children)
//                     (Seq.map
//                         (fun node ->
//                             match node.Value with
//                             | _ when node.Value <> (Seq.head word) -> node)
//                         current.Children) }


let rec mapTrie (func) (current: Node) : Node =
    match Seq.length current.Children with
    | 0 -> { current with Value = func current.Value }
    | _ ->
        { Value = func current.Value
          Children = Seq.map (fun child -> mapTrie func child) current.Children }

let rec filterTrie (func) (current: Node) : Node =
    match Seq.length current.Children with
    | len when len <> 0 ->
        { current with
            Children =
                seq {
                    for child in current.Children do
                        match child.Value with
                        | value when func value = true && (filterTrie func child).Value <> ' ' -> yield filterTrie func child
                        | value when func value = false -> yield! (filterTrie func child).Children
                        | _ -> ()
                } }
    | 0 when func current.Value = true -> current
    | 0 when func current.Value = false -> { current with Value = ' ' }
