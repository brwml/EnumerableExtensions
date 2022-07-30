namespace EnumerableExtensions.Test;

using System;
using System.Collections.Generic;
using System.Linq;

using Bogus;

using Xunit;

public class IsEmptyOrContainsTests
{
    [Theory]
    [MemberData(nameof(GetGuidTestData))]
    public void GuidTests(IEnumerable<Guid>? enumerable, Guid item, bool expected)
    {
        Assert.Equal(expected, enumerable.IsEmptyOrContains(item));
    }

    [Theory]
    [MemberData(nameof(GetStringTestData))]
    public void StringTests(IEnumerable<string>? enumerable, string item, bool expected)
    {
        Assert.Equal(expected, enumerable.IsEmptyOrContains(item));
    }

    [Theory]
    [MemberData(nameof(GetStringComparerTestData))]
    public void StringComparerTests(IEnumerable<string>? enumerable, string item, bool expected, IEqualityComparer<string> comparer)
    {
        Assert.Equal(expected, enumerable.IsEmptyOrContains(item, comparer));
    }

    public static IEnumerable<object?[]> GetGuidTestData()
    {
        var guid = Guid.NewGuid();

        return new[]
        {
            // Case 1: Null enumerable
            new object?[]{ null, Guid.NewGuid(), true },

            // Case 2: Empty enumerable
            new object?[]{ Enumerable.Empty<Guid>(), Guid.NewGuid(), true },

            // Case 3: Single item enumerable contains the item
            new object?[]{ new[] { guid }, guid, true },

            // Case 4: Single item enumerable does not contain the item
            new object?[]{ new[] { Guid.NewGuid() }, Guid.NewGuid(), false },

            // Case 5: Multiple item enumerable contains the item
            new object?[]{ new[] { Guid.NewGuid(), guid, Guid.NewGuid() }, guid, true },

            // Case 6: Multiple item enumerable does not contains the item
            new object?[]{ new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() }, Guid.NewGuid(), false },
        };
    }

    public static IEnumerable<object?[]> GetStringTestData()
    {
        var faker = new Faker();
        var AnyString = () => faker.Random.AlphaNumeric(faker.Random.Number(10, 20));

        var item = AnyString();

        return new[]
        {
            // Case 1: Null enumerable
            new object?[]{ null, AnyString(), true },

            // Case 2: Empty enumerable
            new object?[]{ Enumerable.Empty<string>(), AnyString(), true },

            // Case 3: Single item enumerable contains the item
            new object?[]{ new[] { item }, item, true },

            // Case 4: Single item enumerable does not contain the item
            new object?[]{ new[] { AnyString() }, AnyString(), false },

            // Case 5: Multiple item enumerable contains the item
            new object?[]{ new[]{ AnyString(), item, AnyString() }, item, true },

            // Case 6: Multiple item enumerable does not contains the item
            new object?[]{ new[] { AnyString(), AnyString(), AnyString() }, AnyString(), false },

            // Case 5: Multiple item enumerable contains the item, differs by case
            new object?[]{ new[]{ AnyString(), item.ToLowerInvariant(), AnyString() }, item.ToUpperInvariant(), true },
        };
    }

    public static IEnumerable<object?[]> GetStringComparerTestData()
    {
        var faker = new Faker();
        var AnyString = () => faker.Random.AlphaNumeric(faker.Random.Number(10, 20));

        var item = AnyString();

        return new[]
        {
            // Case 1: Null enumerable
            new object?[]{ null, AnyString(), true, StringComparer.Ordinal },

            // Case 2: Empty enumerable
            new object?[]{ Enumerable.Empty<string>(), AnyString(), true, StringComparer.Ordinal },

            // Case 3: Single item enumerable contains the item
            new object?[]{ new[] { item }, item, true, StringComparer.Ordinal },

            // Case 4: Single item enumerable does not contain the item
            new object?[]{ new[] { AnyString() }, AnyString(), false, StringComparer.Ordinal },

            // Case 5: Multiple item enumerable contains the item
            new object?[]{ new[]{ AnyString(), item, AnyString() }, item, true, StringComparer.Ordinal },

            // Case 6: Multiple item enumerable does not contains the item
            new object?[]{ new[] { AnyString(), AnyString(), AnyString() }, AnyString(), false, StringComparer.Ordinal },

            // Case 7: Multiple item enumerable contains the item, differs by case
            new object?[]{ new[]{ AnyString(), item.ToLowerInvariant(), AnyString() }, item.ToUpperInvariant(), true, StringComparer.OrdinalIgnoreCase },

            // Case 8: Multiple item enumerable contains the item, differs by case
            new object?[]{ new[]{ AnyString(), item.ToLowerInvariant(), AnyString() }, item.ToUpperInvariant(), false, StringComparer.Ordinal },
        };
    }
}
