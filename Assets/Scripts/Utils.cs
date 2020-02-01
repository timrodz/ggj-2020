using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils {
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T> (this IList<T> ts) {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i) {
            var r = UnityEngine.Random.Range (i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }

    public static int IncrementCount (this Dictionary<string, int> someDictionary, string key) {
        if (!someDictionary.ContainsKey (key))
            someDictionary[key] = 0;

        someDictionary[key]++;
        return someDictionary[key];
    }
}