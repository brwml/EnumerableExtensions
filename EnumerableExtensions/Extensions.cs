namespace EnumerableExtensions;

using System.Collections;

public static class Extensions
{
    /// <summary>
    /// Determines whether the enumerable type is null or empty.
    /// </summary>
    /// <typeparam name="T">The item type of the enumerable</typeparam>
    /// <param name="enumerable">The enumerable</param>
    /// <returns><c>true</c> if the enumerable is null or empty; otherwise <c>false</c></returns>
    public static bool IsNullOrEmpty(this IEnumerable? enumerable)
    {
        return enumerable is null 
            || !enumerable.GetEnumerator().MoveNext();
    }

    /// <summary>
    /// Determines whether the first enumerable instance has the same length as the second enumerable instance.
    /// The two instances do not need to have the same item type. An instance that is null or empty is 
    /// considered to have a length of 0.
    /// </summary>
    /// <typeparam name="TFirst">The type of the first instance</typeparam>
    /// <typeparam name="TSecond">The type of the second instance</typeparam>
    /// <param name="first">The first enumerable instance</param>
    /// <param name="second">The second enumerable instance</param>
    /// <returns><c>true</c> if the two instance have the same length; otherwise <c>false</c>.</returns>
    public static bool HasSameLengthAs(this IEnumerable? first, IEnumerable? second)
    {
        if (first is null)
        {
            return second.IsNullOrEmpty();
        }

        if (second is null)
        {
            return first.IsNullOrEmpty();
        }

        var firstEnumerator = first.GetEnumerator();
        var secondEnumerator = second.GetEnumerator();

        while (firstEnumerator.MoveNext())
        {
            if (!secondEnumerator.MoveNext())
            {
                return false;
            }
        }

        return !secondEnumerator.MoveNext();
    }

    /// <summary>
    /// Determines whether the <paramref name="first"/> parameter is null or empty, or contains the specified <paramref name="item"/>.
    /// </summary>
    /// <typeparam name="T">The type of the item and the items in the collection</typeparam>
    /// <param name="first">The collection of items</param>
    /// <param name="item">The item to locate in the collection</param>
    /// <param name="comparer">The comparer to use when looking for item in the collection</param>
    /// <returns><c>true</c> when the collection is null or empty, or the item is present in the collection; otherwise <c>false</c></returns>
    public static bool IsEmptyOrContains<T>(this IEnumerable<T>? first, T item) where T : IEquatable<T>
    {
        if (first.IsNullOrEmpty())
        {
            return true;
        }

        foreach (var element in first!)
        {
            if (element.Equals(item))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Determines whether the <paramref name="first"/> parameter is null or empty, or contains the specified <paramref name="item"/>
    /// using the specified <paramref name="comparer"/>.
    /// </summary>
    /// <typeparam name="T">The type of the item and the items in the collection</typeparam>
    /// <param name="first">The collection of items</param>
    /// <param name="item">The item to locate in the collection</param>
    /// <param name="comparer">The comparer to use when looking for item in the collection</param>
    /// <returns><c>true</c> when the collection is null or empty, or the item is present in the collection; otherwise <c>false</c></returns>
    public static bool IsEmptyOrContains<T>(this IEnumerable<T>? first, T item, IEqualityComparer<T> comparer)
    {
        if (first.IsNullOrEmpty())
        {
            return true;
        }

        foreach (var element in first!)
        {
            if (comparer.Equals(element, item))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Determines whether the <paramref name="first"/> parameter is null or empty, or contains the specified string <paramref name="item"/>.
    /// </summary>
    /// <param name="first">The collection of items</param>
    /// <param name="item">The string item to locate in the collection</param>
    /// <returns><c>true</c> when the collection is null or empty, or the item is present in the collection; otherwise <c>false</c></returns>
    /// <remarks>
    /// When determining if the <paramref name="item"/> is in the collection, the <see cref="StringComparer.OrdinalIgnoreCase"/> comparer is used.
    /// </remarks>
    public static bool IsEmptyOrContains(this IEnumerable<string> first, string item)
    {
        return first.IsEmptyOrContains(item, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Determines whether the intersection of the <paramref name="first"/> and <paramref name="second"/> parameters are both empty,
    /// or the intersection of the two contains any common items.
    /// </summary>
    /// <typeparam name="T">The type of the items in the two collections</typeparam>
    /// <param name="first">The first collection</param>
    /// <param name="second">The second collection</param>
    /// <param name="comparer">The compare to use when looking for common items</param>
    /// <returns><c>true</c> when the two collections are null or empty, or contain a common item; otherwise <c>false</c></returns>
    public static bool IsEmptyOrIntersectionContainsAny<T>(this IEnumerable<T> first, IEnumerable<T> second, IEqualityComparer<T> comparer)
    {
        if (first.IsNullOrEmpty())
        {
            return second.IsNullOrEmpty();
        }

        if (second.IsNullOrEmpty())
        {
            return first.IsNullOrEmpty();
        }

        var firstEnumerator = first.GetEnumerator();

        while (firstEnumerator.MoveNext())
        {
            var secondEnumerator = second.GetEnumerator();

            while (secondEnumerator.MoveNext())
            {
                if (comparer.Equals(firstEnumerator.Current, secondEnumerator.Current))
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Determines whether the intersection of the <paramref name="first"/> and <paramref name="second"/> parameters are both empty,
    /// or the intersection of the two contains any common items.
    /// </summary>
    /// <typeparam name="T">The type of the items in the two collections</typeparam>
    /// <param name="first">The first collection</param>
    /// <param name="second">The second collection</param>
    /// <param name="comparer">The compare to use when looking for common items</param>
    /// <returns><c>true</c> when the two collections are null or empty, or contain a common item; otherwise <c>false</c></returns>
    public static bool IsEmptyOrIntersectionContainsAny<T>(this IEnumerable<T> first, IEnumerable<T> second) where T : IEquatable<T>
    {
        if (first.IsNullOrEmpty())
        {
            return second.IsNullOrEmpty();
        }

        if (second.IsNullOrEmpty())
        {
            return first.IsNullOrEmpty();
        }

        var firstEnumerator = first.GetEnumerator();

        while (firstEnumerator.MoveNext())
        {
            var secondEnumerator = second.GetEnumerator();

            while (secondEnumerator.MoveNext())
            {
                if (firstEnumerator.Current.Equals(secondEnumerator.Current))
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <summary>
    /// Determines whether the intersection of the <paramref name="first"/> and <paramref name="second"/> parameters are both empty,
    /// or the intersection of the two contains any common items.
    /// </summary>
    /// <param name="first">The first collection</param>
    /// <param name="second">The second collection</param>
    /// <returns><c>true</c> when the two collections are null or empty, or contain a common item; otherwise <c>false</c></returns>
    /// <remarks>
    /// When determining if the <paramref name="first"/> and <paramref name="second"/> parameters contain any common items,
    /// the <see cref="StringComparer.OrdinalIgnoreCase"/> comparer is used.
    /// </remarks>
    public static bool IsEmptyOrIntersectionContainsAny(this IEnumerable<string> first, IEnumerable<string> second)
    {
        return first.IsEmptyOrIntersectionContainsAny(second, StringComparer.OrdinalIgnoreCase);
    }
}
