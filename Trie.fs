module Trie

open System

type Node<'T> =
    { Value: 'T option
      Children: seq<Node<'T>>
      isEnd: bool }

let create =
    { Value = None
      Children = Seq.empty
      isEnd = false }

let rec insert (word: seq<'a>) (current: Node<'T>) : Node<'T> =
    match Seq.length word with
    | 0 -> current
    | 1 ->
        match Seq.length current.Children with // is children exsit
        | 0 when current.Value = None -> // to root
            let newChild =
                insert
                    (word)
                    { Value = Seq.head word
                      Children = Seq.empty
                      isEnd = true }

            { current with Children = (Seq.singleton (newChild)) }
        | 0 when Seq.length word = 1 -> // last element as leaf
            { isEnd = true
              Value = Seq.head word
              Children = Seq.empty }
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
                    { isEnd = true
                      Value = Seq.head word
                      Children =
                        Seq.singleton (
                            insert
                                (Seq.tail word)
                                { Value = (Seq.head word)
                                  Children = Seq.empty
                                  isEnd = true }
                        ) }

                { current with Children = Seq.append current.Children (Seq.singleton newChild) }
            | None when Seq.length word = 1 ->
                let newChild =
                    { Value = (Seq.head word)
                      Children = Seq.empty
                      isEnd = true }

                { current with Children = Seq.append current.Children (Seq.singleton newChild) }
    | _ ->
        match Seq.length current.Children with // is children exsit
        | 0 when current.Value = None -> // to root
            let newChild =
                insert
                    (word)
                    { Value = Seq.head word
                      Children = Seq.empty
                      isEnd = false }

            { current with Children = (Seq.singleton (newChild)) }
        | 0 when Seq.length word > 1 -> // part of word as "leaf"
            let newChild =
                insert
                    (Seq.tail word)
                    { Value = Seq.head word
                      Children = Seq.empty
                      isEnd = false }

            { Value = Seq.head word
              Children = Seq.append current.Children (Seq.singleton (newChild))
              isEnd = false }
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
                    { isEnd = false
                      Value = Seq.head word
                      Children =
                        Seq.singleton (
                            insert
                                (Seq.tail word)
                                { Value = (Seq.head word)
                                  Children = Seq.empty
                                  isEnd = false }
                        ) }

                { current with Children = Seq.append current.Children (Seq.singleton newChild) }
            | None when Seq.length word = 1 ->
                let newChild =
                    { Value = (Seq.head word)
                      Children = Seq.empty
                      isEnd = false }

                { current with Children = Seq.append current.Children (Seq.singleton newChild) }

let insertWord (word: 'a) (current: Node<char>) : Node<char> =
    let wordSeq = word.ToString() |> Seq.map (fun a -> Some a) // Convert the word to a string and then to seq<char>
    insert wordSeq current

let rec delete (word: seq<'a>) (current: Node<'T>) : Node<'T> =
    match Seq.tryFind (fun node -> node.Value = (Seq.head word)) current.Children with
    | None -> current
    | Some child when Seq.length word > 0 ->
        { current with
            Children =
                seq {
                    for node in current.Children do
                        if node.Value <> (Seq.head word) then
                            yield node
                        else
                            yield! (delete (Seq.tail word) node).Children
                } }
    | Some child when Seq.length word = 0 ->
        { current with
            Children =
                Seq.append
                    (child.Children)
                    (Seq.map
                        (fun node ->
                            match node.Value with
                            | _ when node.Value <> (Seq.head word) -> node)
                        current.Children) }

let deleteWord (word: 'a) (current: Node<char>) : Node<char> =
    let wordSeq = word.ToString() |> Seq.map (fun a -> Some a) // Convert the word to a string and then to seq<char>
    delete wordSeq current

let rec mapTrie func (current: Node<'T>) : Node<'T> =
    match current.Value with
    | Some value ->
        let newValue = Some(func value)
        let newChildren = Seq.map (mapTrie func) current.Children

        { current with
            Value = newValue
            Children = newChildren }
    | None ->
        let newChildren = Seq.map (mapTrie func) current.Children

        { current with
            Value = None
            Children = newChildren }

let rec filterTrie (func) (current: Node<'T>) : Node<'T> =
    match Seq.length current.Children with
    | len when len <> 0 ->
        { current with
            Children =
                seq {
                    for child in current.Children do
                        match child.Value with
                        | value when
                            func value = true
                            && (filterTrie func child).Value <> None
                            ->
                            yield filterTrie func child
                        | value when func value = false -> yield! (filterTrie func child).Children
                        | _ -> ()
                } }
    | 0 when func current.Value = true -> { current with isEnd = true }
    | 0 when func current.Value = false -> { current with Value = None }

let rec foldTrie (func) state (current: Node<'T>) =
    match current.Value with
    | Some(x) ->
        Seq.fold (foldTrie func) (func state current.Value.Value) current.Children
    | None ->
        Seq.fold (foldTrie func) state current.Children

let rec mergeTrie (leftOne: Node<'T>) (rightOne: Node<'T>) : Node<'T> =
    let mergeChildren children1 children2 =
        children1
        |> Seq.append children2
        |> Seq.groupBy (fun child -> child.Value)
        |> Seq.map (fun (value, nodes) -> nodes |> Seq.reduce mergeTrie)

    { leftOne with Children = (mergeChildren leftOne.Children rightOne.Children) }

let rec areEqual node1 node2 = 
    node1.Value = node2.Value &&  (Seq.isEmpty node1.Children && Seq.isEmpty node2.Children) || Seq.forall2 (fun child1 child2 -> areEqual child1 child2) node1.Children node2.Children