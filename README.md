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
        create nullValueChar |> insert nullValueChar "abc"
    )
```

Удаление реализуется через delete:
```
[<Test>]
let deleteTest () =
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
    create nullValueChar |> insert nullValueChar "abc" |> insert nullValueChar "bca" |> delete "bca"
  )
```

Свертка реализуется через foldTrie:
```
[<Test>]
let foldTest1 () =
    Assert.AreEqual(
        "abc ",
        create nullValueChar
        |> insert nullValueChar "cba"
        |> foldTrie (fun state ch -> string ch + state) ""
    )
```

Отображение реализуется через mapTrie:
```
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
        create nullValueChar
        |> insert nullValueChar "abc"
        |> mapTrie nullValueChar System.Char.ToUpper
    )
```

Фильтрация реализуется через filterTrie:
```
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
        create nullValueChar
        |> insert nullValueChar "ABc"
        |> filterTrie nullValueChar System.Char.IsUpper
    )
```

Свойство моноида:
```
[<Test>]
let monoid () =
    let property (value: string) =
        let result1 =
            create nullValueChar
            |> insert nullValueChar value
            |> insert nullValueChar ""

        let result2 =
            create nullValueChar
            |> insert nullValueChar ""
            |> insert nullValueChar value

        areEqual result1 result2

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10).WithEndSize(1), property)
```

Свойство неповторяемости значений:
```
[<Test>]
let propertyOfSet () =
    let property (value: char) =
        let result1 = { 
                Value = ' '
                Children = seq { yield { Value = value; Children = Seq.empty } } }

        let charSeq = 
            seq {
                yield value
            }

        let result2 =
            create nullValueChar
            |> insert nullValueChar charSeq 
            |> insert nullValueChar charSeq

        areEqual result1 result2

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10).WithEndSize(1), property)
    Assert.Pass()
```

Свойство полиморфизма (например, теперь с числами):
```
let nullValueInt = 0

[<Test>]
let polymorphism () =
    let property (value: byte) =
        let result1 = { 
                Value = nullValueByte
                Children = seq { yield { Value = value; Children = Seq.empty } } }

        let intSeq = 
            seq {
                yield value
            }

        let result2 =
            create nullValueByte
            |> insert nullValueByte intSeq 
            |> insert nullValueByte intSeq

        areEqual result1 result2

    Check.One(Config.QuickThrowOnFailure.WithMaxTest(10).WithEndSize(1), property)
    Assert.Pass()
```

---
Вывод:

Столкнулась с неожиданными проблемами форматирования (частая проблема того, что приходилось методом тыка понять, как компилятор не будет ругаться на написанное)

Всё ещё крайне сложно воспринимать структуру не как коллекцию изменяемых данных, а как константное значение, которое необходимо перестраивать и возвращать новое, а не изменять по пути

Кроме того, в том числе очень сложно думать в контексте рекурсий, а не циклов

Другая проблема случилась в том, что нельзя было возвращать null-value, из-за чего пришлось придумывать костыль :(

Удивило, что pattern-matching требует полного покрытия

Property-test интересная штука