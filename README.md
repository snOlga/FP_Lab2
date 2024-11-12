# FP_Lab2

Сафонова Ольга Данииловна | 368764
F#

Вариант pre-set

[реализация](Trie.fs)

[unit-test](UnitTest1.fs)

[property-test](PropertyTest.fs)

Структура описывается generic-записями:
```
type Node<'T> = { Value: 'T; Children: seq<Node<'T>> }
```

Были использованы бесконечные последовательности seq

Так как структура имеет generic данные, необходимо передавать null-value в функции:
```
let create nullValue = { Value = nullValue; Children = Seq.empty }
```

Вставка реализуется через insert:
```
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
```

Удаление реализуется через delete:
```
[<Test>]
let deleteTest () =
    let expected = create |> insertWord "abc"
    let actual = create |> insertWord "abc" |> insertWord "bca" |> deleteWord "bca"
    Assert.True(areEqual expected actual)
```

Свертка реализуется через foldTrie:
```
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
```

Отображение реализуется через mapTrie:
```
[<Test>]
let mapTest () =
    let expected = create |> insertWord "ABC"
    let actual = create |> insertWord "abc" |> mapTrie (fun (value) -> (System.Char.ToUpper value))
    Assert.True(areEqual expected actual)
```

Фильтрация реализуется через filterTrie:
```
[<Test>]
let filterTest () =
    let expected = create |> insertWord "AB"
    let actual = create |> insertWord "ABc" |> filterTrie (fun (Some value) -> System.Char.IsUpper value)
    Assert.True(areEqual expected actual)
```

Свойства моноида:
```
[<Test>]
let monoid1 () =
    let property (value: string) =
        let result1 = create |> insertWord value |> mergeTrie  create
        let result2 = create |> mergeTrie  (create |> insertWord value)
        let result3 = create |> insertWord value

        (areEqual result1 result2) && (areEqual result1 result3) && (areEqual result2 result3)

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10).WithEndSize(5), property)

[<Test>]
let monoid2 () =
    let property (value1: string) (value2: string) (value3: string) =
        let trie1 = create |> insertWord value1
        let trie2 = create |> insertWord value2
        let trie3 = create |> insertWord value3

        let result1 =  mergeTrie trie1 (mergeTrie trie2 trie3)
        let result2 = mergeTrie (mergeTrie trie1 trie2) trie3

        areEqual result1 result2
    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10).WithEndSize(5),property)
```

Свойство неповторяемости значений:
```
[<Test>]
let propertyOfSet () =
    let property (value: string) =
        let result1 = create |> insertWord value

        let result2 =
            create
            |> insertWord value
            |> insertWord value
            |> insertWord value
            |> insertWord value
        
        let trie = create |> insertWord value

        let result3 = mergeTrie trie trie

        (areEqual result1 result2) && (areEqual result1 result3) && (areEqual result2 result3)

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10), property)
```

Свойство полиморфизма (например, теперь с числами):
```
[<Test>]
let polymorphism () =
    let property (value: byte list) =
        let result1 =
            create
            |> insertWord value

        let result2 =
            create
            |> insertWord value 
            |> insertWord value
            |> insertWord value 
            |> insertWord value
        
        let trie = create |> insertWord value
        let result3 = mergeTrie trie trie

        (areEqual result1 result2) && (areEqual result1 result3) && (areEqual result2 result3)

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10), property)
```

---
Вывод:

Столкнулась с неожиданными проблемами форматирования (частая проблема того, что приходилось методом тыка понять, как компилятор не будет ругаться на написанное)

Всё ещё крайне сложно воспринимать структуру не как коллекцию изменяемых данных, а как константное значение, которое необходимо перестраивать и возвращать новое, а не изменять по пути

Кроме того, в том числе очень сложно думать в контексте рекурсий, а не циклов

Другая проблема случилась в том, что нельзя было возвращать null-value, из-за чего пришлось придумывать костыль :(

Удивило, что pattern-matching требует полного покрытия

Property-test интересная штука