namespace System.Linq;

public static class DistinctByExtension
{
    /// <summary>
    /// Returns distinct elements from a sequence according to a specified key selector function and using a specified comparer to compare keys, with the remaining elements having the maximum keySort value. Facade containing a linq query. 
    /// </summary>
    /// <param name="source">The sequence to remove duplicate elements from.</param>
    /// <param name="keySelector">A function to extract the key to distinguish for each element.</param>
    /// <param name="keySort">A function to extract the key maximizing for each element.</param>
    /// /// <param name="keySortComparer">An IComparer to compare maximizing keys.</param>
    /// <param name="duplicateComparer">An IEqualityComparer to compare maximizing keys.</param>
    /// <typeparam name="TSource">The type of elements of source.</typeparam>
    /// <typeparam name="TKey">The type of key to distinguish elements by.</typeparam>
    /// <typeparam name="TSort">The type of key to maximizing elements by.</typeparam>
    /// <returns>An IEnumerable that contains distinct elements from the source sequence, with the elements having the maximum TSort value.</returns>
    /// <exception cref="ArgumentException">source is null.</exception>
    /// <remarks>This method is implemented by using deferred execution.
    /// The immediate return value is an object that stores all the information that is required to perform the action.
    /// The query represented by this method is not executed until the object is enumerated either by calling its GetEnumerator method directly or by using foreach in Visual C#.</remarks>
    public static IEnumerable<TSource> DistinctByWithMax<TSource, TKey, TSort>(this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector, Func<TSource, TSort> keySort,
        IComparer<TSort>? keySortComparer = null, IEqualityComparer<TKey>? duplicateComparer = null)
    {
        if (source is null)
            throw new ArgumentException("source is null.");

        return source.GroupBy(keySelector, duplicateComparer)
            .Select(g => g.OrderByDescending(keySort, keySortComparer).First());
    }

    /// <summary>
    /// Returns sorted distinct elements from a sequence according to a specified key selector function and using a specified comparer to compare keys, with the remaining elements having the maximum keySort value. 
    /// </summary>
    /// <param name="source">The sequence to remove duplicate elements from.</param>
    /// <param name="keySelector">A function to extract the key to distinguish for each element.</param>
    /// <param name="keySort">A function to extract the key maximizing for each element.</param>
    /// <param name="keySelectorComparer">An IComparer to compare distinguish keys.</param>
    /// <param name="keySortComparer">An IComparer to compare maximizing keys.</param>
    /// <param name="duplicateComparer">An IEqualityComparer to compare maximizing keys.</param>
    /// <typeparam name="TSource">The type of elements of source.</typeparam>
    /// <typeparam name="TKey">The type of key to distinguish elements by.</typeparam>
    /// <typeparam name="TSort">The type of key to maximizing elements by.</typeparam>
    /// <returns>An IEnumerable that contains distinct elements from the source sequence, with the elements having the maximum TSort value.</returns>
    /// <exception cref="ArgumentException">source is null.</exception>
    /// <remarks>This method is implemented by using deferred execution.
    /// The immediate return value is an object that stores all the information that is required to perform the action.
    /// The query represented by this method is not executed until the object is enumerated either by calling its GetEnumerator method directly or by using foreach in Visual C#.</remarks>
    public static IEnumerable<TSource> DistinctByWithMaxSorted<TSource, TKey, TSort>(this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector, Func<TSource, TSort> keySort,
        IComparer<TKey>? keySelectorComparer = null, IComparer<TSort>? keySortComparer = null,
        IEqualityComparer<TKey>? duplicateComparer = null)
    {
        if (source is null)
            throw new ArgumentException("source is null.");

        duplicateComparer ??= EqualityComparer<TKey>.Default;

        var sortedCollection = source.OrderByDescending(keySelector, keySelectorComparer)
            .ThenByDescending(keySort, keySortComparer);

        using var sortedCollectionEnumerator = sortedCollection.GetEnumerator();

        if (!sortedCollectionEnumerator.MoveNext())
            yield break;

        TSource previousObject = sortedCollectionEnumerator.Current;
        yield return previousObject;

        while (sortedCollectionEnumerator.MoveNext())
        {
            TSource currentObject = sortedCollectionEnumerator.Current;

            if (duplicateComparer.Equals(keySelector(previousObject), keySelector(currentObject))) continue;

            yield return currentObject;
            previousObject = currentObject;
        }
    }

    /// <summary>
    /// Returns sorted distinct elements from a sequence according to a specified key selector function and using a specified comparer to compare keys, with the remaining elements having the maximum keySort value. Explicitly uses a dictionary to find duplicates. 
    /// </summary>
    /// <param name="source">The sequence to remove duplicate elements from.</param>
    /// <param name="keySelector">A function to extract the key to distinguish for each element.</param>
    /// <param name="keySort">A function to extract the key maximizing for each element.</param>
    /// <param name="keySortComparer">An IComparer to compare maximizing keys.</param>
    /// <param name="duplicateComparer">An IEqualityComparer to compare maximizing keys.</param>
    /// <typeparam name="TSource">The type of elements of source.</typeparam>
    /// <typeparam name="TKey">The type of key to distinguish elements by.</typeparam>
    /// <typeparam name="TSort">The type of key to maximizing elements by.</typeparam>
    /// <returns>An IEnumerable that contains distinct elements from the source sequence, with the elements having the maximum TSort value.</returns>
    /// <exception cref="ArgumentException">source is null.</exception>
    public static IEnumerable<TSource> DistinctByWithMaxHash<TSource, TKey, TSort>(this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector, Func<TSource, TSort> keySort,
        IComparer<TSort>? keySortComparer = null, IEqualityComparer<TKey>? duplicateComparer = null)
        where TKey : notnull
    {
        if (source is null)
            throw new ArgumentException("source is null.");

        keySortComparer ??= Comparer<TSort>.Default;

        var distinctDictionary = new Dictionary<TKey, TSource>(duplicateComparer);

        foreach (var objectInCollection in source)
        {
            if (distinctDictionary.TryGetValue(keySelector(objectInCollection), out TSource? objectInSet))
            {
                if (keySortComparer.Compare(keySort(objectInSet), keySort(objectInCollection)) < 0)
                {
                    distinctDictionary.Remove(keySelector(objectInSet));
                    distinctDictionary.Add(keySelector(objectInCollection), objectInCollection);
                }

                continue;
            }

            distinctDictionary.Add(keySelector(objectInCollection), objectInCollection);
        }

        return distinctDictionary.Values;
    }
}