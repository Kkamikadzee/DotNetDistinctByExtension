# DotNetDistinctByExtension
Added three IEnumerable&lt;T> extension that returns sorted distinct elements from a sequence according to a specified key selector function and using a specified comparer to compare keys, with the remaining elements having the maximum keySort value.

### DistinctByWithMax&lt;TSource, TKey, TSort>(this IEnumerable&lt;TSource> source, Func&lt;TSource, TKey> keySelector, Func&lt;TSource, TSort> keySort, IComparer&lt;TSort>? keySortComparer = null, IEqualityComparer&lt;TKey>? duplicateComparer = null)
Returns sorted distinct elements from a sequence according to a specified key selector function and using a specified comparer to compare keys, with the remaining elements having the maximum keySort value. Facade containing a linq query. 

<details>
  <summary>Description</summary>

```csharp
public static System.Collections.Generic.IEnumerable<TSource> DistinctByWithMax<TSource, TKey, TSort>(this System.Collections.Generic.IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TSort> keySort,  System.Collections.Generic.IComparer<TSort>? keySortComparer = null, System.Collections.Generic.IEqualityComparer<TKey>? duplicateComparer = null)
```
### Type Parameters
#### TSource
The type of elements of source.

#### TKey
The type of key to distinguish elements by.

#### TSort
The type of key to maximizing elements by.

### Parameters
#### source
##### IEnumerable&lt;TSource>
The sequence to remove duplicate elements from.

#### keySelector
##### Func&lt;TSource,TKey>
A function to extract the key to distinguish for each element.

#### keySort
##### Func&lt;TSource,TSort>
A function to extract the key maximizing for each element.

#### keySortComparer
##### IComparer&lt;TSort>
An IComparer to compare maximizing keys.

#### duplicateComparer
##### IEqualityComparer&lt;TKey>
An IEqualityComparer to compare maximizing keys.

### Returns
#### IEnumerable&lt;TSource>
An IEnumerable that contains distinct elements from the source sequence, with the elements having the maximum TSort value.

### Exceptions
#### ArgumentNullException
source is null.
</details>


### DistinctByWithMaxSorted&lt;TSource, TKey, TSort>(this IEnumerable&lt;TSource> source, Func&lt;TSource, TKey> keySelector, Func&lt;TSource, TSort> keySort, IComparer&lt;TKey>? keySelectorComparer = null, IComparer&lt;TSort>? keySortComparer = null, IEqualityComparer&lt;TKey>? duplicateComparer = null)
Returns sorted distinct elements from a sequence according to a specified key selector function and using a specified comparer to compare keys, with the remaining elements having the maximum keySort value.

<details>
  <summary>Description</summary>

```csharp
public static System.Collections.Generic.IEnumerable<TSource> DistinctByWithMaxSorted<TSource, TKey, TSort>(this System.Collections.Generic.IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TSort> keySort, System.Collections.Generic.IComparer<TKey>? keySelectorComparer = null, System.Collections.Generic.IComparer<TSort>? keySortComparer = null, System.Collections.Generic.IEqualityComparer<TKey>? duplicateComparer = null)
```
### Type Parameters
#### TSource
The type of elements of source.

#### TKey
The type of key to distinguish elements by.

#### TSort
The type of key to maximizing elements by.

### Parameters
#### source
##### IEnumerable&lt;TSource>
The sequence to remove duplicate elements from.

#### keySelector 
##### Func&lt;TSource,TKey>
A function to extract the key to distinguish for each element.

#### keySort 
##### Func&lt;TSource,TSort>
A function to extract the key maximizing for each element.

#### keySelectorComparer
##### IComparer&lt;TKey>
An IComparer to compare distinguish keys.

#### keySortComparer
##### IComparer&lt;TSort>
An IComparer to compare maximizing keys.

#### duplicateComparer
##### IEqualityComparer&lt;TKey>
An IEqualityComparer to compare maximizing keys.

### Returns
#### IEnumerable&lt;TSource>
An IEnumerable that contains distinct elements from the source sequence, with the elements having the maximum TSort value.

### Exceptions
#### ArgumentNullException
source is null.

### Remarks
This method is implemented by using deferred execution. The immediate return value is an object that stores all the information that is required to perform the action. The query represented by this method is not executed until the object is enumerated either by calling its GetEnumerator method directly or by using foreach in Visual C#.
</details>


### DistinctByWithMaxHash&lt;TSource, TKey, TSort>(this IEnumerable&lt;TSource> source, Func&lt;TSource, TKey> keySelector, Func&lt;TSource, TSort> keySort, IComparer&lt;TSort>? keySortComparer = null, IEqualityComparer&lt;TKey>? duplicateComparer = null)
Returns sorted distinct elements from a sequence according to a specified key selector function and using a specified comparer to compare keys, with the remaining elements having the maximum keySort value. Explicitly uses a dictionary to find duplicates.

<details>
  <summary>Description</summary>

```csharp
public static System.Collections.Generic.IEnumerable<TSource> DistinctByWithMaxHash<TSource, TKey, TSort>(this System.Collections.Generic.IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TSort> keySort,  System.Collections.Generic.IComparer<TSort>? keySortComparer = null, System.Collections.Generic.IEqualityComparer<TKey>? duplicateComparer = null)
```
### Type Parameters
#### TSource
The type of elements of source.

#### TKey
The type of key to distinguish elements by.

#### TSort
The type of key to maximizing elements by.

### Parameters
#### source
##### IEnumerable&lt;TSource>
The sequence to remove duplicate elements from.

#### keySelector 
##### Func&lt;TSource,TKey>
A function to extract the key to distinguish for each element.

#### keySort 
##### Func&lt;TSource,TSort>
A function to extract the key maximizing for each element.

#### keySortComparer
##### IComparer&lt;TSort>
An IComparer to compare maximizing keys.

#### duplicateComparer
##### IEqualityComparer&lt;TKey>
An IEqualityComparer to compare maximizing keys.

### Returns
#### IEnumerable&lt;TSource>
An IEnumerable that contains distinct elements from the source sequence, with the elements having the maximum TSort value.

### Exceptions
#### ArgumentNullException
source is null.
</details>

## Benchmark result
| Method |      Mean |     Error |    StdDev |
|------- |----------:|----------:|----------:|
|   Linq |  6.762 ms | 0.0355 ms | 0.0332 ms |
|   Sort | 25.206 ms | 0.1311 ms | 0.1095 ms |
|   Hash |  6.924 ms | 0.0052 ms | 0.0043 ms |

> ???????????????????? ?????????????? ?? ?????????????????????????????? ??????????.

> ???????????? ????????????, ?????????????? ???????????????? ???????????? ???????? ???? ?????????????????????????? ??????????????:
> ???????? ???????????????? ???????????????? ???????????? FooBar ?? ???????????? Foo, Bar ?? Baz.
> ???????????????????? ???????????????? ??????????????????, ?????????????? ???? ?????????? ?????????????????? ???????????? ?? ?????????????????????? ???????????? Foo ?? Bar.
> ?????? ????????, ???????? ?? ???????????????? ?????????????????? ???????????????? ?????? ?????????????? ?? ?????????????????????? Foo ?? Bar ?? ?????????????? Baz, ?? ???????????????????????????? ?????????????????? ???????????? ???????????????? ???????????? ?? ?????????????? Baz.
