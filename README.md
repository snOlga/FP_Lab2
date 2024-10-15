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
    Assert.AreEqual(
        create nullValueChar
        |> insert nullValueChar "a"
        |> insert nullValueChar "",
        create nullValueChar
        |> insert nullValueChar ""
        |> insert nullValueChar "a"
    )
```

Свойство неповторяемости значений:
```
[<Test>]
let propertyOfSet () =
    Assert.AreEqual(
        { Value = ' '
          Children = seq { yield { Value = 'a'; Children = Seq.empty } } },
        create nullValueChar
        |> insert nullValueChar "a"
        |> insert nullValueChar "a"
    )
```

Свойство полиморфизма (например, теперь с числами):
```
let nullValueInt = 0

[<Test>]
let polymorphism () =
    Assert.AreEqual(
        { Value = 0
          Children =
            seq {
                yield
                    { Value = 1
                      Children =
                        seq {
                            yield
                                { Value = 2
                                  Children = seq { yield { Value = 3; Children = Seq.empty } } }
                        } }
            } },
        create nullValueInt
        |> insert nullValueInt (seq { 
                                        yield 1 
                                        yield 2
                                        yield 3})
    )
```

---
Вывод:

Столкнулась с неожиданными проблемами форматирования (частая проблема того, что приходилось методом тыка понять, как компилятор не будет ругаться на написанное)

Всё ещё крайне сложно воспринимать структуру не как коллекцию изменяемых данных, а как константное значение, которое необходимо перестраивать и возвращать новое, а не изменять по пути

Кроме того, в том числе очень сложно думать в контексте рекурсий, а не циклов

Другая проблема случилась в том, что нельзя было возвращать null-value, из-за чего пришлось придумывать костыль :(

Удивило, что pattern-matching требует полного покрытия