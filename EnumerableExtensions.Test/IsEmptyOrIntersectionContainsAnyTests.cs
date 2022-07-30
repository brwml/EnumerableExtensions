namespace EnumerableExtensions.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bogus;

using Xunit;

public class IsEmptyOrIntersectionContainsAnyTests
{
    [Theory]
    [MemberData(nameof(GetGuidTestData))]
    public void GuidTests(IEnumerable<Guid>? first, IEnumerable<Guid>? second, bool expected)
    {
        Assert.Equal(expected, first.IsEmptyOrIntersectionContainsAny(second));
    }

    [Theory]
    [MemberData(nameof(GetStringTestData))]
    public void StringTests(IEnumerable<string>? first, IEnumerable<string>? second, bool expected)
    {
        Assert.Equal(expected, first.IsEmptyOrIntersectionContainsAny(second));
    }

    [Theory]
    [MemberData(nameof(GetStringComparerTestData))]
    public void StringComparerTests(IEnumerable<string>? first, IEnumerable<string>? second, bool expected, IEqualityComparer<string> comparer)
    {
        Assert.Equal(expected, first.IsEmptyOrIntersectionContainsAny(second, comparer));
    }

    public static IEnumerable<object?[]> GetGuidTestData()
    {
        var item = Guid.NewGuid();

        return new[]
        {
            new object?[] { null, null, true },
            new object?[] { Enumerable.Empty<Guid>(), null, true },
            new object?[] { Enumerable.Empty<Guid>(), Enumerable.Empty<Guid>(), true },
            new object?[] { null, Enumerable.Empty<Guid>(), true },

            new object?[] { new[] { item }, new[] { item }, true },
            new object?[] { null, new[] { item }, true },
            new object?[] { new[] { item }, null, false },
            new object?[] { Enumerable.Empty<Guid>(), new[] { item }, true },
            new object?[] { new[] { item }, Enumerable.Empty<Guid>(), false },
            new object?[] { new[] { item }, new[] { Guid.NewGuid() }, false },
            new object?[] { new[] { Guid.NewGuid() }, new[] { item }, false },
            new object?[] { new[] { Guid.NewGuid(), item, Guid.NewGuid() }, new[] { Guid.NewGuid(), item, Guid.NewGuid() }, true },
            new object?[] { new[] { Guid.NewGuid(), item }, new[] { item, Guid.NewGuid() }, true },
            new object?[] { new[] { Guid.NewGuid(), Guid.NewGuid() }, new[] { Guid.NewGuid(), Guid.NewGuid() }, false },
        };
    }

    public static IEnumerable<object?[]> GetStringTestData()
    {
        var faker = new Faker();
        var AnyString = () => faker.Random.AlphaNumeric(faker.Random.Number(5, 10));

        var item = AnyString();

        return new[]
        {
            new object?[] { null, null, true },
            new object?[] { Enumerable.Empty<string>(), null, true },
            new object?[] { Enumerable.Empty<string>(), Enumerable.Empty<string>(), true },
            new object?[] { null, Enumerable.Empty<string>(), true },

            new object?[] { new[] { item }, new[] { item }, true },
            new object?[] { null, new[] { item }, true },
            new object?[] { new[] { item }, null, false },
            new object?[] { Enumerable.Empty<string>(), new[] { item }, true },
            new object?[] { new[] { item }, Enumerable.Empty<string>(), false },
            new object?[] { new[] { item }, new[] { AnyString() }, false },
            new object?[] { new[] { AnyString() }, new[] { item }, false },
            new object?[] { new[] { AnyString(), item, AnyString() }, new[] { AnyString(), item, AnyString() }, true },
            new object?[] { new[] { AnyString(), item }, new[] { item, AnyString() }, true },
            new object?[] { new[] { AnyString(), AnyString() }, new[] { AnyString(), AnyString() }, false },
        };
    }

    public static IEnumerable<object?[]> GetStringComparerTestData()
    {
        var faker = new Faker();
        var AnyString = () => faker.Random.AlphaNumeric(faker.Random.Number(5, 10));

        var item = AnyString();
        var itemLower = item.ToLowerInvariant();
        var itemUpper = item.ToUpperInvariant();

        return new[]
        {
            new object?[] { null, null, true, StringComparer.Ordinal },
            new object?[] { Enumerable.Empty<string>(), null, true, StringComparer.Ordinal },
            new object?[] { Enumerable.Empty<string>(), Enumerable.Empty<string>(), true, StringComparer.Ordinal },
            new object?[] { null, Enumerable.Empty<string>(), true, StringComparer.Ordinal },

            new object?[] { new[] { item }, new[] { item }, true, StringComparer.Ordinal },
            new object?[] { null, new[] { item }, true, StringComparer.Ordinal },
            new object?[] { new[] { item }, null, false, StringComparer.Ordinal },
            new object?[] { Enumerable.Empty<string>(), new[] { item }, true, StringComparer.Ordinal },
            new object?[] { new[] { item }, Enumerable.Empty<string>(), false, StringComparer.Ordinal },
            new object?[] { new[] { item }, new[] { AnyString() }, false, StringComparer.Ordinal },
            new object?[] { new[] { AnyString() }, new[] { item }, false, StringComparer.Ordinal },
            new object?[] { new[] { AnyString(), item, AnyString() }, new[] { AnyString(), item, AnyString() }, true, StringComparer.Ordinal },
            new object?[] { new[] { AnyString(), item }, new[] { item, AnyString() }, true, StringComparer.Ordinal },
            new object?[] { new[] { AnyString(), AnyString() }, new[] { AnyString(), AnyString() }, false, StringComparer.Ordinal },

            new object?[] { new[] { itemLower }, new[] { itemUpper }, true, StringComparer.OrdinalIgnoreCase },
            new object?[] { null, new[] { itemUpper }, true, StringComparer.OrdinalIgnoreCase },
            new object?[] { new[] { itemLower }, null, false, StringComparer.OrdinalIgnoreCase },
            new object?[] { Enumerable.Empty<string>(), new[] { itemUpper }, true, StringComparer.OrdinalIgnoreCase },
            new object?[] { new[] { itemLower }, Enumerable.Empty<string>(), false, StringComparer.OrdinalIgnoreCase },
            new object?[] { new[] { itemLower }, new[] { AnyString() }, false, StringComparer.OrdinalIgnoreCase },
            new object?[] { new[] { AnyString() }, new[] { itemUpper }, false, StringComparer.OrdinalIgnoreCase },
            new object?[] { new[] { AnyString(), itemLower, AnyString() }, new[] { AnyString(), itemUpper, AnyString() }, true, StringComparer.OrdinalIgnoreCase },
            new object?[] { new[] { AnyString(), itemLower }, new[] { itemUpper, AnyString() }, true, StringComparer.OrdinalIgnoreCase },
            new object?[] { new[] { AnyString(), AnyString() }, new[] { AnyString(), AnyString() }, false, StringComparer.OrdinalIgnoreCase },

            new object?[] { new[] { itemLower }, new[] { itemUpper }, false, StringComparer.Ordinal },
            new object?[] { null, new[] { itemUpper }, true, StringComparer.Ordinal },
            new object?[] { new[] { itemLower }, null, false, StringComparer.Ordinal },
            new object?[] { Enumerable.Empty<string>(), new[] { itemUpper }, true, StringComparer.Ordinal },
            new object?[] { new[] { itemLower }, Enumerable.Empty<string>(), false, StringComparer.Ordinal },
            new object?[] { new[] { itemLower }, new[] { AnyString() }, false, StringComparer.Ordinal },
            new object?[] { new[] { AnyString() }, new[] { itemUpper }, false, StringComparer.Ordinal },
            new object?[] { new[] { AnyString(), itemLower, AnyString() }, new[] { AnyString(), itemUpper, AnyString() }, false, StringComparer.Ordinal },
            new object?[] { new[] { AnyString(), itemLower }, new[] { itemUpper, AnyString() }, false, StringComparer.Ordinal },
            new object?[] { new[] { AnyString(), AnyString() }, new[] { AnyString(), AnyString() }, false, StringComparer.Ordinal },
        };
    }
}
