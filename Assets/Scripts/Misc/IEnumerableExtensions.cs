using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class IEnumerableExtensions
{
    public static T GetRandomElement<T>(this IEnumerable<T> enumerable)
    {
        int i = Random.Range(0, enumerable.Count());
        return enumerable.ElementAt(i);
    }

}
