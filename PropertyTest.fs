module PropTest

// open FsCheck
// open NUnit.Framework
// open Trie

// let nullStr = " "

// [<Test>]
// let monoid1 () =
//     let property (value: string list) =
//         let result1 =
//             create nullStr
//             |> insert nullStr (List.toSeq value)
//             |> mergeTrie  (create nullStr)

//         let result2 =
//             create nullStr
//             |> mergeTrie  (create nullStr)
//             |> insert nullStr value

//         let result3 =
//             create nullStr
//             |> insert nullStr value

//         (areEqual result1 result2) && (areEqual result1 result3) && (areEqual result2 result3)

//     Check.One(Config.QuickThrowOnFailure.WithMaxTest(10).WithEndSize(5), property)

// [<Test>]
// let monoid2 () =
//     let property (value1: string list) (value2: string list) (value3: string list) =
//         let trie1 = create nullStr |> insert nullStr (List.toSeq value1)
//         let trie2 = create nullStr |> insert nullStr (List.toSeq value2)
//         let trie3 = create nullStr |> insert nullStr (List.toSeq value3)

//         let result1 =  mergeTrie trie1 (mergeTrie trie2 trie3)
//         let result2 = mergeTrie (mergeTrie trie1 trie2) trie3

//         areEqual result1 result2
//     Check.One(Config.QuickThrowOnFailure.WithMaxTest(10).WithEndSize(5),property)

// [<Test>]
// let propertyOfSet () =
//     let property (value: string list) =
//         let result1 =
//             create nullStr
//             |> insert nullStr (List.toSeq value) 

//         let result2 =
//             create nullStr
//             |> insert nullStr (List.toSeq value) 
//             |> insert nullStr (List.toSeq value)
//             |> insert nullStr (List.toSeq value)
//             |> insert nullStr (List.toSeq value)
        
//         let trie = create nullStr |> insert nullStr (List.toSeq value)
//         let result3 = mergeTrie trie trie

//         (areEqual result1 result2) && (areEqual result1 result3) && (areEqual result2 result3)

//     Check.One(Config.QuickThrowOnFailure.WithMaxTest(10), property)

// let nullValueByte = 0uy

// [<Test>]
// let polymorphism () =
//     let property (value: byte list) =
//         let result1 =
//             create nullValueByte
//             |> insert nullValueByte (List.toSeq value) 

//         let result2 =
//             create nullValueByte
//             |> insert nullValueByte (List.toSeq value) 
//             |> insert nullValueByte (List.toSeq value)
//             |> insert nullValueByte (List.toSeq value) 
//             |> insert nullValueByte (List.toSeq value)
        
//         let trie = create nullValueByte |> insert nullValueByte (List.toSeq value)
//         let result3 = mergeTrie trie trie

//         (areEqual result1 result2) && (areEqual result1 result3) && (areEqual result2 result3)

//     Check.One(Config.QuickThrowOnFailure.WithMaxTest(10), property)